using BussinessObject;
using PawnHubWPF;
using Repository;
using System.Windows;

namespace WpfApp
{
    public partial class Login : Window
    {
        private readonly UserRepository userRepository;

        public Login()
        {
            InitializeComponent();
            userRepository = new UserRepository();

        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;


            // Check if the username and password are valid
            var user = userRepository.GetUserByEmailAndPassword(email, password);
            if (user != null)
            {
                SessionManager.UserId = user.UserID;
                if (user.UserRole == 1)
                {
                    MessageBox.Show("Welcome Admin!");
                    var adminWindow = new AdminWindow(); // Replace with your admin window
                    adminWindow.Show();
                }
                else if (user.UserRole == 2)
                {
                    MessageBox.Show("Welcome Member!");
                    var memberWindow = new MemberWindow(); // Replace with your member window
                    memberWindow.Show();
                }
                else
                {
                    MessageBox.Show("Invalid role.");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            EmailTextBox.Clear();
            PasswordBox.Clear();
        }
    }
}
