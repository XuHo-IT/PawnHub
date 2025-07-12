using BussinessObject;
using Repository;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

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
        private readonly BillRepository billRepository;
        private readonly HttpClient httpClient = new HttpClient();
        public ReBuyMemberWindow()
        {
            pawnContractRepository = new PawnContractRepository();
            itemRepository = new ItemRepository();
            userRepository = new UserRepository();
            billRepository = new BillRepository();
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

        private async void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPawn.SelectedItem is not TransactionDetailViewModel selectedTransaction)
            {
                MessageBox.Show("Please select a transaction to buy.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to proceed with the buy?", "Confirm Purchase",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                var apiUrl = "https://localhost:7155/api/Stripe/create-checkout-session";

                var payload = new
                {
                    ItemName = selectedTransaction.ItemName,
                    Price = selectedTransaction.ItemValue
                };

                var response = await httpClient.PostAsJsonAsync(apiUrl, payload);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadFromJsonAsync<StripeResponse>();
                if (!string.IsNullOrEmpty(json?.SessionUrl) && !string.IsNullOrEmpty(json?.SessionId))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = json.SessionUrl,
                        UseShellExecute = true
                    });

                    // 🕒 Poll for up to 1 minute to wait for payment
                    bool isPaid = false;
                    for (int i = 0; i < 30; i++)
                    {
                        await Task.Delay(2000);

                        var statusResp = await httpClient.GetAsync(
                            $"https://localhost:7155/api/Stripe/check-payment-status/{json.SessionId}");

                        var status = await statusResp.Content.ReadAsStringAsync();
                        if (status == "paid")
                        {
                            isPaid = true;
                            break;
                        }
                    }

                    if (isPaid)
                    {
                        // ✅ Payment successful
                        MessageBox.Show("Payment successful!", "Stripe", MessageBoxButton.OK, MessageBoxImage.Information);

                        // 💰 Update income and bill
                        decimal totalIncome = capitalRepository.GetTotalIncome();
                        totalIncome += selectedTransaction.ItemValue;
                        capitalRepository.UpdateTotalIncome(totalIncome);

                        var bill = new Bill
                        {
                            PawnContractId = selectedTransaction.ContractId,
                            UserId = loggedInUserId,
                            DateBuy = DateTime.Now
                        };
                        billRepository.InsertBill(bill);

                        // Optionally remove contract or update status
                        // pawnContractRepository.RemoveItem(selectedTransaction.ContractId);

                        // 🧹 Update UI
                        transactionDetails.Remove(selectedTransaction);
                        dataGridPawn.ItemsSource = null;
                        dataGridPawn.ItemsSource = transactionDetails;

                        MessageBox.Show("Transaction completed successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Payment not completed.", "Stripe", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to start Stripe Checkout session.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Payment error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.CurrentUser = null;

            var loginWindow = new Login();
            loginWindow.Show();

            this.Close();
        }

        private void dataGridPawn_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }
    }
}
