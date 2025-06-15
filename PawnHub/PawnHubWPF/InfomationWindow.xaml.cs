using BussinessObject;
using Repository;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        private readonly int loggedInUserId = SessionManager.UserId;
        private readonly UserRepository userRepository;
        public InformationWindow()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            LoadInformation();
        }
        private void LoadInformation()
        {
            // Fetch the user information from the repository
            var userInfo = userRepository.GetUserById(loggedInUserId);

            if (userInfo != null) // Check if userInfo is not null
            {
                Username.Text = userInfo.UserName;
                Password.Text = userInfo.Password; // Make sure to use Password.Password
                RealName.Text = userInfo.UserRealName;
                Email.Text = userInfo.EmailAddress;
                Address.Text = userInfo.Address;
                Phone.Text = userInfo.Telephone;
                CID.Text = userInfo.CID;

                // Set the selected gender in the ComboBox
                GenderComboBox.SelectedItem = GenderComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == userInfo.Gender);

                // Set the date of birth
                BirthDate.SelectedDate = userInfo.Dob;
            }
            else
            {
                // Handle the case where user information is not available
                MessageBox.Show("User information not found.");
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // Update user information based on the input fields
            var updatedUser = new User
            {
                UserID = loggedInUserId,
                UserName = Username.Text,
                Password = Password.Text, // Ensure to get the password correctly
                UserRealName = RealName.Text,
                EmailAddress = Email.Text,
                Address = Address.Text,
                Telephone = Phone.Text,
                CID = CID.Text,
                Gender = GenderComboBox.SelectedItem != null
                    ? (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                    : string.Empty,
                Dob = BirthDate.SelectedDate
            };

            // Call the repository method to update user information
            bool isUpdated = userRepository.UpdateUser(updatedUser);

            if (isUpdated)
            {
                MessageBox.Show("Information updated successfully.");
                this.Close(); // Optionally close the window after update
            }
            else
            {
                MessageBox.Show("Failed to update information. Please try again.");
            }
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window
        }


    }
}
