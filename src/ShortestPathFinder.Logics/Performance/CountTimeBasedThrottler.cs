using System;
using System.Threading;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Performance;

namespace ShortestPathFinder.Logics.Performance
{
    /// <inheritdoc cref="IThrottler"/>
    /// <summary>
    /// This throttler will throttle cpu usage by reducing threads to the specified count and delay operations by time
    /// </summary>
    public class CountTimeBasedThrottler : IThrottler
    {
        private readonly CountTimeBasedThrottlerConfiguration _configuration;
        private readonly SemaphoreSlim _semaphore;

        public CountTimeBasedThrottler(CountTimeBasedThrottlerConfiguration configuration = null)
        {
            _configuration = configuration ?? new CountTimeBasedThrottlerConfiguration();
            _semaphore = new SemaphoreSlim(_configuration.MaxParallelism);
        }

        /// <summary>
        /// Throttles the specified function by limiting the maximum amount of threads and delaying it
        /// </summary>
        /// <param name="function">The specified function</param>
        public T Throttle<T>(Func<T> function)
        {
            _semaphore.Wait();
            var result = function.Invoke();
            Thread.Sleep(_configuration.DelayBetweenIterations);
            return result;
        }

        /// <summary>
        /// Asynchronously throttles the specified async function by limiting the maximum amount of threads and delaying it
        /// </summary>
        /// <param name="function">The specified async function</param>
        public async Task<T> ThrottleAsync<T>(Func<Task<T>> function)
        {
            await _semaphore.WaitAsync();
            var result = function.Invoke();
            // Add delay after completion
            var _ = result.ContinueWith(async t => await Task.Delay(_configuration.DelayBetweenIterations)); 
            return await result;
        }
    }
}