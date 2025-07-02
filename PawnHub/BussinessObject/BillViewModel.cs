namespace BussinessObject
{
    public class BillViewModel
    {
        public int BillId { get; set; }
        public int? ShopItemId { get; set; }
        public int? PawnContractId { get; set; } // Optional for rebuy
        public int UserId { get; set; }
        public DateTime DateBuy { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemDescription { get; set; }


    }
}
