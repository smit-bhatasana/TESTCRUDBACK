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
    public class EmployeeDetailController : ControllerBase
    {
        private readonly EmployeeDetailRepository _repository;

        public EmployeeDetailController(EmployeeDetailRepository repository)
        {
            _repository = repository;
        }

        // GET: api/EmployeeDetail
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET: api/EmployeeDetail/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var detail = _repository.GetById(id);

            if (detail == null)
            {
                return NotFound(new
                {
                    message = "Employee details not found"
                });
            }

            return Ok(detail);
        }

        // POST: api/EmployeeDetail
        [HttpPost]
        public IActionResult Create(EmployeeDetail detail)
        {
            try
            {
                _repository.Create(detail);

                return Ok(new
                {
                    message = "Employee details created successfully"
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest(new
                {
                    message = "Please select an existing employee"
                });
            }
        }

        // PUT: api/EmployeeDetail/1
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, EmployeeDetail detail)
        {
            try
            {
                int result = _repository.Update(id, detail);

                if (result == 0)
                {
                    return NotFound(new
                    {
                        message = "Employee details not found"
                    });
                }

                return Ok(new
                {
                    message = "Employee details updated successfully"
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest(new
                {
                    message = "Please select an existing employee"
                });
            }
        }

        // DELETE: api/EmployeeDetail/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            int result = _repository.Delete(id);

            if (result == 0)
            {
                return NotFound(new
                {
                    message = "Employee details not found"
                });
            }

            return Ok(new
            {
                message = "Employee details deleted successfully"
            });
        }
    }
}