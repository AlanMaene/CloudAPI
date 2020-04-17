using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_API.Controllers
{
    [Route("api/v1/bartenders")]
    public class BartenderController : Controller
    {
        private readonly RecipesContext context;
        public BartenderController(RecipesContext context) {
            this.context = context;
        }
        /*[HttpGet]
        public List<Bartender> GetAllBartenders()
        {
            return context.Bartenders.ToList();
        }*/
        [HttpPost]
        public IActionResult CreateBartender([FromBody] Bartender newBartender)
        {
            context.Bartenders.Add(newBartender);
            context.SaveChanges();
            return Created("", newBartender);
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult getCocktailsFromBartender(int id) {

            //List<Cocktail> cocktails = context.Cocktails;

            //var cocktail = context.Cocktails.Include(d => d.Inventor).Where(d => d.Inventor == context.Bartenders.Find(id));
            var cocktail = context.Cocktails.Where(d => d.Inventor == context.Bartenders.Find(id));
            if (cocktail == null)
                return NotFound();
            return Ok(cocktail);
        }
        //[Route("byName")]
        [HttpGet]
        public List<Bartender> getBartenderByName(string name)
        {
            IQueryable<Bartender> query = context.Bartenders;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(d => d.Name == name);
            }

            return query.ToList();
        }
    }
}