using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Algorithms.BFS
{
    /// <summary>
    /// Breadth first search (BFS) algorithm is used to find the shortest path between 2 nodes in a graph
    /// </summary>
    public class BfsAlgorithm<T> : IPathFinderAlgorithm<T> where T: Node
    {
        private readonly IRelationsFinder<T> _relationsFinder;

        public BfsAlgorithm(IRelationsFinder<T> relationsFinder)
        {
            _relationsFinder = relationsFinder;
        }

        /// <summary>
        /// Calculates the <b>shortest</b> path between the specified source and destination nodes 
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>Enumeration of the <b>shortest</b> path between the source and destination</returns>
        public IEnumerable<T> CalculatePath(T source, T destination)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously calculates the <b>shortest</b> path between the specified source and destination nodes 
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>Enumeration of the <b>shortest</b> path between the source and destination</returns>
        public Task<IEnumerable<T>> CalculatePathAsync(T source, T destination)
        {
            throw new NotImplementedException();
        }
    }
}