using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Services.Events;
using Nop.Plugin.Api.Processing.Utilities;
using Nop.Plugin.Api.Processing.Constants;

namespace Nop.Plugin.Api.Processing.Events
{
    public class Consumers : IConsumer<OrderPaidEvent>
        //, IConsumer<OrderPlacedEvent>
    {
        public async Task HandleEventAsync(OrderPaidEvent eventMessage)
        {
           await AzureQueueUtility.QueueMessageAsync(AppConstants.L_4_S_ORDER_QUEUE_NAME, eventMessage.Order.Id.ToString());
        }
    }
}
