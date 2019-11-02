using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Common.Performance;

namespace ShortestPathFinder.Graphs.Http
{
    /// <summary>
    /// Finds all the links in a web page
    /// </summary>
    public class HttpRelationsFinder : IRelationsFinder<HttpNode>
    {
        private readonly HttpClient _httpClient;
        private readonly IThrottler<Stream> _throttler;
        private readonly IEnumerable<INodeFilter<HttpNode>> _nodeFilters;

        public HttpRelationsFinder(HttpClient httpClient, IThrottler<Stream> throttler,
            IEnumerable<INodeFilter<HttpNode>> nodeFilters = null)
        {
            _httpClient = httpClient;
            _throttler = throttler;
            _nodeFilters = nodeFilters;
        }

        /// <summary>
        /// Finds all the links (href) in the specified web page
        /// </summary>
        /// <param name="node">The specified web page</param>
        /// <returns>An enumeration of all the links in the specified web page</returns>
        public IEnumerable<HttpNode> FindRelations(HttpNode node)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Asynchronously finds all the links (href) in the specified web page
        /// </summary>
        /// <param name="node">The specified web page</param>
        /// <returns>An enumeration of all the links in the specified web page</returns>
        public Task<IEnumerable<HttpNode>> FindRelationsAsync(HttpNode node)
        {
            throw new System.NotImplementedException();
        }
    }
}