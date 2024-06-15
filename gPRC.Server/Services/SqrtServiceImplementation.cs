using gRPC.Protos;
using Grpc.Core;

namespace gRPC.Server.Services
{
    public class SqrtServiceImplementation : SqrtService.SqrtServiceBase
    {
        public override async Task<sqrtReponse> SquareRoot(sqrtRequest request, ServerCallContext context)
        {
            int number = request.Number;

            if (number >= 0) return new sqrtReponse { Result = (double)Math.Sqrt(number) };
            else throw new RpcException(new Status(StatusCode.InvalidArgument, "number is smaller than 0"));
        }
    }
}
