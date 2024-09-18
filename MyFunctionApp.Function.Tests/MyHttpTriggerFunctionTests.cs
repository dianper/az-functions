using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionApp.Function.Tests
{
    public class MyHttpTriggerFunctionTests : IAsyncLifetime
    {
        private readonly HttpClient httpClient;

        public MyHttpTriggerFunctionTests()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8080")
            };
        }

        public Task InitializeAsync()
        {
            // We can create a mechanism to wait for the function to start or to start the function in a separate process
            return Task.CompletedTask;
        }

        [Fact]
        public async Task Run_MyHttpTriggerFunction_Returns200Ok()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/MyHttpTriggerFunction");

            // Act
            var response = await this.httpClient.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Welcome to Azure Functions!", responseString);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
