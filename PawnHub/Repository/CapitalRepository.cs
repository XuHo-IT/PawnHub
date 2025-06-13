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
    public class CapitalRepository
    {
        public List<CapitalInformation> GetMoneys() => CapitalDAO.Instance.GetMoneys();
        public void UpdateTotalIncome(decimal newTotalIncome) => CapitalDAO.Instance.UpdateTotalIncome(newTotalIncome);
        public decimal GetTotalIncome() => CapitalDAO.Instance.GetTotalIncome();
        public decimal GetTotalExpenditure() => CapitalDAO.Instance.GetTotalExpenditure();
        public void UpdateTotalExpenditure(decimal newTotalExpenditure) => CapitalDAO.Instance.UpdateTotalExpenditure(newTotalExpenditure);
       
    }
}
