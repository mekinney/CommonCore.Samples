using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AccountViewModel : CoreViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand LoginUser { get; set; }
        public ICommand RegisterUser { get; set; }

        public AccountViewModel()
        {
            LoginUser = new CoreCommand(async(obj) => { await LoginUserMethod(); });

            RegisterUser = new CoreCommand(async (obj) =>
            {
                if (!string.IsNullOrEmpty(UserName)
                    && !string.IsNullOrEmpty(Password)
                    && Password.Equals(ConfirmPassword))
                {
                    bool success = false;
                    this.LoadingMessageHUD = "Creating account...";
                    this.IsLoadingHUD = true;

                    var registerResult = await this.UserLogic.RegisterNewUser(UserName, Password);
                    success = registerResult.Success;
                    string errorMessage = string.Empty;
                    if (success)
                    {
                        this.LoadingMessageHUD = "Logging in...";
                        success = await this.UserLogic.Login(UserName, Password);
                        errorMessage = "There was an issue logging into your account.";
                    }
                    else{
                        errorMessage = "There was an issue creating your account.";
                    }

                    this.IsLoadingHUD = false;
                    Password = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                    if (success)
                    {
                        App.Current.MainPage = new AppMasterDetailPage();
                    }
                    else
                    {
                        this.DialogPrompt.ShowMessage(new Prompt()
                        {
                            Title = "Error",
                            Message = errorMessage
                        });
                    }
                }
            });
        }


        private async Task LoginUserMethod()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                this.LoadingMessageHUD = "Logging in...";
                this.IsLoadingHUD = true;
                var result = await this.UserLogic.Login(UserName, Password);
                this.IsLoadingHUD = false;
                Password = string.Empty;
                Password = string.Empty;
                if (result)
                {
                    App.Current.MainPage = new AppMasterDetailPage();
                }
                else
                {
                    this.DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = "There was an issue logging into your account."
                    });
                }
            }
        }
    }
}
