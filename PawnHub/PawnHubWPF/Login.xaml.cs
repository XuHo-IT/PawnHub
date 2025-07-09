using BussinessObject;
using BussinessObject.Services;
using PawnHubWPF;
using Repository;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class Login : Window
    {
        private readonly UserRepository userRepository;
        private readonly GoogleAuthService googleAuthService;

        public Login()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            googleAuthService = new GoogleAuthService();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.");
                return;
            }

            // Check if the username and password are valid
            var user = userRepository.GetUserByEmailAndPassword(email, password);
            if (user != null)
            {
                LoginSuccess(user);
            }
            else
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng.");
            }
        }

        private async void GoogleLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusLabel.Text = "Đang mở trình duyệt để đăng nhập Google...";
                GoogleLoginButton.IsEnabled = false;

                // Tạo Google OAuth URL
                string clientId = googleAuthService.GetClientId();
                string redirectUri = "http://localhost:8080/oauth/callback";
                string scope = "openid email profile";
                string state = Guid.NewGuid().ToString();

                string authUrl = $"https://accounts.google.com/o/oauth2/auth?" +
                    $"client_id={clientId}&" +
                    $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                    $"scope={Uri.EscapeDataString(scope)}&" +
                    $"response_type=code&" +
                    $"state={state}";

                // Mở trình duyệt
                Process.Start(new ProcessStartInfo
                {
                    FileName = authUrl,
                    UseShellExecute = true
                });

                // Bắt đầu HTTP listener để nhận callback
                await HandleGoogleCallback(redirectUri);
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Lỗi: {ex.Message}";
                GoogleLoginButton.IsEnabled = true;
            }
        }

        private async Task HandleGoogleCallback(string redirectUri)
        {
            try
            {
                using var httpClient = new HttpClient();

                // Tạo một HTTP listener đơn giản
                var listener = new System.Net.HttpListener();
                listener.Prefixes.Add("http://localhost:8080/");
                listener.Start();

                StatusLabel.Text = "Đang chờ phản hồi từ Google...";

                // Chờ request callback
                var context = await listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;

                // Lấy authorization code từ query string
                string authCode = request.QueryString["code"];

                if (string.IsNullOrEmpty(authCode))
                {
                    throw new Exception("Không nhận được mã xác thực từ Google");
                }

                // Gửi response về trình duyệt
                string responseString = @"<html>
<head>
    <meta charset='utf-8'>
    <title>Đăng nhập thành công</title>
</head>
<body>
    <h1>Đăng nhập thành công!</h1>
    <p>Bạn có thể đóng cửa sổ này và quay lại ứng dụng.</p>
    <script>
        setTimeout(function() {
            window.close();
        }, 2000);
    </script>
</body>
</html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.Close();
                listener.Stop();

                // Exchange authorization code for access token
                await ExchangeCodeForToken(authCode);
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Lỗi trong quá trình xác thực: {ex.Message}";
                GoogleLoginButton.IsEnabled = true;
            }
        }

        private async Task ExchangeCodeForToken(string authCode)
        {
            try
            {
                using var httpClient = new HttpClient();

                var tokenRequest = new Dictionary<string, string>
                {
                    {"client_id", googleAuthService.GetClientId()},
                    {"client_secret", googleAuthService.GetClientSecret()},
                    {"code", authCode},
                    {"grant_type", "authorization_code"},
                    {"redirect_uri", "http://localhost:8080/oauth/callback"}
                };

                var content = new FormUrlEncodedContent(tokenRequest);
                var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Lỗi khi lấy token: {responseContent}");
                }

                // Parse token response (bạn có thể dùng Newtonsoft.Json)
                dynamic tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                string accessToken = tokenResponse.access_token;
                string idToken = tokenResponse.id_token;

                // Validate ID token và lấy thông tin user
                var payload = await googleAuthService.ValidateGoogleTokenAsync(idToken);

                if (payload != null)
                {
                    await HandleGoogleUser(payload);
                }
                else
                {
                    throw new Exception("Token không hợp lệ");
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Lỗi khi xử lý token: {ex.Message}";
                GoogleLoginButton.IsEnabled = true;
            }
        }

        private async Task HandleGoogleUser(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        {
            try
            {
                StatusLabel.Text = "Đang xử lý thông tin người dùng...";

                // Tìm user theo GoogleId hoặc Email
                var existingUser = userRepository.GetUserByGoogleId(payload.Subject)
                                 ?? userRepository.GetUserByEmail(payload.Email);

                User user;

                if (existingUser != null)
                {
                    // User đã tồn tại, cập nhật GoogleId nếu cần
                    if (string.IsNullOrEmpty(existingUser.GoogleId))
                    {
                        existingUser.GoogleId = payload.Subject;
                        existingUser.IsGoogleAccount = true;
                        existingUser.ProfilePicture = payload.Picture;
                        userRepository.UpdateUser(existingUser);
                    }
                    user = existingUser;
                    StatusLabel.Text = "Đăng nhập thành công với tài khoản hiện có!";
                }
                else
                {
                    // Tạo user mới
                    user = userRepository.CreateGoogleUser(
                        payload.Subject,
                        payload.Email,
                        payload.Name,
                        payload.Picture
                    );
                    StatusLabel.Text = "Đã tạo tài khoản mới và đăng nhập thành công!";
                }

                // Đăng nhập thành công
                await Task.Delay(1000); // Hiển thị thông báo một chút
                Application.Current.Dispatcher.Invoke(() => LoginSuccess(user));
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Lỗi khi xử lý người dùng: {ex.Message}";
                GoogleLoginButton.IsEnabled = true;
            }
        }

        private void LoginSuccess(User user)
        {
            SessionManager.UserId = user.UserID;

            if (user.UserRole == 1)
            {
                MessageBox.Show($"Chào mừng Admin: {user.UserRealName}!");
                var adminWindow = new AdminWindow();
                adminWindow.Show();
            }
            else if (user.UserRole == 2)
            {
                string loginMethod = user.IsGoogleAccount ? "Google" : "tài khoản thường";
                MessageBox.Show($"Chào mừng khách hàng: {user.UserRealName}!\n(Đăng nhập bằng {loginMethod})");
                var memberWindow = new MemberWindow();
                memberWindow.Show();
            }
            else
            {
                MessageBox.Show("Vai trò người dùng không hợp lệ.");
                return;
            }

            this.Close();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            EmailTextBox.Clear();
            PasswordBox.Clear();
            StatusLabel.Text = "";
            GoogleLoginButton.IsEnabled = true;
        }

        // Phiên bản đơn giản hơn cho test (nếu cần)
        private async void SimpleGoogleLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo dialog để người dùng nhập Google ID Token thủ công
                var dialog = new GoogleTokenDialog();
                if (dialog.ShowDialog() == true)
                {
                    string idToken = dialog.IdToken;

                    StatusLabel.Text = "Đang xác thực Google token...";

                    // Validate token
                    var payload = await googleAuthService.ValidateGoogleTokenAsync(idToken);

                    if (payload != null)
                    {
                        await HandleGoogleUser(payload);
                    }
                    else
                    {
                        MessageBox.Show("Token Google không hợp lệ.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }

    // Dialog đơn giản để nhập token (dùng cho test)
    public partial class GoogleTokenDialog : Window
    {
        public string IdToken { get; private set; }

        public GoogleTokenDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Title = "Google ID Token";
            Height = 200;
            Width = 400;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var grid = new Grid();
            grid.Margin = new Thickness(10);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var label = new TextBlock
            {
                Text = "Nhập Google ID Token:",
                Margin = new Thickness(0, 0, 0, 10)
            };
            Grid.SetRow(label, 0);
            grid.Children.Add(label);

            TokenTextBox = new TextBox
            {
                TextWrapping = TextWrapping.Wrap,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            Grid.SetRow(TokenTextBox, 1);
            grid.Children.Add(TokenTextBox);

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 10, 0, 0)
            };

            var okButton = new Button
            {
                Content = "OK",
                Width = 75,
                Margin = new Thickness(0, 0, 10, 0)
            };
            okButton.Click += OkButton_Click;
            buttonPanel.Children.Add(okButton);

            var cancelButton = new Button
            {
                Content = "Cancel",
                Width = 75
            };
            cancelButton.Click += CancelButton_Click;
            buttonPanel.Children.Add(cancelButton);

            Grid.SetRow(buttonPanel, 2);
            grid.Children.Add(buttonPanel);

            Content = grid;
        }

        private TextBox TokenTextBox;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            IdToken = TokenTextBox.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
