using CommandLine;

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
        [Option('s', "source", Required = false, HelpText = "The source node")]
        public string Source { get; set; }

        /// <summary>
        /// The destination 
        /// </summary>
        [Option('d', "destination", Required = false, HelpText = "The destination node")]
        public string Destination { get; set; }

        /// <summary>
        /// The relation finders
        /// </summary>
        [Option('f', "finder", MetaValue = "0", Required = false,
            HelpText = "The relation finder, 0 - Wikipedia, 1 - Http")]
        public SupportedGraphs RelationFinder { get; set; } = SupportedGraphs.Unknown;

        /// <summary>
        /// The specified algorithm to run the short path
        /// </summary>
        [Option('a', "algorithm", MetaValue = "0", Required = false,
            HelpText = "The path finder algorithm, 0 - Parallel Crawling, 1 - BFS")]
        public SupportedAlgorithm Algorithm { get; set; } = SupportedAlgorithm.Unknown;
    }
}