using BussinessObject;

namespace DataAccessLayer
{
    public class CapitalDAO
    {
        private static CapitalDAO? instance = null;
        private static readonly object instanceLock = new object();
        private readonly PawnShopContext _context;

        public static CapitalDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CapitalDAO();
                    }
                    return instance;
                }
            }
        }

        public CapitalDAO()
        {
            _context = new PawnShopContext();
        }
        public List<CapitalInformation> GetMoneys()
        {
            var capitalInfoList = _context.CapitalInformation.ToList();

            foreach (var capital in capitalInfoList)
            {
                capital.TotalProfit = capital.TotalIncome - (capital.TotalCapital - capital.TotalExpenditure);
            }

            return capitalInfoList;
        }
        public decimal GetTotalIncome()
        {
            return _context.CapitalInformation.Sum(c => c.TotalIncome);
        }
        public decimal GetTotalExpenditure()
        {
            return _context.CapitalInformation.Sum(c => c.TotalExpenditure);
        }
        public void UpdateTotalIncome(decimal newIncome)
        {
            var capital = _context.CapitalInformation.FirstOrDefault();
            if (capital != null)
            {
                capital.TotalIncome = newIncome;
                UpdateTotalProfit(capital);
                _context.SaveChanges();
            }
        }
        private void UpdateTotalProfit(CapitalInformation capital)
        {
            capital.TotalProfit = capital.TotalIncome - (capital.TotalCapital - capital.TotalExpenditure);
        }
        public void UpdateTotalExpenditure(decimal newTotalExpenditure)
        {

            var capitalInfo = _context.CapitalInformation.FirstOrDefault();
            if (capitalInfo != null)
            {
                capitalInfo.TotalExpenditure = newTotalExpenditure;
                UpdateTotalProfit(capitalInfo);
                _context.SaveChanges();
            }

        }
    }
}
