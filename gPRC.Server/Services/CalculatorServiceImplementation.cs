using gRPC.Protos;
using Grpc.Core;

namespace gRPC.Server.Services
{
    public class CalculatorServiceImplementation : CalculatorService.CalculatorServiceBase
    {
        public override Task<ManyTimeResponse> Sum(NumberArray request, ServerCallContext context)
        {
            int sum = 0;
            foreach (var item in request.Arr)
            {
                sum += item.Num;
            }
            return Task.FromResult(new ManyTimeResponse { Result = sum.ToString() });
        }
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
        public override async Task<ManyTimeResponse> ComputeAverage(IAsyncStreamReader<Number> requestStream, ServerCallContext context)
        {
            int sum = 0, count = 0;
            while (await requestStream.MoveNext())
            {
                sum += requestStream.Current.Num;
                count++;
            }
            return new ManyTimeResponse { Result = Convert.ToString(sum / count) };
        }
        public override async Task ComputeMaximum(IAsyncStreamReader<Number> requestStream, IServerStreamWriter<ManyTimeResponse> responseStream, ServerCallContext context)
        {
            List<Number> results = [];
            while (await requestStream.MoveNext())
            {
                var value = requestStream.Current.Num;
                results.Add(new Number { Num = value });

                await responseStream.WriteAsync(new ManyTimeResponse { Result = Convert.ToString(results.Max(x => x.Num)) });
            }
        }
    }
}
