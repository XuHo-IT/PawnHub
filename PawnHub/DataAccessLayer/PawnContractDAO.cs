using BussinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PawnContractDAO
    {
        private static PawnContractDAO? instance = null;
        private static readonly object instanceLock = new object();
        private readonly PawnShopContext _context;

        public static PawnContractDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PawnContractDAO();
                    }
                    return instance;
                }
            }
        }
        public List<PendingItemViewModel> GetPendingItems()
        {
            return (from item in _context.Item
                    join user in _context.User on item.UserId equals user.UserID
                    where item.IsApproved == false
                    select new PendingItemViewModel
                    {
                        ItemId = item.ItemId,
                        ItemName = item.Name,
                        Description = item.Description,
                        Value = item.Value * item.Interest,
                        ExpirationDate = item.ExpirationDate,
                        Status = item.Status,
                        UserId = user.UserID,
                        UserName = user.UserRealName,
                        UserEmail = user.EmailAddress,
                        UserPhone = user.Telephone,
                        UserCID = user.CID,
                    }).ToList();
        }
        public List<PendingItemViewModel> GetPendingItemsPawn()
        {
            return (from item in _context.Item
                    join user in _context.User on item.UserId equals user.UserID
                    where item.IsApproved == false
                    select new PendingItemViewModel
                    {
                        ItemId = item.ItemId,
                        ItemName = item.Name,
                        Description = item.Description,
                        Value = item.Value,
                        ExpirationDate = item.ExpirationDate,
                        Status = item.Status,
                        UserId = user.UserID,
                        UserName = user.UserRealName,
                        UserEmail = user.EmailAddress,
                        UserPhone = user.Telephone,
                        UserCID = user.CID,
                    }).ToList();
        }
        public PawnContractDAO()
        {
            _context = new PawnShopContext();
        }

        public List<PawnContract> GetTransaction()
        {
            return _context.PawnContracts.ToList();

        }

        public void AddContract(PawnContract contract)
        {
            _context.PawnContracts.Add(contract);
            _context.SaveChanges();
        }



        public PawnContract? GetContractById(int contractId)
        {
            return _context.PawnContracts.Find(contractId);
        }
        public void SendToAdminForApproval(Item item)
        {
            _context.Item.Add(item);
            _context.SaveChanges();
        }


        public void ApproveItem(Item item)
        {
            using (var context = new PawnShopContext())
            {
                var dbItem = context.Item.FirstOrDefault(i => i.ItemId == item.ItemId);
                if (dbItem != null)
                {
                    dbItem.IsApproved = true;

                    var contract = new PawnContract
                    {
                        ItemId = dbItem.ItemId,
                        UserId = dbItem.UserId,
                        LoanAmount = dbItem.Value,
                        ContractDate = DateTime.Now,
                        ExpirationDate = dbItem.ExpirationDate
                    };

                    context.PawnContracts.Add(contract);
                    context.SaveChanges();
                }
            }
        }


        public void RemoveItem(int contractId)
        {
            using (var context = new PawnShopContext())
            {
                var contract = context.PawnContracts.SingleOrDefault(c => c.ContractId == contractId);
                if (contract != null)
                {
                    context.PawnContracts.Remove(contract);
                    context.SaveChanges();
                }
            }
        }
        public void RejectItem(Item item)
        {
            _context.Item.Remove(item);
            _context.SaveChanges();
        }
        public List<PawnContract> GetAllContracts()
        {
            return _context.PawnContracts.Include(pc => pc.Item)
                      .ToList();
        }
    }
}
