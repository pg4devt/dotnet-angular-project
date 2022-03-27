#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Backend.DataContext;
using Northwind.Backend.Models;
using Northwind.Backend.Services;

namespace Northwind.Backend.Controllers
{
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly IEmployeeService _service;

        public EmployeesController(NorthwindContext context, IEmployeeService service)
        {
            _context = context;
            _service = service;
        }

        /// <summary>
        /// Get Employees
        /// </summary>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("employees")]
        [ProducesResponseType(typeof(EmployeeListResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEmployeesAsync([FromQuery] int? top, [FromQuery] int? skip)
        {
            var lr = new EmployeeListRequest() { Top = top, Skip = skip };

            var result = await _service.GetEmployeesAsync(lr);

            return Ok(result);
        }

        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("employees/{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeAsunc(int id)
        {
            var employee = await _service.GetEmployeeAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("employees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutEmployeeAsync(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("employees")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostEmployeeAsync([FromBody] Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeAsync", new { id = employee.EmployeeId }, employee);
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("employees/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
