using System;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Graphs.Wikipedia
{
    /// <summary>
    /// Wikipedia node
    /// </summary>
    public class WikipediaNode : Node
    {
        /// <summary>
        /// Wikipedia article's Id
        /// </summary>
        public int ArticleId { get; }


        public WikipediaNode(string articleName, int articleId) : base(articleName)
        {
            ArticleId = articleId;
        }

        /// <summary>
        /// Returns hash of the wikipedia id
        /// </summary>
        public override int GetHashCode()
        {
            return ArticleId.GetHashCode();
        }
    }
}