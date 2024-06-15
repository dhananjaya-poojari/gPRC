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
        public override async Task<LongGreetResponse> LongGreet(IAsyncStreamReader<LongGreetRequest> requestStream, ServerCallContext context)
        {
            string result = "";
            while (await requestStream.MoveNext())
            {
                result += string.Format("Hello {0} {1} {2}", requestStream.Current.Greeting.Name, requestStream.Current.Greeting.Id, Environment.NewLine);
            }
            return new LongGreetResponse() { Result = result };
        }
        public override async Task GreetEveryone(IAsyncStreamReader<LongGreetRequest> requestStream, IServerStreamWriter<GreetManyTimeResponse> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var result = String.Format("Hello {0} {1} {2}", requestStream.Current.Greeting.Name, requestStream.Current.Greeting.Id, Environment.NewLine);
                await responseStream.WriteAsync(new GreetManyTimeResponse { Result = result });
            }
        }
    }
}
