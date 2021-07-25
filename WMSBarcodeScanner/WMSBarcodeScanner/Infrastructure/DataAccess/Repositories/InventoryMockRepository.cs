using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Infrastructure.Events;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.DataAccess.Repositories
{
    public class InventoryMockRepository : IInventoryRepository
    {
        private readonly List<Inventory> Inventory;

        public event EventHandler<InventoryAddEventArgs> InventoryAdd;
        public event EventHandler<InventoryDeleteEventArgs> InventoryDelete;

        public InventoryMockRepository()
        {
            Inventory = new List<Inventory>()
            {
                new Inventory 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Name = "Dexoftyal",
                    Description = "Nawilżające i regenerujące krople do oczu",
                    Quantity = 1,
                    Manufacturer = "TEGE Pharma B.V.",
                    Barcode = "5901549084108",
                    PackageUnit = InventoryUnit.Grames 
                },
                new Inventory 
                { 
                    Id = Guid.NewGuid().ToString(),
                    Name = "ZYX Bio",
                    Description = "Tabletki powlekane przeciwalergiczne",
                    Quantity = 1,
                    Manufacturer = "Biofarm",
                    Barcode = "5909990847938",
                    PackageUnit = InventoryUnit.Pcs 
                },
                new Inventory
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "RenoPuren Zatoki MAX",
                    Description = "Wspomaga funkcjonowanie górnych dróg oddechowych",
                    Quantity = 1,
                    Manufacturer = "Aflofarm Farmacja Polska sp. z o.o.",
                    Barcode = "5906071004099",
                    PackageUnit = InventoryUnit.Pcs
                }
            };
        }

        public async Task<bool> AddInventoryAsync(Inventory inventory)
        {
            Inventory.Add(inventory);
            InventoryAdd.Invoke(this, new InventoryAddEventArgs() { Inventory = inventory });

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateInventoryAsync(Inventory inventory)
        {
            var oldInventory = Inventory.Where((arg) => arg.Id == inventory.Id).FirstOrDefault();
            Inventory.Remove(oldInventory);
            Inventory.Add(inventory);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteInventoryAsync(string id)
        {
            var oldInventory = Inventory.Where((arg) => arg.Id == id).FirstOrDefault();
            Inventory.Remove(oldInventory);
            InventoryDelete.Invoke(this, new InventoryDeleteEventArgs() { Inventory = oldInventory });

            return await Task.FromResult(true);
        }

        public async Task<Inventory> GetInventoryByIdAsync(string id)
        {
            return await Task.FromResult(Inventory.FirstOrDefault(arg => arg.Id == id));
        }

        public async Task<Inventory> GetInventoryByBarcodeAsync(string barcode)
        {
            return await Task.FromResult(Inventory.FirstOrDefault(arg => arg.Barcode == barcode));
        }

        public async Task<IEnumerable<Inventory>> GetInventoryAsync()
        {
            return await Task.FromResult(Inventory);
        }

        public async Task<IEnumerable<Inventory>> SearchForInventory(string searchText)
        {
            List<Inventory> inventoryList = Inventory.Where(arg => arg.Barcode.ToUpper().Contains(searchText.ToUpper())).ToList();

            if (inventoryList.Count == 0)
                inventoryList = Inventory.Where(arg => arg.Name.ToUpper().Contains(searchText.ToUpper())).ToList();

            return await Task.FromResult(inventoryList);
        }

        public async Task<IEnumerable<Inventory>> SearchByBarcode(string barcode)
        {
            List<Inventory> inventoryList = Inventory.Where(arg => arg.Barcode == barcode).ToList();

            return await Task.FromResult(inventoryList);
        }
    }
}