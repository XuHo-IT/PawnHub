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

        public List<BillViewModel> GetShopItemBillsByUserId(int userId)
        {
            var shopItemBills = from bill in _context.Bills
                                join item in _context.ShopItem on bill.ShopItemId equals item.ShopItemId
                                where bill.UserId == userId && bill.ShopItemId != null
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

            return shopItemBills.ToList();
        }

        public List<BillViewModel> GetPawnContractBillsByUserId(int userId)
        {
            var rebuyBills = from bill in _context.Bills
                             join contract in _context.PawnContracts on bill.PawnContractId equals contract.ContractId
                             join item in _context.Item on contract.ItemId equals item.ItemId
                             where bill.UserId == userId && bill.PawnContractId != null
                             select new BillViewModel
                             {
                                 BillId = bill.BillId,
                                 PawnContractId = bill.PawnContractId,
                                 UserId = bill.UserId,
                                 DateBuy = bill.DateBuy,
                                 ItemName = item.Name,
                                 ItemPrice = item.Value,
                                 ItemDescription = item.Description
                             };

            return rebuyBills.ToList();
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
