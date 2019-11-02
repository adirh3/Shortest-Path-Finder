using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShortestPathFinder.Common.Configuration;
using ShortestPathFinder.Common.Performance;
using ShortestPathFinder.Configuration;
using ShortestPathFinder.Factories;
using ShortestPathFinder.Graphs.Wikipedia.Objects;
using ShortestPathFinder.Logics.Performance;

namespace ShortestPathFinder
{
    internal static class Program
    {
        private const string AppSettingsJson = "appsettings.json";
        private const string WikipediaConfigSection = "Wikipedia";
        private const string ConfigSection = "Parallel";

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
                .ConfigureServices((context, collection) => ConfigureServices(context, collection, args))
                .UseConsoleLifetime();

        /// <summary>
        /// Configure the services of the host
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services,
            IEnumerable<string> args)
        {
            #region Configuration Services

            var pathFinderArguments =
                PathFinderArgumentParser.ParsePathFinderArguments(hostContext.Configuration, args);

            services.AddSingleton(pathFinderArguments);
            services.Configure<WikipediaFinderConfiguration>(hostContext.Configuration.GetSection(WikipediaConfigSection));
            services.Configure<ParallelConfiguration>(hostContext.Configuration.GetSection(ConfigSection));
            services.Configure<CountTimeBasedThrottlerConfiguration>(hostContext.Configuration.GetSection(ConfigSection));

            #endregion

            // Configure logging
            services.AddLogging();
            services.AddSingleton<HttpClient>();

            services.AddSingleton<IThrottler, CountTimeBasedThrottler>();


            // Add the necessary graph related services
            RelationFinderFactory.AddRelationFinder(services, pathFinderArguments);

            // Add the necessary algorithm service
            AlgorithmFactory.AddRelationFinder(services, pathFinderArguments);

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