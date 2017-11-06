using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class AccountViewModel : ObservableViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand LoginUser { get; set; }
        public ICommand RegisterUser { get; set; }

        public AccountViewModel()
        {
            LoginUser = new RelayCommand(async (obj) =>
            {
                if (!string.IsNullOrEmpty(UserName) 
                    && !string.IsNullOrEmpty(Password))
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
                        await Navigation.PushAsync(new TodoPage());
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

            });

            RegisterUser = new RelayCommand(async (obj) =>
            {
                if (!string.IsNullOrEmpty(UserName)
                    && !string.IsNullOrEmpty(Password)
                    && Password.Equals(ConfirmPassword))
                {

                    this.LoadingMessageHUD = "Creating account...";
                    this.IsLoadingHUD = true;

                    var result = await this.UserLogic.RegisterNewUser(UserName, Password);
                    string errorMessage = string.Empty;
                    if (result)
                    {
                        this.LoadingMessageHUD = "Logging in...";
                        result = await this.UserLogic.Login(UserName, Password);
                        errorMessage = "There was an issue logging into your account.";
                    }
                    else{
                        errorMessage = "There was an issue creating your account.";
                    }

                    this.IsLoadingHUD = false;
                    Password = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                    if (result)
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
    }
}
