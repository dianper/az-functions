using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyFunctionApp
{
    public class MyBlobTriggerFunction
    {
        private readonly ILogger<MyBlobTriggerFunction> _logger;

        public MyBlobTriggerFunction(ILogger<MyBlobTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function("MyBlobTriggerFunction")]
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
