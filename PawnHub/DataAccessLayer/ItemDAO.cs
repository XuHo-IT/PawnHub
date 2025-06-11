using BussinessObject;

namespace DataAccessLayer
{
    public class ItemDAO
    {
        private static ItemDAO? instance = null;
        private static readonly object instanceLock = new object();
        private readonly PawnShopContext _context;

        public static ItemDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemDAO();
                    }
                    return instance;
                }
            }
        }

        public ItemDAO()
        {
            _context = new PawnShopContext();
        }
        public List<Item> GetItems()
        {
            return _context.Item.ToList();

        }
        public Item GetItemById(int itemId)
        {

            return _context.Item.FirstOrDefault(item => item.ItemId == itemId);
        }

    }
}
