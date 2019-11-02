namespace ShortestPathFinder.Common.Graph
{
    public interface INodeFactory
    {
        Node CreateNodeFromString(string node);
    }
}