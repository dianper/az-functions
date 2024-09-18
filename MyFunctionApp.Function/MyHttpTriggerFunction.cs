using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFunctionApp
{
    public class MyHttpTriggerFunction
    {
        private readonly ILogger<MyHttpTriggerFunction> _logger;

        public MyHttpTriggerFunction(ILogger<MyHttpTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function("MyHttpTriggerFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
