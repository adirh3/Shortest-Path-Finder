using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Configuration
{
    /// <summary>
    /// Arguments needed to start the <see cref="PathFinderService"/>
    /// </summary>
    public class PathFinderStartParameters
    {
        /// <summary>
        /// Source node to start from
        /// </summary>
        public Node SourceNode { get; set; }
        
        /// <summary>
        /// Destination node to find
        /// </summary>
        public Node DestinationNode { get; set; }
    }
}