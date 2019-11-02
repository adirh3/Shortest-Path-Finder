using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Common.Performance;

namespace ShortestPathFinder.Graphs.Wikipedia
{
    /// <summary>
    /// Finds all references links in a Wikipedia article using Wikipedia API
    /// </summary>
    public class WikipediaRelationsFinder : IRelationsFinder<WikipediaNode>
    {
        private readonly HttpClient _httpClient;
        private readonly IThrottler<Stream> _throttler;
        private readonly IEnumerable<INodeFilter<WikipediaNode>> _nodeFilters;

        public WikipediaRelationsFinder(HttpClient httpClient, IThrottler<Stream> throttler,
            IEnumerable<INodeFilter<WikipediaNode>> nodeFilters = null)
        {
            _httpClient = httpClient;
            _throttler = throttler;
            _nodeFilters = nodeFilters;
        }

        /// <summary>
        /// Finds all the referenced articles in a Wikipedia article using Wikipedia API
        /// </summary>
        /// <param name="node">Wikipedia article</param>
        /// <returns>All the referenced articles</returns>
        public IEnumerable<WikipediaNode> FindRelations(WikipediaNode node)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Asynchronously finds all the referenced articles in a Wikipedia article using Wikipedia API
        /// </summary>
        /// <param name="node">Wikipedia article</param>
        /// <returns>All the referenced articles</returns>
        public Task<IEnumerable<WikipediaNode>> FindRelationsAsync(WikipediaNode node)
        {
            throw new System.NotImplementedException();
        }
    }
}