using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShortestPathFinder.Common.Algorithm;
using ShortestPathFinder.Common.Graph;
using ShortestPathFinder.Configuration;

namespace ShortestPathFinder
{
    /// <summary>
    /// The host of the program used to find the path of the specified nodes
    /// </summary>
    public class PathFinderService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IPathFinderAlgorithm _pathFinderAlgorithm;
        private readonly INodeFactory _nodeFactory;
        private readonly PathFinderArguments _finderArguments;

        public PathFinderService(ILogger<PathFinderService> logger,
            IPathFinderAlgorithm pathFinderAlgorithm, INodeFactory nodeFactory,
            PathFinderArguments finderArguments)
        {
            _logger = logger;
            _pathFinderAlgorithm = pathFinderAlgorithm;
            _nodeFactory = nodeFactory;
            _finderArguments = finderArguments;
        }

        /// <summary>
        /// Starts the path finder service by searching the path between the start and the end nodes using the path
        /// finder algorithm
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("PathFinder service is up");

            // Create nodes object from the specified arguments
            var sourceNode = _nodeFactory.CreateNodeFromString(_finderArguments.Source);
            var destNode = _nodeFactory.CreateNodeFromString(_finderArguments.Destination);

            // Calculate the path using the given path finder algorithm
            var foundPath = await _pathFinderAlgorithm.CalculatePathAsync(sourceNode,
                destNode).ConfigureAwait(false);
            _logger.LogInformation("Found path:");
            _logger.LogInformation(string.Join(" - ", foundPath.Select(s => s.DisplayName)));
            await StopAsync(CancellationToken.None);
        }

        /// <summary>
        /// Stops the path finder service
        /// </summary>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service finished running");
            Environment.Exit(-1);
        }
    }
}