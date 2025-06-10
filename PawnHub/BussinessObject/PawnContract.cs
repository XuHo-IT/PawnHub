using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class PawnContract
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Loan amount must be greater than 0.")]
        public decimal LoanAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime ContractDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
    }
}
