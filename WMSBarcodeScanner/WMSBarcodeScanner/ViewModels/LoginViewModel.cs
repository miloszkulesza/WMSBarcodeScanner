using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Infrastructure.Consts;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region private members
        private readonly IUserRepository userRepository = DependencyService.Get<IUserRepository>();
        #endregion

        #region properties
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private bool wrongCredentials;

        public bool WrongCredentials
        {
            get { return wrongCredentials; }
            set { SetProperty(ref wrongCredentials, value); }
        }

        #endregion

        #region commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () => await OnLogin());
            }
        }
        #endregion

        #region construct
        public LoginViewModel()
        {
            Title = ViewTitles.LoginPage;
            WrongCredentials = false;
            Username = "mkulesza";
            Password = "admin";
        }
        #endregion

        #region private methods
        private async Task OnLogin()
        {
            IsBusy = true;
            await Task.Delay(2000);
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                WrongCredentials = true;
            else
            {
                var user = await userRepository.LoginAsync(Username, Password);
                if (user.Id == string.Empty)
                    WrongCredentials = true;
                else
                    Application.Current.MainPage = new AppShell();
            }
            IsBusy = false;
        }
        #endregion
    }
}
