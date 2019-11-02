using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Common.Algorithm
{
    /// <summary>
    /// An interface for a path finder algorithm
    /// </summary>
    public interface IPathFinderAlgorithm<T> where T : Node
    {
        /// <summary>
        /// Finds a path between the specified <param name="source">source</param>
        /// and <param name="destination">destination</param> nodes
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>An enumeration of the nodes that leads the path between the source to the destination (included)</returns>
        IEnumerable<T> CalculatePath(T source, T destination);

        /// <summary>
        /// Asynchronously finds a path between the specified <param name="source">source</param>
        /// and <param name="destination">destination</param> nodes
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>An enumeration of the nodes that leads the path between the source to the destination (included)</returns>
        Task<IEnumerable<T>> CalculatePathAsync(T source, T destination);
    }
}