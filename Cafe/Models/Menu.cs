using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public double  Price { get; set; }
        public int CatagoryId { get; set; }
        [ForeignKey("CatagoryId")]
        public Catagory Catagory { get; set; }
    }
}
