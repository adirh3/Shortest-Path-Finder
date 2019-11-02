using ShortestPathFinder.Common.Graph;

namespace ShortestPathFinder.Graphs.Http
{
    /// <summary>
    /// Http node 
    /// </summary>
    public class HttpNode : Node
    {
        /// <summary>
        /// The http url 
        /// </summary>
        public string HttpUrl { get; }

        public HttpNode(string httpUrl) : base(httpUrl)
        {
            HttpUrl = httpUrl;
        }

        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }
    }
}