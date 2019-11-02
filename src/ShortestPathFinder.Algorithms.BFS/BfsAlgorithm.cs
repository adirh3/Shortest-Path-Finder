using System;
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
    public class BfsAlgorithm<T> : IPathFinderAlgorithm where T: Node
    {
        private readonly ILogger<BfsAlgorithm<T>> _logger;
        private readonly IRelationsFinder<T> _relationsFinder;

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously calculates the <b>shortest</b> path between the specified source and destination nodes 
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>Enumeration of the <b>shortest</b> path between the source and destination</returns>
        public Task<IEnumerable<Node>> CalculatePathAsync(Node source, Node destination)
        {
            throw new NotImplementedException();
        }
    }
}