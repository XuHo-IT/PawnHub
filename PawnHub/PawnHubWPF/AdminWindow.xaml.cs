﻿using System.Windows;

namespace WpfApp
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PawnAdmin pawnAdminWindow = new PawnAdmin();
            pawnAdminWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           StatisticAdminWindow statisticAdminWindow = new StatisticAdminWindow();
            statisticAdminWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Liquidate liquidateWindow = new Liquidate();
            liquidateWindow.Show();
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


    }
}
