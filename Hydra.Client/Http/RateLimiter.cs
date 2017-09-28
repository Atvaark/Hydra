using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hydra.Client.Http
{
    internal class RateLimiter
    {
        private readonly TimeSpan _defaultDelay = TimeSpan.FromMilliseconds(100);

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private DateTime _lastRequest = DateTime.MinValue;
        
        public async Task Await(TimeSpan? delay = null)
        {
            delay = delay ?? _defaultDelay;

            while (true)
            {
                TimeSpan remaining;
                await _semaphore.WaitAsync();
                try
                {
                    DateTime now = DateTime.UtcNow;
                    TimeSpan elapsed = now.Subtract(_lastRequest);
                    remaining = delay.Value - elapsed;
                    if (remaining < TimeSpan.Zero)
                    {
                        _lastRequest = now;
                        return;
                    }
                }
                finally
                {
                    _semaphore.Release();
                }

                await Task.Delay(remaining);
            }
        }
    }
}