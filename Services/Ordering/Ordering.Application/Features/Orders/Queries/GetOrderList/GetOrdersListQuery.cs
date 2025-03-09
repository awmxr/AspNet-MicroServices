using MediatR;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    class GetOrdersListQuery(string userName) : IRequest<List<OrdersDto>>
    {
        public string UserName { get; set; } = userName;
        
    }
}
