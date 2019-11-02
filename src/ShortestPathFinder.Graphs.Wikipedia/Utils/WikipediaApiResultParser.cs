using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ShortestPathFinder.Graphs.Wikipedia.Objects;

namespace ShortestPathFinder.Graphs.Wikipedia.Utils
{
    /// <summary>
    /// Wikipedia API result parser
    /// </summary>
    public class WikipediaApiResultParser
    {
        #region Consts

        private const string ContinuePropertyName = "continue";
        private const string PlContinuePropertyName = "plcontinue";
        private const string QueryPropertyName = "query";
        private const string PagesPropertyName = "pages";
        private const string TitlePropertyName = "title";
        private const string LinksPropertyName = "links";

        #endregion

        /// <summary>
        /// Parses the wikipedia API result from the specified stream
        /// </summary>
        /// <param name="stream">The specified stream</param>
        /// <returns>Wikipedia API result object</returns>
        public static WikipediaApiResult ParseFromStream(Stream stream)
        {
            using var jsonDocument = JsonDocument.Parse(stream);
            return ParseJsonDocument(jsonDocument);
        }

        /// <summary>
        /// Asynchronously parses the wikipedia API result from the specified stream
        /// </summary>
        /// <param name="stream">The specified stream</param>
        /// <returns>Wikipedia API result object</returns>
        public static async Task<WikipediaApiResult> ParseFromStreamAsync(Stream stream)
        {
            using var jsonDocument = await JsonDocument.ParseAsync(stream);
            return ParseJsonDocument(jsonDocument);
        }

        /// <summary>
        /// Parses the specified json document
        /// </summary>
        /// <param name="jsonDocument">The specified json document</param>
        private static WikipediaApiResult ParseJsonDocument(JsonDocument jsonDocument)
        {
            var result = new WikipediaApiResult
            {
                Links = new List<WikipediaNode>()
            };
            var rootElement = jsonDocument.RootElement;
            // NOTE: We dont use model parsing (parse json to object)
            // That's because wikipedia returns json with changing tag name (the article id as json tag)
            if (rootElement.TryGetProperty(ContinuePropertyName, out var continueElement) &&
                continueElement.TryGetProperty(PlContinuePropertyName, out var plContinue))
            {
                result.Continue = true;
                result.ContinueArgs = plContinue.GetString();
            }

            if (rootElement.TryGetProperty(QueryPropertyName, out var query) &&
                query.TryGetProperty(PagesPropertyName, out var pages))
            {
                var article = pages.EnumerateObject();
                article.MoveNext(); // Move to the first article
                if (article.Current.Value.TryGetProperty(LinksPropertyName, out var linksElement))
                {
                    var links = linksElement.EnumerateArray();
                    foreach (var linkElement in links)
                    {
                        if (linkElement.TryGetProperty(TitlePropertyName, out var title))
                        {
                            result.Links.Add(new WikipediaNode(title.GetString()));
                        }
                    }
                }
            }

            return result;
        }
    }
}