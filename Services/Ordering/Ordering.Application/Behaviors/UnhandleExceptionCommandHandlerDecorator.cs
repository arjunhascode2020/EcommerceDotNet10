using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;

namespace Ordering.Application.Behaviors
{
    public class UnhandleExceptionCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly ILogger<TCommand> _logger;

        public UnhandleExceptionCommandHandlerDecorator(ICommandHandler<TCommand, TResult> inner,
            ILogger<TCommand> logger)
        {
            _inner = inner;
            _logger = logger;
        }
        public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellation)
        {
            try
            {
                return await _inner.HandleAsync(command, cancellation);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unhandled exception occurred while processing command {CommandName}", typeof(TCommand).Name);
                throw;
            }
        }
    }
}
