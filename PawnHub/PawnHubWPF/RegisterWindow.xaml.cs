using BussinessObject;
using Repository;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class RegisterWindow : Window
    {
        private readonly UserRepository userRepository;

        public RegisterWindow()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            
            // Set default date to 18 years ago
            BirthDatePicker.SelectedDate = DateTime.Now.AddYears(-18);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate all inputs
                if (!ValidateInputs())
                {
                    return;
                }

                // Check if user already exists
                if (IsUserExists())
                {
                    return;
                }

                // Create new user
                var newUser = CreateUserFromInputs();

                // Add user to database
                bool success = userRepository.AddUser(newUser);

                if (success)
                {
                    MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.", 
                                   "Thành Công", 
                                   MessageBoxButton.OK, 
                                   MessageBoxImage.Information);
                    
                    // Close register window and return to login
                    var loginWindow = new Login();
                    loginWindow.Show();
                    this.Close();
                }
                else
                {
                    ShowError("Đăng ký thất bại. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi hệ thống: {ex.Message}");
            }
        }

        private bool ValidateInputs()
        {
            // Clear previous error
            HideError();

            // Check required fields
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                ShowError("Tên đăng nhập không được để trống.");
                UsernameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(RealNameTextBox.Text))
            {
                ShowError("Họ và tên không được để trống.");
                RealNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                ShowError("Email không được để trống.");
                EmailTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                ShowError("Mật khẩu không được để trống.");
                PasswordBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                ShowError("Xác nhận mật khẩu không được để trống.");
                ConfirmPasswordBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                ShowError("Số điện thoại không được để trống.");
                PhoneTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(AddressTextBox.Text))
            {
                ShowError("Địa chỉ không được để trống.");
                AddressTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(CIDTextBox.Text))
            {
                ShowError("CCCD/CMND không được để trống.");
                CIDTextBox.Focus();
                return false;
            }

            if (BirthDatePicker.SelectedDate == null)
            {
                ShowError("Ngày sinh không được để trống.");
                BirthDatePicker.Focus();
                return false;
            }

            // Validate username length
            if (UsernameTextBox.Text.Length < 3 || UsernameTextBox.Text.Length > 50)
            {
                ShowError("Tên đăng nhập phải từ 3-50 ký tự.");
                UsernameTextBox.Focus();
                return false;
            }

            // Validate email format
            if (!IsValidEmail(EmailTextBox.Text))
            {
                ShowError("Định dạng email không hợp lệ.");
                EmailTextBox.Focus();
                return false;
            }

            // Validate password
            if (PasswordBox.Password.Length < 8)
            {
                ShowError("Mật khẩu phải có ít nhất 8 ký tự.");
                PasswordBox.Focus();
                return false;
            }

            // Validate password confirmation
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ShowError("Mật khẩu xác nhận không khớp.");
                ConfirmPasswordBox.Focus();
                return false;
            }

            // Validate phone format
            if (!IsValidPhone(PhoneTextBox.Text))
            {
                ShowError("Số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng.");
                PhoneTextBox.Focus();
                return false;
            }

            // Validate age (must be 18+)
            if (!IsValidAge(BirthDatePicker.SelectedDate.Value))
            {
                ShowError("Bạn phải đủ 18 tuổi để đăng ký.");
                BirthDatePicker.Focus();
                return false;
            }

            // Validate CID format
            if (!IsValidCID(CIDTextBox.Text))
            {
                ShowError("CCCD/CMND phải có 9-12 số.");
                CIDTextBox.Focus();
                return false;
            }

            return true;
        }

        private bool IsUserExists()
        {
            // Check if email already exists
            var existingUserByEmail = userRepository.GetUserByEmail(EmailTextBox.Text);
            if (existingUserByEmail != null)
            {
                ShowError("Email này đã được sử dụng. Vui lòng chọn email khác.");
                EmailTextBox.Focus();
                return true;
            }

            // Check if username already exists
            var existingUserByUsername = userRepository.GetUserByUsername(UsernameTextBox.Text);
            if (existingUserByUsername != null)
            {
                ShowError("Tên đăng nhập này đã tồn tại. Vui lòng chọn tên khác.");
                UsernameTextBox.Focus();
                return true;
            }

            return false;
        }

        private User CreateUserFromInputs()
        {
            var selectedGender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Nam";
            
            return new User
            {
                UserName = UsernameTextBox.Text.Trim(),
                UserRealName = RealNameTextBox.Text.Trim(),
                EmailAddress = EmailTextBox.Text.Trim().ToLower(),
                Password = PasswordBox.Password,
                Telephone = PhoneTextBox.Text.Trim(),
                Gender = selectedGender,
                Dob = BirthDatePicker.SelectedDate ?? DateTime.Now.AddYears(-18),
                Address = AddressTextBox.Text.Trim(),
                CID = CIDTextBox.Text.Trim(),
                UserRole = 2, // Default to Member role
                Items = new List<Item>(),
                PawnContracts = new List<PawnContract>(),
                Bills = new List<Bill>()
            };
        }

        #region Validation Helper Methods

        private bool IsValidEmail(string email)
        {
            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            // Remove spaces and dashes
            string cleanPhone = phone.Replace(" ", "").Replace("-", "");
            
            // Check if it's all digits and has valid length
            return Regex.IsMatch(cleanPhone, @"^\d{10,11}$");
        }

        private bool IsValidAge(DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.AddYears(age) > DateTime.Now)
                age--;
                
            return age >= 18;
        }

        private bool IsValidCID(string cid)
        {
            // CID should be 9-12 digits
            return Regex.IsMatch(cid.Replace(" ", ""), @"^\d{9,12}$");
        }

        #endregion

        #region UI Helper Methods

        private void ShowError(string message)
        {
            ErrorMessageTextBlock.Text = message;
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Event Handlers

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn hủy đăng ký?", 
                                       "Xác Nhận", 
                                       MessageBoxButton.YesNo, 
                                       MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }

        #endregion
    }
} 