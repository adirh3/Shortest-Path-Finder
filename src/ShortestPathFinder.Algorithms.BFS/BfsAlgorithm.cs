using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Algorithms.BFS
{
    /// <summary>
    /// Breadth first search (BFS) algorithm is used to find the shortest path between 2 nodes in a graph
    /// </summary>
    public class BfsAlgorithm<T> : IPathFinderAlgorithm where T : Node
    {
        private readonly ILogger<BfsAlgorithm<T>> _logger;
        private readonly IRelationsFinder<T> _relationsFinder;
        private bool _runAsync = true;

        public BfsAlgorithm(ILogger<BfsAlgorithm<T>> logger, IRelationsFinder<T> relationsFinder)
        {
            _logger = logger;
            _relationsFinder = relationsFinder;
        }

        /// <summary>
        /// Calculates the <b>shortest</b> path between the specified source and destination nodes 
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>Enumeration of the <b>shortest</b> path between the source and destination</returns>
        public IEnumerable<Node> CalculatePath(Node source, Node destination)
        {
            _runAsync = false;
            return CalculatePathAsync(source, destination).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously calculates the <b>shortest</b> path between the specified source and destination nodes 
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>Enumeration of the <b>shortest</b> path between the source and destination</returns>
        public async Task<IEnumerable<Node>> CalculatePathAsync(Node source, Node destination)
        {
            _logger.LogInformation(
                $"Started BFS algorithm to find the shortest path between {source} and {destination}");
            var sourceNode = source as T;
            var destNode = destination as T;
            var queue = new Queue<T>();
            var previous = new Dictionary<T, T>();
            queue.Enqueue(sourceNode);
            var result = new List<Node>();
            // Update dictionary for all nodes
            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                if (vertex.Equals(destNode)) // Found destination
                {
                    GetPathForResult(result, vertex, previous, sourceNode);
                    break;
                }

                var relations = _runAsync
                    ? await _relationsFinder.FindRelationsAsync(vertex)
                    : _relationsFinder.FindRelations(vertex);
                foreach (var node in relations)
                {
                    if (previous.ContainsKey(node))
                        continue;
                    previous[node] = vertex;
                    queue.Enqueue(node);
                }
            }

            return result;
        }

        private static void GetPathForResult(List<Node> result, T vertex, Dictionary<T, T> previous, T source)
        {
            // Create it's path
            result.Add(vertex);
            var curr = vertex;
            while (!curr.Equals(source))
            {
                if (!previous.TryGetValue(curr, out curr))
                    break;
                result.Add(curr);
            }

            result.Reverse(); // Reverse it to get the result from the start
        }
    }
}