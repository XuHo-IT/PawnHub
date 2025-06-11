using BussinessObject;

namespace DataAccessLayer
{
    public class BillDAO
    {
        private static BillDAO? instance = null;
        private static readonly object instanceLock = new object();
        private readonly PawnShopContext _context;

        public static BillDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BillDAO();
                    }
                    return instance;
                }
            }
        }

        public BillDAO()
        {
            _context = new PawnShopContext();
        }
        public List<Bill> GetItems()
        {
            return _context.Bills.ToList();

        }
        public List<BillViewModel> GetBillsByUserId(int userId)
        {
            var billsWithItems = from bill in _context.Bills
                                 join item in _context.ShopItem on bill.ShopItemId equals item.ShopItemId
                                 where bill.UserId == userId
                                 select new BillViewModel
                                 {
                                     BillId = bill.BillId,
                                     ShopItemId = bill.ShopItemId,
                                     UserId = bill.UserId,
                                     DateBuy = bill.DateBuy,
                                     ItemName = item.Name,
                                     ItemPrice = item.Price,
                                     ItemDescription = item.Description
                                 };

            return billsWithItems.ToList();
        }
        public List<BillViewModel> GetBills()
        {
            var billsWithItems = from bill in _context.Bills
                                 join item in _context.ShopItem on bill.ShopItemId equals item.ShopItemId
                                 select new BillViewModel
                                 {
                                     BillId = bill.BillId,
                                     ShopItemId = bill.ShopItemId,
                                     UserId = bill.UserId,
                                     DateBuy = bill.DateBuy,
                                     ItemName = item.Name,
                                     ItemPrice = item.Price,
                                     ItemDescription = item.Description
                                 };

            return billsWithItems.ToList();
        }
        public bool InsertBill(Bill bill)
        {
            try
            {
                _context.Bills.Add(bill);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error inserting bill: {ex.Message}");
                return false;
            }
        }


    }
}
