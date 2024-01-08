using CustomerManagement.Interfaces;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerservice;

        public CustomerController (ICustomerService customerservice)
        {
            _customerservice = customerservice;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        // GET /api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetails>>> GetAllTasks()
        {
            var tasks = await _customerservice.GetAllCustomers();
            return Ok(tasks);
        }


        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetails>> GetTaskById(int id)
        {
            var task = await _customerservice.GetCustomerById(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST /api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerDetails>> CreateCustomer([FromBody] CustomerDetails customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _customerservice.CheckExistingEmail(customer.Email))
            {
                ModelState.AddModelError("Email", "Email already exists."); 
                return BadRequest(ModelState);
            }
            var createdCustomerId = await _customerservice.CreateCustomer(customer);
            return await _customerservice.GetCustomerById(createdCustomerId);
        }

        // PUT /api/customers/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] CustomerDetails customer)
        {
            //if (id != customer.CustomerId)
            //    return BadRequest();

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (!await _customerservice.UpdateCustomer(customer))
                return NotFound();

            return NoContent();
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<bool> DeleteTask(int id)
        {

            bool value = await _customerservice.DeleteCustomer(id);
            return value;

        }
    }
}
