using System;
using System.Threading.Tasks;

namespace ShortestPathFinder.Common.Performance
{
    /// <summary>
    /// Throttle interface to throttle specific function by type 
    /// </summary>
    public interface IThrottler<T>
    {
        /// <summary>
        /// Throttles the specified function
        /// </summary>
        /// <param name="function">The specified function</param>
        /// <returns>The result returned by function after finishing executing</returns>
        T Throttle(Func<T> function);

        /// <summary>
        /// Throttles the specified asynchronous function
        /// </summary>
        /// <param name="function">The specified asynchronous function</param>
        /// <returns>The result returned by function after finishing executing</returns>
        T ThrottleAsync(Func<Task<T>> function);
    }
}