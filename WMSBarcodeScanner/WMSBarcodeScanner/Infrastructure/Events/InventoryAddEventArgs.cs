using System;
using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.Infrastructure.Events
{
    public class InventoryAddEventArgs : EventArgs
    {
        public Inventory Inventory { get; set; }
    }
}
