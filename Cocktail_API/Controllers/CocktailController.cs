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
        [Authorize]
        [HttpGet]
        public IQueryable getCocktails(string name, string sort, string dir = "asc")
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



            return query;
        }
        
        //[HttpGet("auth/google")]
        //[ProducesDefaultResponseType]
        //public async Task<string> GoogleLogin(string idToken)
        //{
        //    try
        //    {
        //        var validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken, new ValidationSettings
        //        {
        //            Audience = new[] { "998171199839-2061ud931cfaqgckitsfimod47c8nkhn.apps.googleusercontent.com" }
        //        });
        //        if (validPayload == null)
        //        {
        //            return "No valid Token";
        //        }
        //        else
        //        {
        //            return "Valid token";
                    
        //        }
        //    }
        //    catch
        //    {
        //        return "No valid Token";
        //    }
            
            
            
        //}



        // How to generate a JWT token is not in this post's scope

        /*
        public async Task<User> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        {
            // Login already linked to a user
            var user = await _userManager.FindByLoginAsync(provider, key);
            if (user != null)
                return user;

            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // No user exists with this email address, we create a new one
                user = new User
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                await _userManager.CreateAsync(user);
            }

            // Link the user to this login
            var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
            var result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
                return user;

            _logger.LogError("Failed add a user linked to a login.");
            _logger.LogError(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            return null;
        }

        // How to generate a JWT token is not in this post's scope
        public async Task<string> GenerateToken(User user)
        {
            var claims = await GetUserClaims(user);
            return GenerateToken(user, claims);
        }

    }*/
    }
}