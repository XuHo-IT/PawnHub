using BussinessObject;
using Repository;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ShopInformationWindow.xaml
    /// </summary>
    public partial class ShopInformationWindow : Window
    {
        private readonly ShopInformationRepository shopInformationRepository;
        public ShopInformationWindow()
        {
            InitializeComponent();
            shopInformationRepository = new ShopInformationRepository();
            LoadShopInformation();

        }
        private void LoadShopInformation()
        {
            // Fetch the shop information from the repository
            var shopInfo = shopInformationRepository.GetInformation();

            if (shopInfo != null) // Check if shopInfo is not null
            {
                // Bind the data to the controls
                ShopName.Text = shopInfo.ShopName; // Assuming you have a ShopName property
                Email.Text = shopInfo.EmailAddress;
                Address.Text = shopInfo.EmailAddress;
                Phone.Text = shopInfo.Telephone;
            }
            else
            {
                // Handle the case where shop information is not available
                MessageBox.Show("Shop information not found.");
            }
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
