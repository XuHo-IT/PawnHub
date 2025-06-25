using System.Windows;
using BussinessObject;
using Repository;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ReBuyMemberWindow.xaml
    /// </summary>
    public partial class ReBuyMemberWindow : Window
    {
        decimal TotalIncome = new CapitalInformation().TotalIncome;
        private readonly int loggedInUserId = SessionManager.UserId;
        private readonly PawnContractRepository pawnContractRepository;
        private readonly ItemRepository itemRepository;
        private readonly UserRepository userRepository;
        private List<TransactionDetailViewModel> transactionDetails;
        private readonly CapitalRepository capitalRepository;

        public ReBuyMemberWindow()
        {
            pawnContractRepository = new PawnContractRepository();
            itemRepository = new ItemRepository();
            userRepository = new UserRepository();
            capitalRepository = new CapitalRepository();

            InitializeComponent();

            var transactions = pawnContractRepository.GetTransaction()
                                .Where(t => t.UserId == loggedInUserId);

            transactionDetails = new List<TransactionDetailViewModel>();

            foreach (var transaction in transactions)
            {
                var item = itemRepository.GetItemById(transaction.ItemId);
                var user = userRepository.GetUserById(transaction.UserId);
                var today = DateTime.Now;
                var daysElapsed = (today - transaction.ContractDate).Days;

                if (item != null && user != null)
                {

                    transactionDetails.Add(new TransactionDetailViewModel
                    {
                        ContractId = transaction.ContractId,
                        LoanAmount = transaction.LoanAmount,
                        ContractDate = transaction.ContractDate,
                        ExpirationDate = transaction.ExpirationDate,
                        ItemName = item.Name,
                        ItemValue = item.Value + (item.Value * item.Interest * daysElapsed),
                        Description = item.Description,
                        UserRealName = user.UserRealName,
                        UserPhone = user.Telephone,
                        UserEmail = user.EmailAddress,
                        Address = user.Address,
                        CID = user.CID
                    });
                }
            }

            // Bind the view model list to the DataGrid
            dataGridPawn.ItemsSource = transactionDetails;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTransaction = dataGridPawn.SelectedItem as TransactionDetailViewModel;

            if (selectedTransaction == null)
            {
                MessageBox.Show("Please select a transaction to buy.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to proceed with the buy?", "Confirm Purchase", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                decimal totalIncome = capitalRepository.GetTotalIncome();
                totalIncome += selectedTransaction.ItemValue;
                capitalRepository.UpdateTotalIncome(totalIncome);
                pawnContractRepository.RemoveItem(selectedTransaction.ContractId);


                transactionDetails.Remove(selectedTransaction);
                dataGridPawn.ItemsSource = null;
                dataGridPawn.ItemsSource = transactionDetails;

                MessageBox.Show("Transaction completed successfully.");
            }
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
