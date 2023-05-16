using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebWithAPI.Client.Pages;

namespace WebWithAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public SuperHeroController(DataContext context) 
        {
            _context = context;
        }





        public static List<Comic> comics = new List<Comic> {
        new Comic { Id = 1, Name = "Saqib"},
        new Comic { Id = 2, Name = "Bilal"}
        };
        public static List<SuperHero> hero = new List<SuperHero> {
        new SuperHero { id = 1, FirstName = "Saqib",
                                LastName = "Bajwa",
                                HeroName = "SpiderMan",
                                Comic = comics[0],
                                ComicId = 1, },
        new SuperHero { id = 2, FirstName = "Bilal",
                                LastName = "Muzzafar",
                                HeroName = "BatMan",
                                Comic = comics[1],
        ComicId = 2,}
        };
        private DataContext _context;

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeros()
        {
            var heros =await _context.SuperHeroes.Include(sh => sh.Comic).ToListAsync();
            return Ok(heros);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> Getcomics()
        {
            var comics = _context.Comics.ToListAsync();
            return Ok(comics);
        }

        [HttpGet("{id}")]

        public ActionResult<SuperHero> GetSingleHeros(int id)
        {
            var hero = _context.SuperHeroes
                .Include(h => h.id == id)
                .FirstOrDefaultAsync(h => h.id == id);
            if (hero == null)
            {
                return NotFound("Sorry, no hero here. :/");
            }
            return base.Ok(SuperHeroController.hero);
        }

        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            hero.Comic = null;
            _context.SuperHeroes.Add(hero);
               await _context.SaveChangesAsync();
            return Ok(await GetDbHeros());
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int id)
        {
           var dbHero = await _context.SuperHeroes
        .Include(sh => sh.Comic)
        .FirstOrDefaultAsync(sh => sh.id == id);
            if (dbHero == null)
                return NotFound("Sorry, No Hero Found");
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.HeroName = hero.HeroName;
            dbHero.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();
            return Ok(await GetDbHeros());
    }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero( int id)
        {
            var dbHero = await _context.SuperHeroes
         .Include(sh => sh.Comic)
         .FirstOrDefaultAsync(sh => sh.id == id);
            if (dbHero == null)
                return NotFound("Sorry, No Hero Found");
           
            _context.SuperHeroes.Remove(dbHero);

            await _context.SaveChangesAsync();
            return Ok(await GetDbHeros());
        }




        private async Task<List<SuperHero>> GetDbHeros() 
        {
            return await _context.SuperHeroes.Include(sh => sh.Comic).ToListAsync();
        }
    }
}
