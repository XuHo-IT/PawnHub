﻿using BussinessObject;
using PawnHubWPF;
using System.Windows;

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
            ChatAI chatAI = new ChatAI();
            chatAI.Show();
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
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.CurrentUser = null;

            var loginWindow = new Login();
            loginWindow.Show();

            this.Close();
        }
    }
}
