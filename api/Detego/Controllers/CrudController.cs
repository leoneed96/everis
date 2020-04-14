using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Everis.Controllers
{
    [ApiController]
    [Route("")]
    public class CrudController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CrudController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("operators")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Operators.Select(x => new
            {
                x.ID,
                x.Code,
                x.Title
            }).ToListAsync());

        }

        [HttpGet]
        [Route("operator/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _dataContext.Operators
                .Where(x => x.ID == id)
                .Select(x => new
                {
                    x.ID,
                    x.Code,
                    x.Title
                }).SingleOrDefaultAsync();

            if (record == null)
                return NotFound();

            return Ok(record);

        }

        [HttpDelete]
        [Route("operator/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _dataContext.Operators
              .Where(x => x.ID == id)
              .AnyAsync();

            if (!exists)
                return NotFound();

            _dataContext.Operators.Remove(new Operator() { ID = id });
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("operator/create")]
        public async Task<IActionResult> Create(Operator entity)
        {
            _dataContext.Operators.Add(entity);
            await _dataContext.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpPatch]
        [Route("operator")]
        public async Task<IActionResult> Update(Operator entity)
        {
            var exists = await _dataContext.Operators
               .Where(x => x.ID == entity.ID)
               .AnyAsync();
            if (!exists)
                return NotFound();

            _dataContext.Operators.Update(entity);
            await _dataContext.SaveChangesAsync();

            return Ok(entity);
        }


    }
}
