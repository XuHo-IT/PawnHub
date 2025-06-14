using BussinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class PawnContractRepository
    {
    

        public void SendToAdminForApproval(Item item) => PawnContractDAO.Instance.SendToAdminForApproval(item);

        public List<PendingItemViewModel> GetPendingItems() => PawnContractDAO.Instance.GetPendingItems();


        public void ApproveItem(Item item) => PawnContractDAO.Instance.ApproveItem(item);
       

        public void RejectItem(Item item) => PawnContractDAO.Instance.RejectItem(item);

        public List<PawnContract> GetTransaction() => PawnContractDAO.Instance.GetTransaction();
        public void RemoveItem(int contractId) => PawnContractDAO.Instance.RemoveItem(contractId);
        public List<PendingItemViewModel> GetPendingItemsPawn() => PawnContractDAO.Instance.GetPendingItemsPawn();      
        public List<PawnContract> GetAllContracts() => PawnContractDAO.Instance.GetAllContracts();

    }
}
