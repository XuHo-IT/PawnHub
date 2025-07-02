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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        public MemberWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PawnMemberWindow pawnMemberWindow = new PawnMemberWindow();
            pawnMemberWindow.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BuyMemberWindow pawnMemberWindow = new BuyMemberWindow();
            pawnMemberWindow.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BillMemberWindow pawnMemberWindow = new BillMemberWindow();
            pawnMemberWindow.Show();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ReBuyMemberWindow pawnMemberWindow = new ReBuyMemberWindow();
            pawnMemberWindow.Show();
        }
        private void UserInformationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow userProfileWindow = new InformationWindow();
            userProfileWindow.Show();
        }
        private void ShopInformationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShopInformationWindow userProfileWindow = new ShopInformationWindow();
            userProfileWindow.Show();
        }
        private void SignOutMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
