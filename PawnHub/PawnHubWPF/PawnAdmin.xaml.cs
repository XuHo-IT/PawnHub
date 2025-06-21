using BussinessObject;
using Repository;
using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class PawnAdmin : Window
    {
   
        private readonly PawnContractRepository pawnContractRepository;
        private readonly ItemRepository itemRepository;
        private readonly CapitalRepository capitalRepository;
        public PawnAdmin()
        {
            itemRepository = new ItemRepository();
            pawnContractRepository = new PawnContractRepository();
            capitalRepository = new CapitalRepository();
            InitializeComponent();
            LoadPendingItems();
        }

        private void LoadPendingItems()
        {
            PawnItemsGrid.ItemsSource = pawnContractRepository.GetPendingItemsPawn();
        }

        private void PawnItemsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PawnItemsGrid.SelectedItem as PendingItemViewModel;
            if (selectedItem != null)
            {
                var item = itemRepository.GetItemById(selectedItem.ItemId);

       
                if (capitalRepository.GetTotalExpenditure()- item.Value < 0)
                {
                    MessageBox.Show("Sorry, we are out of money. Please consider again.");
                    return; 
                }

                pawnContractRepository.ApproveItem(item);
                decimal totalExpenditure = capitalRepository.GetTotalExpenditure();
                totalExpenditure -= item.Value;
                capitalRepository.UpdateTotalExpenditure(totalExpenditure);        
                MessageBox.Show("Item accepted.");
                LoadPendingItems();
            }
            else
            {
                MessageBox.Show("Please select an item first.");
            }
        }


        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PawnItemsGrid.SelectedItem as PendingItemViewModel;
            if (selectedItem != null)
            {
                var item = itemRepository.GetItemById(selectedItem.ItemId);
                pawnContractRepository.RejectItem(item);
                MessageBox.Show("Item denied.");
                LoadPendingItems();
            }
            else
            {
                MessageBox.Show("Please select an item first.");
            }
        }


        private void RefreshPendingItems()
        {
            PawnItemsGrid.ItemsSource = null;
            PawnItemsGrid.ItemsSource = pawnContractRepository.GetPendingItems();
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
