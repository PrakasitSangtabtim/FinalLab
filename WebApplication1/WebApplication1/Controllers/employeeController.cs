using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Databases;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private readonly DataDbContext _dbContext;
        public employeeController(DataDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        //all
        [HttpGet]
        public async Task<ActionResult<List<employees>>> getEmployees()
        {
            var employees = await _dbContext.employees.ToListAsync();

            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // Get id
        [HttpGet("id")]
        public async Task<ActionResult<employees>> getEmployeeById(int id)
        {
            var employees = await _dbContext.employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            
            return Ok(employees);
        }

        //current Salary 
        [HttpGet("current year")]
        public async Task<ActionResult<employees>> getSalarycurrentYear(string id)
        {

            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var yearsOfWork = (DateTime.Now.Year - employee.hireDate.Year)-1;

            var position = _dbContext.positions.Find(employee.position_Id);
            if (position == null)
            {
                return NotFound();
            }
            var salary = (position.baseSalary + (position.baseSalary * position.salaryIncreaseRate))* yearsOfWork;

            return Ok(salary);
        }

        //guess Salary 
        [HttpGet("guess Salary")]
        public async Task<ActionResult<employees>> getguessSalary(string id,int year)
        {

            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            var position = _dbContext.positions.Find(employee.position_Id);
            if (position == null)
            {
                return NotFound();
            }
            var salary = (position.baseSalary + (position.baseSalary * position.salaryIncreaseRate)) * (year-1);

            return Ok(salary);
        }

        //Post
        [HttpPost]
        public async Task<ActionResult<employees>> createEmployees(employees employees)
        {    

            try
            {
                var position = _dbContext.positions.FirstOrDefault(p => p.position_Id == employees.position_Id);
                if (position == null)
                {
                    return BadRequest("Invalid position ID");
                }
  
                _dbContext.employees.Add(employees);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(employees);
        }

        //Put
        [HttpPut]
        public async Task<ActionResult<employees>> putEmployees(string id, employees newEmployees)
        {
            try
            {
                if (_dbContext.positions.FirstOrDefault(p => p.position_Id == newEmployees.position_Id) == null)
                {
                    return BadRequest("Invalid position ID");
                }

                var employees = await _dbContext.employees.FindAsync(id);
                if (employees == null)
                {
                    return NotFound();
                }


                employees.empName = newEmployees.empName;
                employees.email = newEmployees.email;
                employees.phoneNumber = newEmployees.phoneNumber;
                employees.hireDate = newEmployees.hireDate;
                employees.position_Id = newEmployees.position_Id;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

 
          
           
            return Ok(newEmployees);
        }


        //Delete

        [HttpDelete]
        public async Task<ActionResult<employees>> deleteEmployees(string id)
        {
            var employees = await _dbContext.employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            //Remove
            _dbContext.employees.Remove(employees);

            //save
            await _dbContext.SaveChangesAsync();

            return Ok(employees);
        }



    }
}
