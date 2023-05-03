using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Databases;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    //https://localhost:7203/api/manufacturer
    [Route("api/[controller]")]
    [ApiController]
    public class positionController : ControllerBase
    {
        //Variable
        private readonly DataDbContext _dbContext;

        //Cotructure Method
        public positionController(DataDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        //get push put delete
        //get
        [HttpGet]
        public async Task<ActionResult<List<positions>>> getPositions()
        {
            var positions = await _dbContext.positions.ToListAsync();

            if (positions.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(positions);
        }

        //get by id
        [HttpGet("id")]
        public async Task<ActionResult<positions>> getPositionsID(string id)
        {
            var positions = await _dbContext.positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }

        //get by getPositionsID
        [HttpGet("PositionsID")]
        public async Task<ActionResult<positions>> getEmpPositionsID(string id)
        {
            var positions = _dbContext.employees.FirstOrDefault(e => e.empId == id);
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }


        //Post
        [HttpPost]
        public async Task<ActionResult<positions>> postPosition(positions positions)
        {
            try
            {
                _dbContext.positions.Add(positions);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(positions);
        }

        //Put
        [HttpPut]
        public async Task<ActionResult<positions>> putPosition(int id, positions newPositions)
        {
            var positions = await _dbContext.positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }

            positions.position_Id = newPositions.position_Id;
            positions.position_Name = newPositions.position_Name;
            positions.baseSalary = newPositions.baseSalary;
            positions.salaryIncreaseRate = newPositions.salaryIncreaseRate;

            await _dbContext.SaveChangesAsync();
            return Ok(positions);
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<positions>> deletePositions(string id)
        {
            var employees = _dbContext.employees.Where(e => e.position_Id == id).ToList();
            if (employees != null && employees.Count > 0)
            {
                return BadRequest("Cannot delete position with employees assigned to it.");
            }
            var position = _dbContext.positions.SingleOrDefault(p => p.position_Id == id);
            if (position == null)
            {
                return NotFound();
            }
            _dbContext.positions.Remove(position);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
