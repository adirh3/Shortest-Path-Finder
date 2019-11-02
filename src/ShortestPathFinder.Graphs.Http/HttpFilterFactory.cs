using System;
using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Graphs.Http
{
    public static class HttpFilterFactory
    {
        /// <summary>
        /// Gets an instance of http INodeFilter for the specified filter name
        /// </summary>
        /// <param name="filterName">The specified filter name</param>
        /// <returns>Instance of http node filter</returns>
        public static INodeFilter<HttpNode> GetNodeFilter(string filterName)
        {
            throw new NotImplementedException();
        }
    }
}