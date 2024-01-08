using CustomerManagement.Interfaces;
using CustomerManagement.Models;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    public class CustomerService : ICustomerService
    {


        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> CheckExistingEmail(string email)
        {
           return await _customerRepository.CheckExistingEmailAsync(email);
        }

        public async Task<int> CreateCustomer(CustomerDetails customer)
        {
            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            return await _customerRepository.DeleteCustomerAsync(id);
        }

        public async Task<IEnumerable<CustomerDetails>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<CustomerDetails> GetCustomerById(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<bool> UpdateCustomer(CustomerDetails customer)
        {
            return await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
