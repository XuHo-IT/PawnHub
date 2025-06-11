using BussinessObject;

namespace DataAccessLayer
{
    public class ShopItemDAO
    {
        private static ShopItemDAO? instance = null;
        private static readonly object instanceLock = new object();
        private readonly PawnShopContext _context;

        public static ShopItemDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShopItemDAO();
                    }
                    return instance;
                }
            }
        }

        public ShopItemDAO()
        {
            _context = new PawnShopContext();
        }
        public List<ShopItem> GetItemsShop()
        {
            return _context.ShopItem
                .Where(item => item.IsExpired)
                .ToList();
        }
        public ShopItem GetItemById(int ShopitemId)
        {

            return _context.ShopItem.FirstOrDefault(ShopItem => ShopItem.ShopItemId == ShopitemId);
        }
        public bool UpdateItemExpirationStatus(int shopItemId, bool isExpired)
        {
            var item = _context.ShopItem.Find(shopItemId);
            if (item != null)
            {
                item.IsExpired = isExpired;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
