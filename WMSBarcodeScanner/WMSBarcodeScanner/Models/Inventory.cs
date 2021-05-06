namespace WMSBarcodeScanner.Models
{
    public class Inventory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public InventoryUnit PackageUnit { get; set; }
        public string Barcode { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
