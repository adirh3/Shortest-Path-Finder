namespace ShortestPathFinder.Common.Graph
{
    /// <summary>
    /// Filter nodes based on the implemented logic
    /// </summary>
    /// <typeparam name="T">Node</typeparam>
    public interface INodeFilter<in T> where T : Node
    {
        /// <summary>
        /// Filters node based on the implemented logic
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Whether node passed the filter or not</returns>
        bool Filter(T node);
    }
}