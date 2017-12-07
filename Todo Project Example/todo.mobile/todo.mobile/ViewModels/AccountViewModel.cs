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
        public User CurrentUser 
        { 
            get { return AppSettings.AppUser; }
            set { AppSettings.AppUser = value; }
        }
        public ICommand LoginUser { get; set; }
        public ICommand RegisterUser { get; set; }
        public ICommand SaveProfile { get; set; }

        public AccountViewModel()
        {
            LoginUser = new CoreCommand(async (obj) => { await LoginUserMethod(); });
            RegisterUser = new CoreCommand(
                async (obj) => { await RegisterUserMethod(); },
                ValidateRegistration,
                this);
            SaveProfile = new CoreCommand(
                async (obj) => { await SaveProfileMethod(); },
                ValidateProfileSave,
                this);
        }

        private bool ValidateRegistration()
        {
            return this.ValidateTextFields(UserName, Password).
                               ValidatePasswordMatch(Password, ConfirmPassword);
        }

        private bool ValidateProfileSave()
        {
            return this.ValidateTextFields(CurrentUser.FirstName, CurrentUser.LastName).
                               ValidatePasswordMatch(Password, ConfirmPassword);
        }
        private async Task SaveProfileMethod()
        {
            var title = string.Empty;
            var message = string.Empty;

            var profile = new Profile(CurrentUser)
            {
                Password = this.Password
            };
            this.LoadingMessageHUD = "Updating account...";
            this.IsLoadingHUD = true;
           
            var result = await this.UserLogic.UpdateProfile(profile);
            this.IsLoadingHUD = false;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            if(result.Success)
            {
                title = "Success";
                message = "The profile has been update";
            }
            else
            {
                title = "Error";
                message = result.error.Message;
            }
            this.DialogPrompt.ShowMessage(new Prompt()
            {
                Title = title,
                Message = message
            });
        }
        private async Task RegisterUserMethod()
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
            else
            {
                errorMessage = "There was an issue creating your account.";
            }

            this.IsLoadingHUD = false;
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

        private async Task LoginUserMethod()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                this.LoadingMessageHUD = "Logging in...";
                this.IsLoadingHUD = true;
                var result = await this.UserLogic.Login(UserName, Password);
                this.IsLoadingHUD = false;
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
                        Message = "There was an issue logging into your account."
                    });
                }
            }
        }
    }
}
