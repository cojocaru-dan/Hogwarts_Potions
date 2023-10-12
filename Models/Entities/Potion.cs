using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Name { get; set; }
        public Student Student { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string BrewingStatus { get; set; } = "brew";
        public Recipe Recipe { get; set; }
    }
}