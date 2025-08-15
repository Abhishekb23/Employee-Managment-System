using Managment_back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Managment_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DepartmentModel model)
        {
          await  _context.Departments.AddAsync(new Entities.Department
            {
                Name = model.Name,
                Description = model.Description
            });

            return Ok(await _context.SaveChangesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Departments.Include(d=>d.Employees).ToListAsync());
        }
    }
}
