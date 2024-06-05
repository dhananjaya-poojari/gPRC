using gRPC.Client;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace gRPC.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerService.CustomerServiceClient _client;

        public CustomerController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7065");
            _client = new CustomerService.CustomerServiceClient(channel);
        }

        [HttpGet("all")]
        public CustomerList GetAll()
        {
            return _client.GetAll(new Empty() { });
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
