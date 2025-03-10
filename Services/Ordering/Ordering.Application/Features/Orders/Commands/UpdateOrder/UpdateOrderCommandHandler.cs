using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository , IMapper mapper , ILogger<UpdateOrderCommandHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderForUpdate == null)
            {
                _logger.LogError($"order with id {request.Id} not exist");
            }
            else
            {
                _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));
                await _orderRepository.UpdateAsync(orderForUpdate);

                _logger.LogInformation($"Order {orderForUpdate.Id} is successfully updated.");
            }

                

            //return Unit.Value;
        }
    }
}
