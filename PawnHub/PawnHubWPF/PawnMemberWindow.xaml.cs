using BussinessObject;
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


    }
}
