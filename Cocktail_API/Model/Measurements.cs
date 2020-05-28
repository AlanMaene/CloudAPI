using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cocktail_API.Model
{
    public class Measurements
    {
        [JsonIgnore]
        public int Id { get; set; }
        public Ingredient ingredient { get; set; }
        [StringLength(40)]
        [Required]
        public String measurements { get; set; }
    }
}
