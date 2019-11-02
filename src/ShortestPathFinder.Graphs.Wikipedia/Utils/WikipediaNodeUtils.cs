using System;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Graphs.Wikipedia.Objects;

namespace ShortestPathFinder.Graphs.Wikipedia.Utils
{
    /// <summary>
    /// Creates wikipedia node based on the node string
    /// </summary>
    public class WikipediaNodeFactory : INodeFactory
    {
        /// <summary>
        /// Creates new Wikipedia Node based on the string
        /// </summary>
        /// <param name="nodeString">String representation of wikipedia article (can be url or name)</param>
        /// <returns>Wikipedia Node</returns>
        public Node CreateNodeFromString(string nodeString)
        {
            var lastIndexOf = nodeString.LastIndexOf(@"/", StringComparison.Ordinal);
            var articleName = nodeString;
            if (lastIndexOf != -1 && lastIndexOf != nodeString.Length - 1)
            {
                articleName = articleName.Substring(lastIndexOf + 1);
            }

            return new WikipediaNode(articleName.Replace("_", " "));
        }
    }
}