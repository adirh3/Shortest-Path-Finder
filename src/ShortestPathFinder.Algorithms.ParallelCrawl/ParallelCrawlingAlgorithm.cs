using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dasync.Collections;
using Microsoft.Extensions.Logging;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Configuration;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Algorithm.ParallelCrawl
{
    /// <summary>
    /// Finds the <b>shortest</b> path between the source and destination by crawling in parallel
    /// </summary>
    /// <typeparam name="T">The node type</typeparam>
    public class ParallelCrawlingAlgorithm<T> : IPathFinderAlgorithm where T : Node
    {
        private readonly ILogger _logger;
        private readonly IRelationsFinder<T> _relationsFinder;
        private IList<Node> _currentResult;
        private readonly object _currentMinListLock = new object();
        private readonly ConcurrentDictionary<T, int> _nodeResults;
        private readonly int _maxDegreeOfParallelism;

        public ParallelCrawlingAlgorithm(ILogger<ParallelCrawlingAlgorithm<T>> logger,
            IRelationsFinder<T> relationsFinder, ParallelConfiguration parallelConfiguration = null)
        {
            _logger = logger;
            _relationsFinder = relationsFinder;
            _nodeResults = new ConcurrentDictionary<T, int>();
            parallelConfiguration ??= new ParallelConfiguration();
            _maxDegreeOfParallelism = parallelConfiguration.MaxParallelism;
        }

        /// <summary>
        /// Finds the <b>shortest</b> path between the source and destination by crawling in parallel
        /// </summary>
        /// <param name="source">The source node</param>
        /// <param name="destination">The destination node</param>
        /// <returns>An enumeration of the <b>shortest</b> path between the source and the destination</returns>
        public IEnumerable<Node> CalculatePath(Node source, Node destination)
        {
            return CalculatePathAsync(source, destination).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously finds the <b>shortest</b> path between the source and destination by crawling in parallel
        /// </summary>
        /// <param name="source">The source node</param>
        /// <param name="destination">The destination node</param>
        /// <returns>An enumeration of the <b>shortest</b> path between the source and the destination</returns>
        public async Task<IEnumerable<Node>> CalculatePathAsync(Node source, Node destination)
        {
            _logger.LogInformation(
                $"Started Parallel Crawling algorithm to find the shortest path between {source} and {destination}");

            // Cast both nodes to the dest node
            var sourceNode = source as T;
            var destNode = destination as T;
            if (sourceNode == null || destination == null)
            {
                throw new InvalidCastException("Source node and destination nodes are not of the same type!");
            }

            // Add source to the lists
            var results = new List<Node> {sourceNode};
            _nodeResults.TryAdd(sourceNode, 0);

            // Get all relations of source
            var sourceRelations = await _relationsFinder.FindRelationsAsync(sourceNode);

            // Check if dest is within 1 hop to reduce runtime
            if (sourceRelations.Contains(destNode))
            {
                results.Add(destNode);
                return results; // Destination is within 1 hop, no need to keep going
            }

            // Run asynchronously in parallel will improve performance  
            await sourceRelations.ParallelForEachAsync(
                async currNode => { await HandleNode(results, currNode, destNode); },
                _maxDegreeOfParallelism, CancellationToken.None);
            return _currentResult;
        }

        /// <summary>
        /// Handles each node in recursive manner
        /// </summary>
        /// <param name="results">The current results of the node</param>
        /// <param name="currNode">The current node</param>
        /// <param name="destNode">The destination node</param>
        private async Task HandleNode(IEnumerable<Node> results, T currNode, T destNode)
        {
            var currList = results.ToList();
            currList.Add(currNode);

            if (currList.Count >= 6 || (_currentResult != null && currList.Count >= _currentResult.Count - 1) ||
                _nodeResults.TryGetValue(currNode, out var count) &&
                count < currList.Count)
            {
                return;
            }

            var relations = await _relationsFinder.FindRelationsAsync(currNode);

            if (relations.Contains(destNode))
            {
                currList.Add(destNode);
                HandleFoundResult(currList);
                return;
            }

            _nodeResults.AddOrUpdate(currNode, currList.Count,
                (node1, info) => currList.Count);
            foreach (var node in relations)
            {
                if (_nodeResults.ContainsKey(node))
                    continue;
                await HandleNode(currList, node, destNode);
            }
        }


        /// <summary>
        /// Handles a result situation by checking whether or not it is shorter than older result
        /// </summary>
        /// <param name="resultList">The result list</param>
        private void HandleFoundResult(IList<Node> resultList)
        {
            if (_currentResult == null || resultList.Count < _currentResult.Count) // Check if there is better result
            {
                lock (_currentMinListLock)
                {
                    if (_currentResult == null || resultList.Count < _currentResult.Count)
                    {
                        _currentResult = resultList;
                        _logger.LogDebug(
                            $"Found new best result: {string.Join(" - ", resultList.Select(s => s.DisplayName))}");
                    }
                }
            }
        }
    }
}