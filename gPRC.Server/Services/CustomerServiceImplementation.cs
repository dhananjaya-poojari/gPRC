using Grpc.Core;
using gRPC.Protos;
using gRPC.Server.Data;

namespace gRPC.Server.Services
{
    public class CustomerServiceImplementation : CustomerService.CustomerServiceBase
    {
        private readonly AppDBContext _appDBContext;

        public CustomerServiceImplementation(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public override Task<Customer> Get(CustomerRequestId request, ServerCallContext context)
        {
            var customer = _appDBContext.Customers.First(x => x.Id == request.Id);
            return Task.FromResult(customer);
        }
        public override Task<CustomerList> GetAll(Empty request, ServerCallContext context)
        {
            var customerList = new CustomerList();
            var customers = _appDBContext.Customers.ToList();
            customerList.Customers.AddRange(customers);
            return Task.FromResult(customerList);
        }
        public override Task<Customer> Insert(Customer request, ServerCallContext context)
        {
            _appDBContext.Customers.Add(request);
            _appDBContext.SaveChanges();
            return Task.FromResult(request);
        }
        public override Task<Empty> Remove(CustomerRequestId request, ServerCallContext context)
        {
            var customer = _appDBContext.Customers.First(x => x.Id == request.Id);
            _appDBContext.Customers.Remove(customer);
            _appDBContext.SaveChanges();
            return Task.FromResult(new Empty { });
        }
        public override Task<Customer> Update(Customer request, ServerCallContext context)
        {
            var customer = _appDBContext.Customers.First(x => x.Id == request.Id);
            if (customer == null)
            {
                _appDBContext.Customers.Add(request);
            }
            else
            {
                customer.Address = request.Address;
                customer.Age = request.Age;
                customer.Name = request.Name;
                _appDBContext.Customers.Update(customer);
            }
            return Task.FromResult(request);
        }
    }
}
