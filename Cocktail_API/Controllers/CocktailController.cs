using Cocktail_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.JsonWebToken;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using System.Web.Providers.Entities;
using Google.Apis.Auth;

namespace Cocktail_API.Controllers
{
    [Route("api/v1/cocktails")]
    [ApiController]
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
            IQueryable<Cocktail> queryCocktail = recipesContext.Cocktails.Where(d => d.Name == newCocktail.Name);
            if (queryCocktail.Count() == 0)
            {
                IQueryable<Bartender> query = recipesContext.Bartenders.Where(d => d.Name == newCocktail.Inventor.Name);

                if (query.Count() != 0)
                {
                    Bartender bar = query.First();
                    newCocktail.Inventor = bar;
                }

                for (int i = 0; i < newCocktail.Measurements.Count(); i++)
                {
                    IQueryable<Ingredient> queryIngredient = recipesContext.Ingredients.Where(d => d.Name == newCocktail.Measurements[i].ingredient.Name);
                    if (queryIngredient.Count() != 0)
                    {
                        newCocktail.Measurements[i].ingredient = queryIngredient.First();
                    }

                    IQueryable<Measurements> queryMeasurements = recipesContext.Measurements.Where(d => d.ingredient.Name == newCocktail.Measurements[i].ingredient.Name).Where(d => d.measurements == newCocktail.Measurements[i].measurements);
                    if(queryMeasurements.Count() != 0)
                    {
                        newCocktail.Measurements[i] = queryMeasurements.First();
                    }
                }


                recipesContext.Cocktails.Add(newCocktail);
                recipesContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            //return "";

            //recipesContext.Cocktails.Add(newCocktail);
            //recipesContext.SaveChanges();
            return Created("", newCocktail);
        }
        [HttpPut]
        public IActionResult UpdateCocktail([FromBody] Cocktail cocktail)
        {
            var orgCocktail = recipesContext.Cocktails.Where(d=> d.Id == cocktail.Id).Include(cocktail => cocktail.Measurements).ThenInclude(d => d.ingredient).First();
            if (orgCocktail == null)
                return NotFound();
            
            if(cocktail.Name != null) orgCocktail.Name = cocktail.Name;
            if (cocktail.Instructions != null) orgCocktail.Instructions = cocktail.Instructions;
            if (cocktail.Inventor != null) orgCocktail.Inventor = cocktail.Inventor;

            for (int i = 0; i < cocktail.Measurements.Count(); i++)
            {
                bool allowedToAdd = true;
                for (int k = 0; k < orgCocktail.Measurements.Count(); k++)
                {
                    if (cocktail.Measurements[i].ingredient.Name == orgCocktail.Measurements[k].ingredient.Name)
                    {
                        orgCocktail.Measurements[k].measurements = cocktail.Measurements[i].measurements;
                        allowedToAdd = false;
                    }
                }
                if (allowedToAdd == true)
                {
                    orgCocktail.Measurements.Add(cocktail.Measurements[i]);
                }
                /*
                for(int l = 0; l < orgCocktail.Measurements.Count() - cocktail.Measurements.Count(); l++)
                {
                    recipesContext.Remove(orgCocktail.Measurements[cocktail.Measurements.Count()+l]);
                }*/


            }


            //for(int i = 0; i < cocktail.Measurements.Count(); i++)
            //{
            //    bool allowedToAdd = true;
            //    for (int k=0 ; k<orgCocktail.Measurements.Count() ; k++){
            //        if (cocktail.Measurements[i].ingredient.Name == orgCocktail.Measurements[k].ingredient.Name)
            //        {
            //            //only change the number of measurement
            //            cocktail.Measurements[i].measurements = orgCocktail.Measurements[k].measurements;
            //            allowedToAdd = false;
            //        }
            //    }
            //    if(allowedToAdd == true)
            //    {
            //        orgCocktail.Measurements.Add(cocktail.Measurements[i]);
            //    }


            //}


            recipesContext.SaveChanges();
            return Ok(orgCocktail);

        }
        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteCocktail(int id)
        {
            var cocktail = recipesContext.Cocktails.Where(d=> d.Id == id).Include(cocktail => cocktail.Measurements).ThenInclude(d => d.ingredient).First();
            if (cocktail == null)
                return NotFound();
            
            for( int i = 0; i < cocktail.Measurements.Count(); i++)
            {
                recipesContext.Measurements.Where(d => d.measurements == cocktail.Measurements[i].measurements);
            }
            recipesContext.Cocktails.Remove(cocktail);
            recipesContext.SaveChanges();
            return NoContent();
        }


        [Route("{id}")]
        [HttpGet]
        public IActionResult getCocktailsFromId(int id)
        {
            var cocktail = recipesContext.Cocktails.Where(d => d.Id == id).Include(cocktail => cocktail.Measurements).ThenInclude(d => d.ingredient).Include(cocktail => cocktail.Inventor).First();
            if (cocktail == null)
                return NotFound();
            return Ok(cocktail);
        }

        [Route("getMeasurements")]
        [HttpGet]
        public string getM()
        {
            var cocktail = recipesContext.Cocktails.Where(d => d.Id == 1).Include(cocktail => cocktail.Measurements).ThenInclude(d => d.ingredient).Include(cocktail => cocktail.Inventor).First();

            return cocktail.Measurements.Count().ToString();
        }
        //[Route("byName")]
        [Authorize]
        [HttpGet]
        public IQueryable getCocktails(string name, string sort, int? page, string dir = "asc",  int length = 100)
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
                        else if (dir == "desc")
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
            if (page.HasValue)
                query = query.Skip(page.Value * length);
            query = query.Take(length);



            return query;
        }
        
    }
}