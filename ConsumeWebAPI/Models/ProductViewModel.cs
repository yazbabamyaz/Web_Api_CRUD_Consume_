using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]        
        public float Price { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int Qty { get; set; }
    }
}
