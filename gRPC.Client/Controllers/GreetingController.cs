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

        [HttpPost]
        public async Task<string> Send([FromBody] List<Greeting> greetings)
        {
            var stream = _client.LongGreet();
            foreach (Greeting greet in greetings)
            {
                var request = new LongGreetRequest { Greeting = greet };
                await stream.RequestStream.WriteAsync(request);
            }
            await stream.RequestStream.CompleteAsync();

            var response = await stream.ResponseAsync;

            return response.Result;
        }
    }
}
