using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShortestPathFinder.Algorithm.ParallelCrawl;
using ShortestPathFinder.Algorithms.BFS;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Common.Performance;
using ShortestPathFinder.Graphs.Http;
using ShortestPathFinder.Graphs.Wikipedia;
using ShortestPathFinder.Graphs.Wikipedia.Objects;
using ShortestPathFinder.Graphs.Wikipedia.Utils;
using ShortestPathFinder.Logics.Performance;

namespace ShortestPathFinder
{
    internal static class Program
    {
        private const string AppSettingsJson = "appsettings.json";

        private static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetService<ILogger>();
            try
            {
                await host.StartAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "ShortestPathFinder exited unexpectedly");
            }
        }

        /// <summary>
        /// Creates and configures the host builder
        /// </summary>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureAppConfig)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime();

        /// <summary>
        /// Configure the services of the host
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<HttpClient>();

            services.AddSingleton<IThrottler, CountTimeBasedThrottler>();

            var nodeType = "wiki";
            switch (nodeType)
            {
                case "http":
                    services.AddSingleton<INodeFactory, HttpNodeFactory>();
                    services.AddSingleton<IRelationsFinder<HttpNode>, HttpRelationsFinder>();
                    services.AddSingleton<IPathFinderAlgorithm, BfsAlgorithm<HttpNode>>();
                    break;
                case "wiki":
                    services.AddSingleton<INodeFactory, WikipediaNodeFactory>();
                    services.AddSingleton<IRelationsFinder<WikipediaNode>, WikipediaRelationsFinder>();
                    services.AddSingleton<IPathFinderAlgorithm, ParallelCrawlingAlgorithm<WikipediaNode>>();
                    break;
            }

            // Add the hosted service
            services.AddHostedService<PathFinderService>();
        }

        /// <summary>
        /// Configures the app settings of the host
        /// </summary>
        private static void ConfigureAppConfig(HostBuilderContext hostBuilderContext,
            IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile(AppSettingsJson);
        }

        /// <summary>
        /// Configures the logging of the host
        /// </summary>
        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConfiguration(context.Configuration);
            loggingBuilder.AddConsole();
            loggingBuilder.AddEventLog();
        }
    }
}