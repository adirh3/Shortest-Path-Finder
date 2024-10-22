using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Common.Performance;
using ShortestPathFinder.Graphs.Wikipedia.Objects;
using ShortestPathFinder.Graphs.Wikipedia.Utils;

namespace ShortestPathFinder.Graphs.Wikipedia
{
    /// <summary>
    /// Finds all references links in a Wikipedia article using Wikipedia API
    /// </summary>
    public class WikipediaRelationsFinder : IRelationsFinder<WikipediaNode>
    {
        private const string PlContinue = "plcontinue=";
        private readonly ILogger<WikipediaRelationsFinder> _logger;
        private readonly HttpClient _httpClient;
        private readonly IThrottler _throttler;
        private readonly IEnumerable<INodeFilter<WikipediaNode>> _nodeFilters;
        private string _baseUrl;

        public WikipediaRelationsFinder(ILogger<WikipediaRelationsFinder> logger, HttpClient httpClient,
            IThrottler throttler,
            IOptions<WikipediaFinderConfiguration> configuration,
            IEnumerable<INodeFilter<WikipediaNode>> nodeFilters = null)
        {
            _logger = logger;
            _httpClient = httpClient;
            _throttler = throttler; // We need this throttler since we can't use Wiki API as much as we want
            _nodeFilters = nodeFilters ?? new List<INodeFilter<WikipediaNode>>();
            InitiateBaseUrl(configuration.Value);
        }

        /// <summary>
        /// Finds all the referenced articles in a Wikipedia article using Wikipedia API
        /// </summary>
        /// <param name="node">Wikipedia article</param>
        /// <returns>All the referenced articles</returns>
        public IList<WikipediaNode> FindRelations(WikipediaNode node)
        {
            WikipediaApiResult wikipediaApiResult = null;
            var results = new List<WikipediaNode>();
            do
            {
                var apiResultStream = GetWikipediaLinks(node.DisplayName,
                    wikipediaApiResult != null ? wikipediaApiResult.ContinueArgs : string.Empty);
                // Parse from HTTP stream
                if (apiResultStream == null)
                    break;
                wikipediaApiResult = WikipediaApiResultParser.ParseFromStream(apiResultStream);
                EnumerateFilteredResultLinks(wikipediaApiResult, results);
            } while (wikipediaApiResult.Continue);

            return results;
        }

        /// <summary>
        /// Asynchronously finds all the referenced articles in a Wikipedia article using Wikipedia API
        /// </summary>
        /// <param name="node">Wikipedia article</param>
        /// <returns>All the referenced articles</returns>
        public async Task<IList<WikipediaNode>> FindRelationsAsync(WikipediaNode node)
        {
            WikipediaApiResult wikipediaApiResult = null;
            var results = new List<WikipediaNode>();
            do
            {
                var apiResultStream = await GetWikipediaLinksAsync(node.DisplayName,
                    wikipediaApiResult != null ? wikipediaApiResult.ContinueArgs : string.Empty);
                // Parse from HTTP stream
                if (apiResultStream == null)
                    break;
                wikipediaApiResult = await WikipediaApiResultParser.ParseFromStreamAsync(apiResultStream);
                EnumerateFilteredResultLinks(wikipediaApiResult, results);
            } while (wikipediaApiResult.Continue);

            return results;
        }

        /// <summary>
        /// Enumerates through the wikipedia api result links and filters them
        /// </summary>
        /// <param name="wikipediaApiResult"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private void EnumerateFilteredResultLinks(WikipediaApiResult wikipediaApiResult,
            List<WikipediaNode> results)
        {
            results.AddRange(from wikipediaNode in wikipediaApiResult.Links
                let keep = _nodeFilters.Aggregate(true,
                    (current, nodeFilter) => current && nodeFilter.Filter(wikipediaNode))
                where keep
                select wikipediaNode);
        }

        /// <summary>
        /// Initiates the base url of the get request
        /// </summary>
        private void InitiateBaseUrl(WikipediaFinderConfiguration configuration)
        {
            var urlStart = configuration.UseSSL ? "https://" : "http://";
            _baseUrl = urlStart +
                       $"{configuration.Language}.wikipedia.org/w/api.php?action=query&format=json&prop=links&pllimit=max&titles=";
        }

        /// <summary>
        /// Gets all the wikipedia reference links from the specified article using the get-links API
        /// </summary>
        /// <param name="articleName">The specified article name</param>
        /// <param name="continueArgs">Continue args</param>
        /// <returns>Stream of the HTTP result in JSON</returns>
        private Stream GetWikipediaLinks(string articleName, string continueArgs = "")
        {
            var getApi = CreateGetApiUrl(articleName, continueArgs);
            var result = _throttler.Throttle(() => _httpClient.GetStreamAsync(getApi).GetAwaiter().GetResult());
            return result;
        }

        /// <summary>
        /// Asynchronously gets all the wikipedia reference links from the specified article using the get-links API
        /// </summary>
        /// <param name="articleName">The specified article name</param>
        /// <param name="continueArgs">Continue args</param>
        /// <returns>Stream of the HTTP result in JSON</returns>
        private async Task<Stream> GetWikipediaLinksAsync(string articleName, string continueArgs = "")
        {
            var getApi = CreateGetApiUrl(articleName, continueArgs);
            try
            {
                var result = await _throttler.ThrottleAsync(() => _httpClient.GetStreamAsync(getApi));
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Http Request failed");
            }

            return null;
        }

        /// <summary>
        /// Creates get-links API url based on the article name and the continue args
        /// </summary>
        /// <param name="articleName">The article name</param>
        /// <param name="continueArgs">Continue args</param>
        /// <returns></returns>
        private string CreateGetApiUrl(string articleName, string continueArgs)
        {
            var getAPi = _baseUrl + articleName;
            if (continueArgs != string.Empty)
                getAPi += "&" + PlContinue + continueArgs;
            return getAPi;
        }
    }
}