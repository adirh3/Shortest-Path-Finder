namespace ShortestPathFinder.Common.Configuration
{
    public class ParallelConfiguration
    {
        /// <summary>
        /// The maximum amount of threads allowed to run at a time
        /// </summary>
        public int MaxParallelism { get; set; } = 8;
    }
}