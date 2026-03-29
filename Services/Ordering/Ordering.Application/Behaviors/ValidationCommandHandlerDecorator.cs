using FluentValidation;
using Ordering.Application.Abstractions;

namespace Ordering.Application.Behaviors
{
    public class ValidationCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _inner;
        private readonly IEnumerable<IValidator<TCommand>> _validators;

        public ValidationCommandHandlerDecorator(ICommandHandler<TCommand, TResult> inner,
            IEnumerable<IValidator<TCommand>> validators)
        {
            _inner = inner;
            _validators = validators;
        }
        public Task<TResult> HandleAsync(TCommand command, CancellationToken cancellation)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TCommand>(command);
                var validationResults = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();
                if (validationResults.Any())
                {
                    throw new ValidationException(validationResults);
                }
            }
            return _inner.HandleAsync(command, cancellation);
        }
    }
}
