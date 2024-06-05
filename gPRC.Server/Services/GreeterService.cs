using Grpc.Core;

namespace gRPC.Server.Services
{
    public class GreeterService : CustomerService.CustomerServiceBase
    {
        List<Customer> _customers = new();

        public GreeterService()
        {
            _customers = new List<Customer>
                    {
                        new Customer
                            {
                Id = "12345678",
                    Name = "John Doe",
        Address = "1234 Elm Street, Springfield, IL 62704, USA",
        Age = 30
                        },
    new Customer
    {
        Id = "87654321",
        Name = "Jane Smith",
        Address = "5678 Oak Street, Springfield, IL 62704, USA",
        Age = 25
    },
    new Customer
    {
        Id = "11223344",
        Name = "Alice Johnson",
        Address = "9101 Maple Avenue, Springfield, IL 62704, USA",
        Age = 40
    }
                };
        }

        public override Task<Customer> Get(CustomerRequestId request, ServerCallContext context)
        {
            var customer = _customers.Find(x => x.Id == request.Id);

            return Task.FromResult(customer);
        }
        public override Task<CustomerList> GetAll(Empty request, ServerCallContext context)
        {
            var customerList = new CustomerList();
            customerList.Customers.AddRange(_customers);
            return Task.FromResult(customerList);
        }
        public override Task<Customer> Insert(Customer request, ServerCallContext context)
        {
            _customers.Add(request);
            return Task.FromResult(request);
        }
        public override Task<Empty> Remove(CustomerRequestId request, ServerCallContext context)
        {
            _customers = _customers.Where(x => x.Id != request.Id).ToList();
            return Task.FromResult(new Empty { });
        }
        public override Task<Customer> Update(Customer request, ServerCallContext context)
        {
            var customer = _customers.Find(x => x.Id == request.Id);
            if (customer == null) _customers.Add(request);
            else
            {
                customer.Address = request.Address;
                customer.Age = request.Age;
                customer.Name = request.Name;
            }
            return Task.FromResult(request);
        }
    }
}
