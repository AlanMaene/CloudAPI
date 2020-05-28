using Cocktail_API.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model
{
    public class Cocktail
    {
       
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string Instructions { get; set; }
    
        public Bartender Inventor { get; set; }
        public List<Measurements> Measurements { get; set; }
    }
}
