using Cocktail_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_API.Controllers
{
    [Route("api/v1/cocktails")]
    public class CocktailController : Controller
    {
        private readonly RecipesContext recipesContext;
        public CocktailController(RecipesContext _context)
        {
            this.recipesContext = _context;
        }
        [HttpPost]
        public IActionResult CreateBartender([FromBody] Bartender newBartender)
        {
            recipesContext.Bartenders.Add(newBartender);
            recipesContext.SaveChanges();
            return Created("", newBartender);
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult getCocktailsFromBartender(int id)
        {

            //List<Cocktail> cocktails = context.Cocktails;

            //var cocktail = context.Cocktails.Include(d => d.Inventor).Where(d => d.Inventor == context.Bartenders.Find(id));
            var cocktail = recipesContext.Cocktails.Where(d => d.Inventor == recipesContext.Bartenders.Find(id));
            if (cocktail == null)
                return NotFound();
            return Ok(cocktail);
        }
        //[Route("byName")]
        [HttpGet]
        public  IQueryable getCocktails(string name, string sort, string dir = "asc")
        {
            IQueryable<Cocktail> query = recipesContext.Cocktails.Include(cocktail => cocktail.Measurements).ThenInclude(d => d.ingredient).Include(cocktail => cocktail.Inventor);
          
             if (!string.IsNullOrWhiteSpace(name))
             {
                 query = query.Where(d => d.Name == name);
             }
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "name":
                        if (dir == "asc")
                            query = query.OrderBy(cocktail => cocktail.Name);
                        else if(dir == "desc")
                            query = query.OrderByDescending(cocktail => cocktail.Name);
                        break;
                    case "bartender":
                        if (dir == "asc")
                            query = query.OrderBy(cocktail => cocktail.Inventor);
                        else if (dir == "desc")
                            query = query.OrderByDescending(cocktail => cocktail.Inventor);
                        break;
                    case "measurements":
                        if (dir == "asc")
                            query = query.OrderBy(cocktail => cocktail.Measurements.Count);
                        else if (dir == "desc")
                            query = query.OrderByDescending(cocktail => cocktail.Measurements.Count);
                        break;
                }
            }

            

            return query ;
        }
        /*
        [HttpGet]
        public List<Measurements> getCocktails(string name)
        {
            IQueryable<Measurements> query = recipesContext.Measurements.Include(d => d.cocktail).Include(d=> d.ingredient);


            return query.ToList();
        }*/
    }
}