using System.Windows;
using BussinessObject;
using DataAccessLayer;
using Repository;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for BuyMemberWindow.xaml
    /// </summary>
    public partial class BuyMemberWindow : Window
    {
        int loggedInUserId = SessionManager.UserId;
        private readonly ShopItemRepository shopItemRepository;
        private readonly BillRepository billRepository;
        private readonly CapitalRepository capitalRepository;


        public BuyMemberWindow()
        {
            InitializeComponent();
            shopItemRepository = new ShopItemRepository();
            PawnItemsGrid.ItemsSource = shopItemRepository.GetItemShops();
            billRepository = new BillRepository();
            capitalRepository = new CapitalRepository();
        }
        private void PawnItemsGrid_SelectionChanged()
        {

        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (PawnItemsGrid.SelectedItem is ShopItem selectedItem)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to buy '{selectedItem.Name}' for {selectedItem.Price:C}?",
                    "Confirm Purchase", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    decimal totalIncome = capitalRepository.GetTotalIncome();
                    totalIncome += selectedItem.Price;
                    capitalRepository.UpdateTotalIncome(totalIncome);
                    Bill bill = new Bill
                    {
                        ShopItemId = selectedItem.ShopItemId,
                        UserId = loggedInUserId,
                        DateBuy = DateTime.Now
                    };


                    if (billRepository.InsertBill(bill))
                    {

                        if (shopItemRepository.UpdateItemExpirationStatus(selectedItem.ShopItemId, false))
                        {
                            MessageBox.Show("Purchase successful! Item is now available.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while updating the item status.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                        PawnItemsGrid.ItemsSource = shopItemRepository.GetItemShops();
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while processing the purchase.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to buy.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
