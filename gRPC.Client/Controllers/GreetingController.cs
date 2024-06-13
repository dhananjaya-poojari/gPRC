using Grpc.Net.Client;
using gRPC.Protos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;

namespace gRPC.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly GreetingService.GreetingServiceClient _client;

        public GreetingController(GreetingService.GreetingServiceClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var request = new GreetManyTimesRequest
            {
                Greeting = new Greeting { Id = "1", Name = "Dhananjaya" }
            };
            string output = "";
            using var call = _client.GreetManyTimes(request);

            try
            {
                await foreach (var response in call.ResponseStream.ReadAllAsync())
                {
                    output += response.Result;
                }
            }
            catch (RpcException e)
            {
                Console.WriteLine($"RPC error: {e.Status.Detail}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return output;
        }
    }
}
