using System.Collections.Generic;

namespace Model
{
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public ICollection<Bartender> Inventor { get; set; }
    }
}
