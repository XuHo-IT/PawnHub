using BussinessObject;
using Repository;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for BillMemberWindow.xaml
    /// </summary>
    public partial class BillMemberWindow : Window
    {
        int loggedInUserId = SessionManager.UserId;
        private readonly BillRepository billRepository;

        public BillMemberWindow()
        {
            InitializeComponent();
            billRepository = new BillRepository();
            LoadUserBills();
        }

        private void LoadUserBills()
        {
            ShopItemsGrid.ItemsSource = billRepository.GetShopItemBillsByUserId(loggedInUserId);
            PawnContractsGrid.ItemsSource = billRepository.GetPawnContractBillsByUserId(loggedInUserId);
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
    }
}