using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class DBInitializer
    {

        public static void Initialize(RecipesContext context)
        {
            context.Database.EnsureCreated();
            /*
            var bt = new Bartender()
            {
                Name = "Beachbum",
                Desciption = "Jeff 'Beachbum' Berry (born c. 1958[1]) is an American restaurant owner, author, and historian of tiki culture, particularly the drinks associated with the tiki theme. In addition to researching and reconstructing lost recipes, he has invented and published his own cocktail recipes.",
            };
            context.Add(bt);

            var ck = new Cocktail()
            {
                Name = "Coronado Luau Special",
                Instructions = "instructies",
                Inventor = context.Bartenders.Find(2),
            };
            context.Add(ck);

            context.SaveChanges();
            */

            
        }

    }
}
