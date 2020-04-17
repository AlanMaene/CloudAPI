using System.Text.Json.Serialization;

namespace Model
{
    public class Bartender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        //[JsonIgnore]
        //public Cocktail cocktail { get; set; }

    }
}
