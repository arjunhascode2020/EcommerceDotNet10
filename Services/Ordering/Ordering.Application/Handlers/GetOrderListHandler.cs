using Ordering.Application.Abstractions;
using Ordering.Application.DTOs;
using Ordering.Application.Mappers;
using Ordering.Application.Queries;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class GetOrderListHandler : IQueryHandler<GetOrderListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _repository;

        public GetOrderListHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<OrderDto>> HandleAsync(GetOrderListQuery query, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetOrdersByUserNameAsync(query.UserName);
            return orders.Select(o => o.ToDto()).ToList();
        }
    }
}
