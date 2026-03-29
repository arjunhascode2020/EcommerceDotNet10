using Ordering.Application.Abstractions;

namespace Ordering.Application.Commands
{
    public record DeleteOrderCommand(int Id) : ICommand;

}
