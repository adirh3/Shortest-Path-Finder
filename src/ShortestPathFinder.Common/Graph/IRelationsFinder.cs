using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortestPathFinder.Common.Graph
{
    public interface IRelationsFinder
    {
        IEnumerable<Node> FindRelations(Node node);
        
        Task<IEnumerable<Node>> FindRelationsAsync(Node node);
    }
}