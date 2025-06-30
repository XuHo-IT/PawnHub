using BussinessObject;
using Repository;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Liquidate.xaml
    /// </summary>
    public partial class Liquidate : Window
    {
        private readonly PawnContractRepository pawnContractRepository;
        private readonly ItemRepository items;

        public Liquidate()
        {
            InitializeComponent();
            pawnContractRepository = new PawnContractRepository();
            items = new ItemRepository();
            LoadPawnContracts();

        }

        private void LoadPawnContracts()
        {
            var pawnContracts = pawnContractRepository.GetAllContracts();
            var pawnContractId = pawnContracts.Select(pc => pc.ItemId).ToList();
            var itemsInPawn = items.GetItems().Where(i => pawnContractId.Contains(i.ItemId)).ToList();

            PawnContractsGrid.ItemsSource = pawnContracts;

        }






        private void BtnLiquidate_Click(object sender, RoutedEventArgs e)
        {
            var selectedContract = (PawnContract)PawnContractsGrid.SelectedItem;

            if (selectedContract == null)
            {
                MessageBox.Show("Please select a contract to liquidate.");
                return;
            }

            using (var context = new PawnShopContext())
            {
                var pawnContract = context.PawnContracts
                    .FirstOrDefault(pc => pc.ContractId == selectedContract.ContractId);

                if (pawnContract != null)
                {
                    // Fetch the associated item using ItemId
                    var item = context.Item.FirstOrDefault(i => i.ItemId == pawnContract.ItemId);

                    if (item != null)
                    {
                        var shopItem = new ShopItem
                        {
                            Name = item.Name,
                            Description = item.Description,
                            Price = item.Value,
                            DateAdded = DateTime.Now,
                            IsExpired = true,
                        };

                        try
                        {
                            context.ShopItem.Add(shopItem);
                            item.Status = "Liquidated"; // Update item's status
                            context.PawnContracts.Remove(pawnContract); // Remove the pawn contract
                            context.SaveChanges();

                            MessageBox.Show("The item has been successfully liquidated and added to the shop.");
                            LoadPawnContracts(); // Refresh the DataGrid
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error liquidating pawn contract: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item associated with this pawn contract not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Pawn contract not found.");
                }
            }
        }


        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}