using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cocktail_API.Model
{
    public class Measurements
    {
        [JsonIgnore]
        public int Id { get; set; }
        //public Cocktail cocktail { get; set; }
        public Ingredient ingredient { get; set; }
        public double measurements { get; set; }
    }
}
