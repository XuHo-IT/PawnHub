using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

    }
}
