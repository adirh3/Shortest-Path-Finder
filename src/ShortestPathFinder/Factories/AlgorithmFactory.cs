using System;
using Microsoft.Extensions.DependencyInjection;
using ShortestPathFinder.Algorithm.ParallelCrawl;
using ShortestPathFinder.Algorithms.BFS;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Configuration;
using ShortestPathFinder.Graphs.Http;
using ShortestPathFinder.Graphs.Wikipedia.Objects;

namespace ShortestPathFinder.Factories
{
    /// <summary>
    /// Factory to create and add the <see cref="IPathFinderAlgorithm"/> to the service collection
    /// </summary>
    internal class AlgorithmFactory
    {
        /// <summary>
        /// Adds a <see cref="IPathFinderAlgorithm"/> to the services using the path finder arguments 
        /// </summary>
        /// <param name="services">The services collection</param>
        /// <param name="pathFinderArguments">Path finder arguments</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the factory could not find
        /// an algorithm for the specified type</exception>
        internal static void AddRelationFinder(IServiceCollection services, PathFinderArguments pathFinderArguments)
        {
            switch (pathFinderArguments.Algorithm)
            {
                case SupportedAlgorithm.ParallelCrawl:
                    switch (pathFinderArguments.RelationFinder)
                    {
                        case SupportedGraphs.Wikipedia:
                            services.AddSingleton<IPathFinderAlgorithm, ParallelCrawlingAlgorithm<WikipediaNode>>();
                            break;
                        case SupportedGraphs.Http:
                            services.AddSingleton<IPathFinderAlgorithm, ParallelCrawlingAlgorithm<HttpNode>>();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case SupportedAlgorithm.BFS:
                    switch (pathFinderArguments.RelationFinder)
                    {
                        case SupportedGraphs.Wikipedia:
                            services.AddSingleton<IPathFinderAlgorithm, BfsAlgorithm<WikipediaNode>>();
                            break;
                        case SupportedGraphs.Http:
                            services.AddSingleton<IPathFinderAlgorithm, BfsAlgorithm<HttpNode>>();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}