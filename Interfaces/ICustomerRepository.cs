using CustomerManagement.Models;

namespace CustomerManagement.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDetails>> GetAllCustomersAsync();
        Task<CustomerDetails> GetCustomerByIdAsync(int id);
        Task<int> CreateCustomerAsync(CustomerDetails task);
        Task<bool> UpdateCustomerAsync(CustomerDetails task);
        Task<bool> DeleteCustomerAsync(int id);

        Task<bool> CheckExistingEmailAsync(string email);
    }
}
