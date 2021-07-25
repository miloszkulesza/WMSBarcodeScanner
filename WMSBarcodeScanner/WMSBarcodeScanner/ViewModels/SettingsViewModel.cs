using System.Windows.Input;
using WMSBarcodeScanner.Infrastructure.Consts;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region private members
        private readonly ISettingsManager settingsManager = DependencyService.Get<ISettingsManager>();
        #endregion

        #region properties
        private string wmsServiceAddress;
        public string WMSServiceAddress
        {
            get { return wmsServiceAddress; }
            set { SetProperty(ref wmsServiceAddress, value); }
        }
        #endregion

        #region commands
        public ICommand SaveCommmand { get; set; }
        #endregion

        #region ctor
        public SettingsViewModel()
        {
            Title = ViewTitles.SettingsPage;
            SaveCommmand = new Command(OnSave);
            WMSServiceAddress = settingsManager.WMSServiceAddress;
        }
        #endregion

        #region private methods
        private void OnSave()
        {
            settingsManager.WMSServiceAddress = WMSServiceAddress;
        }
        #endregion
    }
}
