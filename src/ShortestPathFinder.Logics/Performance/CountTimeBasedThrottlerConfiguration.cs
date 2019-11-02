using ShortestPathFinder.Common.Configuration;

namespace ShortestPathFinder.Logics.Performance
{
    /// <summary>
    /// Configuration for <see cref="CountTimeBasedThrottler{T}"/>
    /// </summary>
    public class CountTimeBasedThrottlerConfiguration : ParallelConfiguration
    {
        /// <summary>
        /// The time in milliseconds between each iteration
        /// </summary>
        public int DelayBetweenIterations { get; set; } = 0;
    }
}