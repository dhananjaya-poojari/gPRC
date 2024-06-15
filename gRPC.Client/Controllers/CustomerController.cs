using Microsoft.AspNetCore.Mvc;
using gRPC.Protos;
using Grpc.Core;

namespace gRPC.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerService.CustomerServiceClient _client;

        public CustomerController(CustomerService.CustomerServiceClient client)
        {
            _client = client;
        }

        [HttpGet("all")]
        public CustomerList GetAll()
        {
            try
            {
                return _client.GetAll(new Empty() { }, deadline: DateTime.UtcNow.AddMilliseconds(100));
            }
            catch (RpcException e) when (e.StatusCode == Grpc.Core.StatusCode.DeadlineExceeded)
            {
                throw new Exception("Error" + e.Status.StatusCode);
            }
        }

        [HttpGet("{id:int}")]
        public Customer Get(string id)
        {
            return _client.Get(new CustomerRequestId { Id = id });
        }

        [HttpPost]
        public Customer Post(Customer customer)
        {
            return _client.Insert(customer);
        }

        [HttpPut]
        public Customer Put(Customer customer)
        {
            return _client.Update(customer);
        }

        [HttpDelete]
        public void Delete(string id)
        {
            _client.Remove(new CustomerRequestId { Id = id });
        }
    }
}
