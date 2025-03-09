using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersDto>>
    {
        #region Constructor
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
             _orderRepository = orderRepository;
            _mapper = mapper;
        }

        #endregion
        async Task<List<OrdersDto>> IRequestHandler<GetOrdersListQuery, List<OrdersDto>>.Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrderByUserName(request.UserName);

            return _mapper.Map<List<OrdersDto>>(orderList);
        }
    }
}
