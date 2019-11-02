using System;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Graphs.Wikipedia.Objects;

namespace ShortestPathFinder.Graphs.Wikipedia
{
    public static class WikipediaFilterFactory
    {
        /// <summary>
        /// Gets an instance of Wikipedia INodeFilter for the specified filter name
        /// </summary>
        /// <param name="filterName">The specified filter name</param>
        /// <returns>Instance of Wikipedia node filter</returns>
        public static INodeFilter<WikipediaNode> GetNodeFilter(string filterName)
        {
            throw new NotImplementedException();
        }
    }
}