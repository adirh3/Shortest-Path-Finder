using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Graphs.Wikipedia.Objects
{
    /// <summary>
    /// Wikipedia node
    /// </summary>
    public class WikipediaNode : Node
    {
        public WikipediaNode(string articleName) : base(articleName)
        {
        }

        /// <summary>
        /// Returns hash of the wikipedia id
        /// </summary>
        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }
    }
}