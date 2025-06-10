using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }

        [ForeignKey("ShopItem")]
        public int ShopItemId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBuy { get; set; }

        public ShopItem ShopItem { get; set; }
        public User User { get; set; }

    }
}
