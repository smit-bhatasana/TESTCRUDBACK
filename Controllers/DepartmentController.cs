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
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _repository;

        public DepartmentController(DepartmentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Department
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET: api/Department/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var department = _repository.GetById(id);

            if (department == null)
            {
                return NotFound(new
                {
                    message = "Department not found"
                });
            }

            return Ok(department);
        }

        // POST: api/Department
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
            {
                return BadRequest(new
                {
                    message = "Department name is required"
                });
            }

            _repository.Create(department);

            return Ok(new
            {
                message = "Department created successfully"
            });
        }

        // PUT: api/Department/1
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Department department)
        {
            int result = _repository.Update(id, department);

            if (result == 0)
            {
                return NotFound(new
                {
                    message = "Department not found"
                });
            }

            return Ok(new
            {
                message = "Department updated successfully"
            });
        }

        // DELETE: api/Department/1
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
                        message = "Department not found"
                    });
                }

                return Ok(new
                {
                    message = "Department deleted successfully"
                });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return Conflict(new
                {
                    message = "Cannot delete this department because employees belong to it."
                });
            }
        }
    }
}