using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Algorithm.ParallelCrawl
{
    /// <summary>
    /// Finds the <b>shortest</b> path between the source and destination by crawling in parallel
    /// </summary>
    /// <typeparam name="T">The node type</typeparam>
    public class ParallelCrawlingAlgorithm<T> : IPathFinderAlgorithm<T> where T : Node
    {
        private readonly IRelationsFinder<T> _relationsFinder;

        public ParallelCrawlingAlgorithm(IRelationsFinder<T> relationsFinder)
        {
            _relationsFinder = relationsFinder;
        }

        /// <summary>
        /// Finds the <b>shortest</b> path between the source and destination by crawling in parallel
        /// </summary>
        /// <param name="source">The source node</param>
        /// <param name="destination">The destination node</param>
        /// <returns>An enumeration of the <b>shortest</b> path between the source and the destination</returns>
        public IEnumerable<T> CalculatePath(T source, T destination)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously finds the <b>shortest</b> path between the source and destination by crawling in parallel
        /// </summary>
        /// <param name="source">The source node</param>
        /// <param name="destination">The destination node</param>
        /// <returns>An enumeration of the <b>shortest</b> path between the source and the destination</returns>
        public Task<IEnumerable<T>> CalculatePathAsync(T source, T destination)
        {
            throw new NotImplementedException();
        }
    }
}