using System;
using System.Threading.Tasks;
using ShortestPathFinder.Common.Performance;

namespace ShortestPathFinder.Logics.Performance
{
    /// <inheritdoc cref="IThrottler{T}"/>
    /// <summary>
    /// This throttler will throttle cpu usage by reducing threads to the specified count and delay operations by time
    /// </summary>
    public class CountTimeBasedThrottler<T> : IThrottler<T>
    {
        private readonly CountTimeBasedThrottlerConfiguration _configuration;

        public CountTimeBasedThrottler(CountTimeBasedThrottlerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Throttle(Func<T> function)
        {
            throw new NotImplementedException();
        }

        public T ThrottleAsync(Func<Task<T>> function)
        {
            throw new NotImplementedException();
        }
    }
}