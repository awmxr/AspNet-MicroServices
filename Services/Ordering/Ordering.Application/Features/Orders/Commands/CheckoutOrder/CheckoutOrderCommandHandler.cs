using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        #region Constructor
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IEmailService emailService, ILogger logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            var newOrder = await _orderRepository.AddAsync(order);

            _logger.LogInformation($"Order {newOrder.Id} has successfully created!");

            // send email

            return newOrder.Id;
        }

        private async Task SendEmail(Order order)
        {
            try
            {
                // send emai;
                await _emailService.SendEmail(new Email()
                {
                    To = "test@test.com",
                    Subject = "subj",
                    Body = "body"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError("email has not been sent!");
            }
        }
    }
}
