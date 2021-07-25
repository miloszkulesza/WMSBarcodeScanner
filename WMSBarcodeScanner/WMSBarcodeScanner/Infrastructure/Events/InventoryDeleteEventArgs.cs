using System;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.Events
{
    public class InventoryDeleteEventArgs : EventArgs
    {
        public Inventory Inventory { get; set; }
    }
}
