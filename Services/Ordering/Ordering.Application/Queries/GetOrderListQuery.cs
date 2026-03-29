using Ordering.Application.Abstractions;
using Ordering.Application.DTOs;

namespace Ordering.Application.Queries
{
    public record GetOrderListQuery(string UserName) : IQuery<List<OrderDto>>;

}
