using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class CapitalInformation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Total capital cannot be negative.")]
        public decimal TotalCapital { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total income cannot be negative.")]
        public decimal TotalIncome { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total expenditure cannot be negative.")]
        public decimal TotalExpenditure { get; set; }
        public decimal TotalProfit { get; set; }


    }
}
