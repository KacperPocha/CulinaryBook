using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBook.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public string Category { get; set; }
        public string Taste { get; set; }
        public string Time { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
