namespace ShortestPathFinder.Configuration
{
    /// <summary>
    /// Argument needed for running the shortest path finder
    /// </summary>
    public class PathFinderArguments
    {
        /// <summary>
        /// The source of the path, the crawling will start from it
        /// </summary>
        public string Source { get; set; } = "Travelling salesman problem";

        /// <summary>
        /// The destination 
        /// </summary>
        public string Destination { get; set; } = "Software engineering";


        /// <summary>
        /// The specified algorithm to run the short path
        /// </summary>
        public string Algorithm { get; set; }

        public PathFinderArguments()
        {
        }

        /// <summary>
        /// Creates an object to represents the running args
        /// </summary>
        public PathFinderArguments(string source, string destination, string algorithm)
            => (Source, Destination, Algorithm) = (source, destination, algorithm);
    }
}