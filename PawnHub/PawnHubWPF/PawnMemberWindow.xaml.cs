using BussinessObject;
using Repository;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PawnMemberWindow.xaml
    /// </summary>
    public partial class PawnMemberWindow : Window
    {
        int loggedInUserId = SessionManager.UserId;
        private readonly PawnContractRepository pawnContractRepository;
        public PawnMemberWindow()
        {
            pawnContractRepository = new PawnContractRepository();
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void SubmitItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if the expiration date is selected
            if (ExpirationDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select an expiration date.");
                return;
            }

            // Collect item details
            var item = new Item
            {
                Name = ItemNameTextBox.Text,
                Description = DescriptionTextBox.Text,
                Value = decimal.Parse(ValueTextBox.Text),
                Status = StatusTextBox.Text,
                ExpirationDate = ExpirationDatePicker.SelectedDate.Value, // Get the date from the DatePicker
                Interest = decimal.Parse(InterstTextBox.Text),
                UserId = SessionManager.UserId,
            };

            // Call the static method directly on the class
            pawnContractRepository.SendToAdminForApproval(item);

            MessageBox.Show("Item submitted for admin approval.");
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
