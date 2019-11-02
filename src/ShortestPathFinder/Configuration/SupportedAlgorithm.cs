namespace ShortestPathFinder.Configuration
{
    /// <summary>
    /// Supported algorithms by the ShortPathFinder
    /// </summary>
    public enum SupportedAlgorithm
    {
        Unknown=-1,
        /// <summary>
        /// Search in asynchronous parallel crawling
        /// </summary>
        ParallelCrawl = 0,
        /// <summary>
        /// Breath-first search algorithm
        /// </summary>
        BFS = 1,
    }
}