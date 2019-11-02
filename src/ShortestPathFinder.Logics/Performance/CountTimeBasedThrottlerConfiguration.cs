namespace ShortestPathFinder.Logics.Performance
{
    /// <summary>
    /// Configuration for <see cref="CountTimeBasedThrottler{T}"/>
    /// </summary>
    public class CountTimeBasedThrottlerConfiguration
    {
        /// <summary>
        /// The maximum amount of threads allowed to run at a time
        /// </summary>
        public int MaxParallelism { get; set; } = 8;

        /// <summary>
        /// The time in milliseconds between each iteration
        /// </summary>
        public int DelayBetweenIterations { get; set; } = 0;
    }
}