using System.ComponentModel.DataAnnotations;

namespace WMSBarcodeScanner.Models
{
    public enum InventoryUnit
    {
        [Display(Name = "szt")]
        Pcs,
        [Display(Name = "kg")]
        Kilogrames,
        [Display(Name = "g")]
        Grames
    }
}
