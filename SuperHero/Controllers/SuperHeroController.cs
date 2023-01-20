using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
       
        private readonly DataContext _dataContext;

        public SuperHeroController (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetById(int id)
        {
            return Ok(await _dataContext.SuperHeroes.FindAsync(id));
        }

        [HttpPost()]
        [Route("[action]")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            var add = _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await _dataContext.SuperHeroes.FindAsync(request.Id);

            if (dbhero == null)
            {
                return BadRequest("Hero not found");
            }
            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var remove = await _dataContext.SuperHeroes.FindAsync(id);
            _dataContext.SuperHeroes.Remove(remove);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
    }
}
