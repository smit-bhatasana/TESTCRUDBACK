using APIProject.Models;
using APIProject.Repository;
using APIProject.Models;
using APIProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET: api/Employee/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var employee = _repository.GetById(id);

            if (employee == null)
            {
                return NotFound(new
                {
                    message = "Employee not found"
                });
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.EmployeeName) ||
                string.IsNullOrWhiteSpace(employee.Email))
            {
                return BadRequest(new
                {
                    message = "Employee name and email are required"
                });
            }

            if (employee.Salary < 0)
            {
                return BadRequest(new
                {
                    message = "Salary cannot be negative"
                });
            }

            try
            {
                _repository.Create(employee);

                return Ok(new
                {
                    message = "Employee created successfully"
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest(new
                {
                    message = "Please select an existing department"
                });
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict(new
                {
                    message = "This email address already exists"
                });
            }
        }

        // PUT: api/Employee/1
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Employee employee)
        {
            try
            {
                int result = _repository.Update(id, employee);

                if (result == 0)
                {
                    return NotFound(new
                    {
                        message = "Employee not found"
                    });
                }

                return Ok(new
                {
                    message = "Employee updated successfully"
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest(new
                {
                    message = "Please select an existing department"
                });
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict(new
                {
                    message = "This email address already exists"
                });
            }
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int result = _repository.Delete(id);

                if (result == 0)
                {
                    return NotFound(new
                    {
                        message = "Employee not found"
                    });
                }

                return Ok(new
                {
                    message = "Employee deleted successfully"
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return Conflict(new
                {
                    message = "Cannot delete this employee because employee details exist."
                });
            }
        }
    }
}