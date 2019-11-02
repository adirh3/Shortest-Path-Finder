namespace ShortestPathFinder.Common.Configuration
{
    /// <summary>
    /// Argument needed for running the shortest path finder
    /// </summary>
    public class ShortestPathArguments
    {
        /// <summary>
        /// The source of the path, the crawling will start from it
        /// </summary>
        public string Source { get; }
        
        /// <summary>
        /// The destination 
        /// </summary>
        public string Destination { get; }
        
        /// <summary>
        /// Creates an object to represents the running args
        /// </summary>
        public ShortestPathArguments(string source, string destination) =>
            (Source, Destination) = (source, destination);

    }
}