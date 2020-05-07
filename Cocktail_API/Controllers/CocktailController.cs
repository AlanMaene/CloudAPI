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
        public IActionResult CreateCocktail([FromBody] Cocktail newCocktail)
        {
            
            recipesContext.Cocktails.Add(newCocktail);
            recipesContext.SaveChanges();
            return Created("", newCocktail);
        }
        [HttpPut] 
        public IActionResult UpdateCocktail([FromBody] Cocktail cocktail)
        {
            var orgCocktail = recipesContext.Cocktails.Find(cocktail.Id);
            if (orgCocktail == null)
                return NotFound();

            orgCocktail.Name = cocktail.Name;
            orgCocktail.Instructions = cocktail.Instructions;
            orgCocktail.Inventor = cocktail.Inventor;
            orgCocktail.Measurements = cocktail.Measurements;
            recipesContext.SaveChanges();
            return Ok(orgCocktail);
            
        }
        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteCocktail(int id)
        {
            var cocktail = recipesContext.Cocktails.Find(id);
            if (cocktail == null)
                return NotFound();
            recipesContext.Cocktails.Remove(cocktail);
            recipesContext.SaveChanges();
            return NoContent();
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult getCocktailsFromBartender(int id)
        {
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
    }
}