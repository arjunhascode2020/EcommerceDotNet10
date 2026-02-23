using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Catalog.Application.Behaviors
{
    public sealed class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();

            _timer.Stop();
            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning(
                    "Long Running Request:{Name} {ElapsedMillisecond}ms",
                    typeof(TRequest).Name,
                    _timer.ElapsedMilliseconds
                    );
            }

            return response;
        }
    }
}
