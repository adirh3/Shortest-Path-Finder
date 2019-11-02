namespace ShortestPathFinder.Graphs.Wikipedia.Objects
{
    /// <summary>
    /// Configuration for the <see cref="WikipediaRelationsFinder"/> WikipediaRelationsFinder
    /// </summary>
    public class WikipediaFinderConfiguration
    {
        /// <summary>
        /// Wikipedia server to query. Default is english
        /// </summary>
        public string Language { get; set; } = "en";

        /// <summary>
        /// Whether to establish ssl connection
        /// </summary>
        public bool UseSSL { get; set; } = true;

        //TODO: Add options to authenticate with user and password
    }
}