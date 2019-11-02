using System.Collections.Generic;

namespace ShortestPathFinder.Graphs.Wikipedia.Objects
{
    /// <summary>
    /// Represents a Get-Links result returned by Wikipedia API
    /// </summary>
    public class WikipediaApiResult
    {
        /// <summary>
        /// Whether to continue the get request
        /// </summary>
        public bool Continue { get; set; }

        /// <summary>
        /// If <see cref="Continue"/> was specified, use this continue args to pass to the API using plcontinue
        /// </summary>
        public string ContinueArgs { get; set; }

        /// <summary>
        /// List of the wikipedia links found in the result
        /// </summary>
        public IList<WikipediaNode> Links { get; set; }
    }
}