using CustomerManagement.Interfaces;
using CustomerManagement.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(IConfiguration configuration, ILogger<CustomerRepository> logger)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _dbConnection = new SqlConnection(connectionString);
            _logger = logger;
        }

        public async Task<int> CreateCustomerAsync(CustomerDetails task)
        {
            try
            {
                var parameters = new
                {
                    task.FirstName,
                    task.LastName,
                    task.Email,
                    task.DateofJoining,
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now
                };

                string storedProcedureName = "CreateCustomer"; 

                int insertedId = await _dbConnection.ExecuteScalarAsync<int>(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return insertedId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateCustomerAsync");
                throw;
            }
        }
        //public async Task<int> CreateCustomerAsync(CustomerDetails task)
        //{
        //    //CustomerId,FirstName,LastName,Email,CreatedDate,LastUpdatedDate 
        //    try
        //    {
        //        //task.CreatedDate = DateTime.Now;
        //        //task.LastUpdatedDate = DateTime.Now;
        //        //string query = @"INSERT INTO Customers( FirstName, LastName, Email,DateofJoining, CreatedDate,
        //        //LastUpdatedDate)
        //        //VALUES( @FirstName, @LastName, @Email,@DateofJoining,@CreatedDate, @LastUpdatedDate); SELECT CAST(SCOPE_IDENTITY()
        //        //as int);";
        //        //   return await _dbConnection.ExecuteScalarAsync<int>(query, task);


        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in CreateTaskAsync");
        //        throw;
        //    }
        //}

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            try
            {
                string storedProcedureName = "DeleteCustomer"; 

                int affectedRows = await _dbConnection.ExecuteAsync(
                    storedProcedureName,
                    new { CustomerId = customerId },
                    commandType: CommandType.StoredProcedure
                );

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteCustomerAsync");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDetails>> GetAllCustomersAsync()
        {
            try
            {
                string storedProcedureName = "GetAllCustomers"; 

                return await _dbConnection.QueryAsync<CustomerDetails>(
                    storedProcedureName,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllCustomersAsync");
                throw;
            }
        }

        public async Task<CustomerDetails> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                string storedProcedureName = "GetCustomerById"; 

                return await _dbConnection.QueryFirstOrDefaultAsync<CustomerDetails>(
                    storedProcedureName,
                    new { CustomerId = customerId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCustomerByIdAsync");
                throw;
            }
        }


        //public async Task<bool> UpdateCustomerAsync(CustomerDetails customer)
        //{
        //    try
        //    {
        //        //var oldEmailId = getemailById(customer.Id);
        //        //var new emailId = customer.Email;
        //        //if(oldEmailId==ne)
        //        //{
        //        //    //only fields
        //        //}
        //        //else
        //        //{
        //        //    checkifnewemailalreadyexiststs--bad r
        //        //}
        //        //if()
        //        string query = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email,DateofJoining=@DateofJoining,LastUpdatedDate =@LastUpdatedDate  WHERE CustomerId = @CustomerId";
        //        return await _dbConnection.ExecuteAsync(query, new
        //        {
        //            CustomerId = customer.CustomerId,
        //            FirstName=customer.FirstName,
        //            LastName=customer.LastName,
        //            Email=customer.Email,
        //            DateofJoining=customer.DateofJoining,
        //            LastUpdatedDate=DateTime.Now
        //        }) > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in UpdateCustomerAsync");
        //        throw;
        //    }
        //}
        public async Task<bool> UpdateCustomerAsync(CustomerDetails customer)
        {
            try
            {
                string storedProcedureName = "UpdateCustomer"; 

                int affectedRows = await _dbConnection.ExecuteAsync(
                    storedProcedureName,
                    new
                    {
                        customer.CustomerId,
                        customer.FirstName,
                        customer.LastName,
                        customer.Email,
                        customer.DateofJoining,
                        LastUpdatedDate = DateTime.Now
                    },
                    commandType: CommandType.StoredProcedure
                );

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateCustomerAsync");
                throw;
            }
        }
        public async Task<bool> CheckExistingEmailAsync(string email)
        {
            try
            {
                string storedProcedureName = "CheckExistingEmail"; 

                int emailExists = await _dbConnection.ExecuteScalarAsync<int>(
                    storedProcedureName,
                    new { Email = email },
                    commandType: CommandType.StoredProcedure
                );

                return emailExists == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckExistingEmailAsync");
                throw;
            }
        }



    }
}
