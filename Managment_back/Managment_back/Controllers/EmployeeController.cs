using Managment_back.Entities;
using Managment_back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Managment_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EmployeeModel model)
        {

            await _context.Employees.AddAsync(new Entities.Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,
                DateOfBirth = model.DateOfBirth,
                HireDate = model.HireDate,
                DepartmentId = model.DepartmentId,
            });

            return Ok(await _context.SaveChangesAsync());
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Employees.ToListAsync());
        }


        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string? search=null)
        {
            var query = _context.Employees.AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(q => q.FirstName.Contains(search) || q.LastName.Contains(search) || q.Address.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var employees = await query.OrderBy(q => q.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new PagedList<Employee>
            {
                Items=employees,
                TotalCount=totalCount,
                PageIndex=pageIndex,
                PageSize=pageSize,
            });
        }

    }
}
