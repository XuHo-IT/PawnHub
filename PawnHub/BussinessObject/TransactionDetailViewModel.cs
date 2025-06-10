namespace BussinessObject
{
    public class TransactionDetailViewModel
    {
        public int ContractId { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ItemName { get; set; }
        public decimal ItemValue { get; set; }
        public string Description { get; set; }
        public string UserRealName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CID { get; set; }
    }
}
