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

                var ck = new Cocktail()
                {
                    Name = "Mojito",
                    Instructions = "instructies"
                };
            context.Add(ck);
            context.SaveChanges();

            
        }

    }
}
