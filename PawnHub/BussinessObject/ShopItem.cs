using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class ShopItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopItemId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        public bool IsExpired { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
