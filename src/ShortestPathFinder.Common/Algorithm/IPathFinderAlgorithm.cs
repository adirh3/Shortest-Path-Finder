using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Common.Algorithm
{
    /// <summary>
    /// An interface for a path finder algorithm
    /// </summary>
    public interface IPathFinderAlgorithm
    {
        /// <summary>
        /// Finds a path between the specified <param name="source">source</param>
        /// and <param name="destination">destination</param> nodes
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>An enumeration of the nodes that leads the path between the source to the destination (included)</returns>
        IEnumerable<Node> CalculatePath(Node source, Node destination);

        /// <summary>
        /// Asynchronously finds a path between the specified <param name="source">source</param>
        /// and <param name="destination">destination</param> nodes
        /// </summary>
        /// <param name="source">The specified source node</param>
        /// <param name="destination">The specified destination node</param>
        /// <returns>An enumeration of the nodes that leads the path between the source to the destination (included)</returns>
        Task<IEnumerable<Node>> CalculatePathAsync(Node source, Node destination);
    }
}