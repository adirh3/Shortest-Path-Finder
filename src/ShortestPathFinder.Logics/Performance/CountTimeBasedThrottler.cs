using System;
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

        public CountTimeBasedThrottler(CountTimeBasedThrottlerConfiguration configuration = null)
        {
            _configuration = configuration ?? new CountTimeBasedThrottlerConfiguration();
        }

        public T Throttle<T>(Func<T> function)
        {
            throw new NotImplementedException();
        }

        public Task<T> ThrottleAsync<T>(Func<Task<T>> function)
        {
            throw new NotImplementedException();
        }
    }
}