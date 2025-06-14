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
    public class BillRepository
    {
        public List<Bill> GetItems() => BillDAO.Instance.GetItems();

        public bool InsertBill(Bill bill) => BillDAO.Instance.InsertBill(bill);
        public List<BillViewModel> GetBillsByUserId(int userId) => BillDAO.Instance.GetBillsByUserId(userId);

        public List<BillViewModel> GetBills() => BillDAO.Instance.GetBills();




    }
}
