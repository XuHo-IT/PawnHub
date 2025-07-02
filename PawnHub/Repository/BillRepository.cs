using BussinessObject;
using DataAccessLayer;

namespace Repository
{
    public class BillRepository
    {
        public List<Bill> GetItems() => BillDAO.Instance.GetItems();

        public bool InsertBill(Bill bill) => BillDAO.Instance.InsertBill(bill);
        public List<BillViewModel> GetShopItemBillsByUserId(int userId) => BillDAO.Instance.GetShopItemBillsByUserId(userId);
        public List<BillViewModel> GetPawnContractBillsByUserId(int userId) => BillDAO.Instance.GetPawnContractBillsByUserId(userId);


        public List<BillViewModel> GetBills() => BillDAO.Instance.GetBills();




    }
}
