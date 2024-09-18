using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyFunctionApp
{
    public class MyServiceBusTriggerFunction
    {
        private readonly ILogger<MyServiceBusTriggerFunction> _logger;

        public MyServiceBusTriggerFunction(ILogger<MyServiceBusTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function("MyServiceBusTriggerFunction")]
        public async Task Run(
            [ServiceBusTrigger("myqueue", Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            await Task.CompletedTask;

            // Complete the message
            // await messageActions.CompleteMessageAsync(message);
        }
    }
}
