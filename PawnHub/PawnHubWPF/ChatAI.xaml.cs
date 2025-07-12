using BussinessObject;
using BussinessObject.AI;
using Repository.AIService;
using System.Windows;
using WpfApp;

namespace PawnHubWPF
{
    /// <summary>
    /// Interaction logic for ChatAI.xaml
    /// </summary>
    public partial class ChatAI : Window
    {
        private readonly OllamaChatService _chatService;
        private List<string> _context;
        private readonly ChatModel _chatModel = new();

        public ChatAI()
        {
            InitializeComponent();
            _chatService = new OllamaChatService();
            _context = new List<string>();
        }



        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userMessage)) return;

            AppendMessage($"You: {userMessage}");

            string reply = await _chatService.ChatOllamaAsync(_chatModel, userMessage);

            AppendMessage($"Lusi: {reply}");

            UserInput.Text = "";
            UserInput.Focus();
        }


        private void AppendMessage(string message)
        {
            ChatDisplay.Text += message + "\n\n";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.CurrentUser = null;

            var loginWindow = new Login();
            loginWindow.Show();

            this.Close();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainMenu = new MemberWindow();
            mainMenu.Show();
            this.Close();
        }
    }
}
