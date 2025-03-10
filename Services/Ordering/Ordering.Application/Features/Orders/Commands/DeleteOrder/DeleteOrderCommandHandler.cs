using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exeptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository , IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository; 
            _mapper = mapper;
        }
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderForDelete == null)
            {
                _logger.LogError($"order with id {request.Id} not exist");
                throw new NotFoundExeption(nameof(Order) , request.Id);

            }
            else
            {
                await _orderRepository.DeleteAsync(orderForDelete);
                _logger.LogInformation($"Order with id: {orderForDelete.Id} has successfully deleted.");
            }

            //return Unit.Value;

            
        }
    }
}
