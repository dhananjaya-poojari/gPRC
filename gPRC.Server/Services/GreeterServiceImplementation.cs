using gRPC.Protos;
using Grpc.Core;

namespace gRPC.Server.Services
{
    public class GreeterServiceImplementation : GreetingService.GreetingServiceBase
    {
        public override async Task GreetManyTimes(GreetManyTimesRequest request, IServerStreamWriter<GreetManyTimeResponse> responseStream, ServerCallContext context)
        {
            string result = string.Format("hello {0}", request.Greeting.Name);
            foreach (int i in Enumerable.Range(1, 10))
            {
                await responseStream.WriteAsync(new GreetManyTimeResponse() { Result = result });
            }            
        }
    }
}
