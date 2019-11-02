using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ShortestPathFinder.Configuration
{
    /// <summary>
    /// Parses program arguments into the PathFinderArguments
    /// </summary>
    public static class PathFinderArgumentParser
    {
        private const string StartArgs = "StartArgs";

        /// <summary>
        /// Adds the PathFinderArguments with the configuration and the console args 
        /// </summary>
        /// <param name="configuration">The host configuration</param>
        /// <param name="args">Program arguments</param>
        /// <returns>Final parsed PathFinderArguments</returns>
        internal static PathFinderArguments ParsePathFinderArguments(IConfiguration configuration, IEnumerable<string> args)
        {
            var pathFinderArguments = new PathFinderArguments();
            configuration.GetSection(StartArgs).Bind(pathFinderArguments);

            Parser.Default.ParseArguments<PathFinderArguments>(args).WithParsed(arguments =>
                {
                    // Update only if not default value
                    if (!string.IsNullOrEmpty(arguments.Source))
                        pathFinderArguments.Source = arguments.Source;
                    if (!string.IsNullOrEmpty(arguments.Destination))
                        pathFinderArguments.Destination = arguments.Destination;
                    if (arguments.Algorithm != SupportedAlgorithm.Unknown)
                        pathFinderArguments.Algorithm = arguments.Algorithm;
                    if (arguments.RelationFinder != SupportedGraphs.Unknown)
                        pathFinderArguments.RelationFinder = arguments.RelationFinder;
                })
                .WithNotParsed(errors => { Environment.Exit(-1); });
            return pathFinderArguments;
        }
    }
}