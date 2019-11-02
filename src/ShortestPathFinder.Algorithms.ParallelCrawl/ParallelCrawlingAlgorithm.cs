using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Algorithm.ParallelCrawl
{
    /// <summary>
    /// Finds the <b>shortest</b> path between the source and destination by crawling in parallel
    /// </summary>
    /// <typeparam name="T">The node type</typeparam>
    public class ParallelCrawlingAlgorithm<T> : IPathFinderAlgorithm where T:Node
    {
        private readonly ILogger _logger;
        private readonly IRelationsFinder<T> _relationsFinder;

        public ParallelCrawlingAlgorithm(ILogger logger, IRelationsFinder<T> relationsFinder)
        {
            _logger = logger;
            _relationsFinder = relationsFinder;
        }

        /// <summary>
        /// Finds the <b>shortest</b> path between the source and destination by crawling in parallel
        /// </summary>
        /// <param name="source">The source node</param>
        /// <param name="destination">The destination node</param>
        /// <returns>An enumeration of the <b>shortest</b> path between the source and the destination</returns>
        public IEnumerable<Node> CalculatePath(Node source, Node destination)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously finds the <b>shortest</b> path between the source and destination by crawling in parallel
        /// </summary>
        /// <param name="source">The source node</param>
        /// <param name="destination">The destination node</param>
        /// <returns>An enumeration of the <b>shortest</b> path between the source and the destination</returns>
        public Task<IEnumerable<Node>> CalculatePathAsync(Node source, Node destination)
        {
            throw new NotImplementedException();
        }
    }
}