using System;
using Microsoft.Extensions.DependencyInjection;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Configuration;
using ShortestPathFinder.Graphs.Http;
using ShortestPathFinder.Graphs.Wikipedia;
using ShortestPathFinder.Graphs.Wikipedia.Objects;
using ShortestPathFinder.Graphs.Wikipedia.Utils;

namespace ShortestPathFinder.Factories
{
    /// <summary>
    /// Relation finder factory class
    /// </summary>
    public static class RelationFinderFactory
    {
        /// <summary>
        /// Adds all the graph related objects to the service collection using the path finder arguments
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="pathFinderArguments">The path finder arguments</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when couldn't find object for the arguments</exception>
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