using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        [JsonIgnore]
        public Bartender Inventor { get; set; }
    }
}
