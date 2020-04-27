using Cocktail_API.Model;
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
            List<Measurements> measurements = new List<Measurements>();

            var bt = new Bartender()
            {
                Name = "Beachbum",
                Desciption = "Jeff 'Beachbum' Berry (born c. 1958[1]) is an American restaurant owner, author, and historian of tiki culture, particularly the drinks associated with the tiki theme. In addition to researching and reconstructing lost recipes, he has invented and published his own cocktail recipes.",
            };
            context.Add(bt);



            var ingr = new Ingredient()
            {
                Name = "Lemon juice"
            };
            context.Add(ingr);

            var measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 60,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Orange juice"
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 30,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Orgeat syrup"
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 15,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Light rum"
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 60,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Brandy"
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 30,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Gin"
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 15,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Cream cherry"
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 15,

            };
            context.Add(measure);
            measurements.Add(measure);

            var ck = new Cocktail()
            {
                Name = "Fog Cutter",
                Instructions = "instructies",
                Inventor = bt,
                Measurements = measurements
            };
            context.Add(ck);
            context.SaveChanges();

            measurements = new List<Measurements>();

            bt = new Bartender()
            {
                Name = "Beachbum",
                Desciption = "Jeff 'Beachbum' Berry (born c. 1958[1]) is an American restaurant owner, author, and historian of tiki culture, particularly the drinks associated with the tiki theme. In addition to researching and reconstructing lost recipes, he has invented and published his own cocktail recipes.",
            };
            context.Add(bt);

            ingr = new Ingredient()
            {
                Name = "Dark Jamaican rum"
            };
            context.Add(ingr);

             measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 30,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Rhum agricole vieux",
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 30,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Orange Curacao",
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 15,
            };
            context.Add(measure);
            measurements.Add(measure);

            measure = new Measurements()
            {
                ingredient = context.Ingredients.Where(d=> d.Name == "Orgeat syrup").ToArray()[0],
                measurements = 7.5,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Simple syrup",
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 7.5,
            };
            context.Add(measure);
            measurements.Add(measure);

            ingr = new Ingredient()
            {
                Name = "Lime juice",
            };
            context.Add(ingr);

            measure = new Measurements()
            {
                ingredient = ingr,
                measurements = 30,
            };
            context.Add(measure);
            measurements.Add(measure);

            ck = new Cocktail()
            {
                Name = "Mai Tai",
                Instructions = "instructies",
                Inventor = bt,
                Measurements = measurements
            };
            context.Add(ck);

            
            



            /*
            var cocktail = context.Cocktails.Find(1);
            cocktail.Ingredients = context.Measurements.Where(d => d.cocktail.Id == 1).Select(d => d.ingredient).ToList();
            cocktail.Ingredients = ingredients;
            */
            context.SaveChanges();
        }

    }
}
