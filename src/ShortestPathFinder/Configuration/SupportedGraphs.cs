namespace ShortestPathFinder.Configuration
{
    /// <summary>
    /// Supported graphs for the ShortestPathFinder
    /// </summary>
    public enum SupportedGraphs
    {
        Unknown=-1,
        /// <summary>
        /// Wikipedia using Wikipedia API
        /// </summary>
        Wikipedia,
        /// <summary>
        /// Http using extracting hrefs
        /// </summary>
        Http
    }
}