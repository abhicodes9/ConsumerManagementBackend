using CustomerManagement.Models;

namespace CustomerManagement.Interfaces
{
    public interface ICustomerService
    {

        Task<int> CreateCustomer(CustomerDetails customer);
        Task<CustomerDetails> GetCustomerById(int id);
        Task<bool> UpdateCustomer(CustomerDetails customer);
        Task<bool> DeleteCustomer(int id);
        Task<IEnumerable<CustomerDetails>> GetAllCustomers();

        Task<bool> CheckExistingEmail(string email);
    }
}
