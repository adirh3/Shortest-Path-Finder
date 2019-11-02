using System;
using Microsoft.Extensions.DependencyInjection;
using ShortestPathFinder.Algorithm.ParallelCrawl;
using ShortestPathFinder.Algorithms.BFS;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Configuration;
using ShortestPathFinder.Graphs.Http;
using ShortestPathFinder.Graphs.Wikipedia;
using ShortestPathFinder.Graphs.Wikipedia.Objects;
using ShortestPathFinder.Graphs.Wikipedia.Utils;

namespace ShortestPathFinder.Factories
{
    public static class RelationFinderFactory
    {
        internal static void AddRelationFinder(IServiceCollection services, PathFinderArguments pathFinderArguments)
        {
            switch (pathFinderArguments.RelationFinder)
            {
                case SupportedGraphs.Http:
                    services.AddSingleton<INodeFactory, HttpNodeFactory>();
                    services.AddSingleton<IRelationsFinder<HttpNode>, HttpRelationsFinder>();
                    break;
                case SupportedGraphs.Wikipedia:
                    services.AddSingleton<INodeFactory, WikipediaNodeFactory>();
                    services.AddSingleton<IRelationsFinder<WikipediaNode>, WikipediaRelationsFinder>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}