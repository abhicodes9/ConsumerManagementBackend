using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{



    public class CustomerDetails
    {

        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DateofJoining { get; set; }
        public DateTime LastUpdatedDate { get; set; }





        public CustomerDetails(int customerId, string firstName, string lastName, string email,DateTime dateofJoining, DateTime createdDate,DateTime lastUpdatedDate)
        {
            CustomerId= customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateofJoining = dateofJoining;
            CreatedDate = createdDate;
            LastUpdatedDate= lastUpdatedDate;
        }
    }

    
    }
