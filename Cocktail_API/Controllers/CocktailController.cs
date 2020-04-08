using Microsoft.AspNetCore.Mvc;

namespace Cocktail_API.Controllers
{
    [Route("api/v1")]
    public class CocktailController : Controller
    {
        [Route("allCocktails")]
        [HttpGet]
        public IActionResult getRecipe()
        {
            return Content("Mojito");
        }
    }
}
