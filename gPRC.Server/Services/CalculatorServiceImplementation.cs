using gRPC.Protos;
using Grpc.Core;

namespace gRPC.Server.Services
{
    public class CalculatorServiceImplementation : CalculatorService.CalculatorServiceBase
    {
        public override async Task PrimeNumber(Number request, IServerStreamWriter<ManyTimeResponse> responseStream, ServerCallContext context)
        {
            int k = 2, N = request.Num;
            while (N > 1)
            {
                if (N % k == 0)
                {
                    N = N / k;
                    await responseStream.WriteAsync(new ManyTimeResponse() { Result = Convert.ToString(k) });
                }
                else k++;
            }
        }
        public override async Task<Number> ComputeAverage(IAsyncStreamReader<Number> requestStream, ServerCallContext context)
        {
            int sum = 0, count = 0;
            while (await requestStream.MoveNext())
            {
                sum += requestStream.Current.Num;
                count++;
            }
            return new Number { Num = sum / count };
        }
    }
}
