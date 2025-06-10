namespace BussinessObject
{
    public class PendingItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserCID { get; set; }

    }
}
