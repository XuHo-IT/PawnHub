using BussinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ItemRepository
    {
        public List<Item> GetItems() => ItemDAO.Instance.GetItems();
        public Item GetItemById(int itemId) => ItemDAO.Instance.GetItemById(itemId);
        

    }
}
