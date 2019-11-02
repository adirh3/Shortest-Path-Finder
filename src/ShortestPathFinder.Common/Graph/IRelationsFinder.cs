using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortestPathFinder.Common.Graph
{
    /// <summary>
    /// Interface for relation finder - finds all the relations of a node
    /// Note: We return IList because every algorithm will enumerate the result more than once 
    /// </summary>
    public interface IRelationsFinder<T> where T : Node
    {
        /// <summary>
        /// Finds all the relations (1-hop) of the specified node
        /// </summary>
        /// <param name="node">The specified node</param>
        /// <returns>Enumeration of the relations of the specified node</returns>
        IList<T> FindRelations(T node);

        /// <summary>
        /// Asynchronously finds all the relations (1-hop) of the specified node
        /// </summary>
        /// <param name="node">The specified node</param>
        /// <returns>Enumeration of the relations of the specified node</returns>
        Task<IList<T>> FindRelationsAsync(T node);
    }
}