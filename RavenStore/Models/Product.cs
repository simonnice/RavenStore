using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RavenStore.Models
{
 
    public class Product
    {
        [StringLength(20)]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 450)]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }

        public string Image { get; set; }
    }
}
