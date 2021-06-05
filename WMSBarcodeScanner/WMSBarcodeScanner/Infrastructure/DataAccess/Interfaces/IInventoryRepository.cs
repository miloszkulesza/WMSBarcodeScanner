using System.Collections.Generic;
using System.Threading.Tasks;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces
{
    public interface IInventoryRepository
    {
        Task<bool> AddInventoryAsync(Inventory item);
        Task<bool> UpdateInventoryAsync(Inventory item);
        Task<bool> DeleteInventoryAsync(string id);
        Task<Inventory> GetInventoryByIdAsync(string id);
        Task<IEnumerable<Inventory>> GetInventoryAsync();
        Task<Inventory> GetInventoryByBarcodeAsync(string barcode);
        Task<IEnumerable<Inventory>> SearchForInventory(string searchText);
        Task<IEnumerable<Inventory>> SearchByBarcode(string barcode);
    }
}
