using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Consts;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region properties
        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string password;
        public string Passowrd
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        #endregion

        #region commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command(() => OnLogin());
            }
        }
        #endregion

        #region construct
        public LoginViewModel()
        {
            Title = ViewTitles.LoginPage;
        }
        #endregion

        #region private methods
        private void OnLogin()
        {
            Application.Current.MainPage = new AppShell();            
        }
        #endregion
    }
}
