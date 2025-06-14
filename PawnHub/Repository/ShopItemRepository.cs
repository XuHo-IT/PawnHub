using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ShopItemRepository
    {
        public List<ShopItem> GetItemShops() => ShopItemDAO.Instance.GetItemsShop();

        public ShopItem GetItemById(int ShopitemId) => ShopItemDAO.Instance.GetItemById(ShopitemId);
        public bool UpdateItemExpirationStatus(int shopItemId, bool isExpired) => ShopItemDAO.Instance.UpdateItemExpirationStatus(shopItemId,isExpired);
       

    }
}
