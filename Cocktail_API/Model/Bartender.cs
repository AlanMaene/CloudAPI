using System.Text.Json.Serialization;

namespace Model
{
    public class Bartender
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Desciption { get; set; }
        //[JsonIgnore]
        //public Cocktail cocktail { get; set; }

    }
}
