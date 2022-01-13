using OrderFoodApp.Assets.Contains;
using OrderFoodApp.Models;
using OrderFoodApp.Services;
using OrderFoodApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrderFoodApp.ViewModels
{
    public class PasswordViewModel : BaseViewModel
    {
        private string useroldpassword;
        public string UserOldPassword
        {
            get => useroldpassword;
            set
            {
                useroldpassword = value;
                OnPropertyChanged();
                SaveNewPassword.ChangeCanExecute();
            }
        }

        private string usernewpassword;
        public string UserNewPassword
        {
            get => usernewpassword;
            set
            {
                usernewpassword = value;
                OnPropertyChanged();
                SaveNewPassword.ChangeCanExecute();
            }
        }

        private string usernewpasswordconfirm;
        public string UserNewPasswordConfirm
        {
            get => usernewpasswordconfirm;
            set
            {
                usernewpasswordconfirm = value;
                OnPropertyChanged();
                SaveNewPassword.ChangeCanExecute();
            }
        }

        public Command SaveNewPassword { get; set; }

        public PasswordViewModel()
        {

            SaveNewPassword = new Command(SaveNewPasswordExecute, () => SaveNewPasswordCanExecute());
        }

        public async void SaveNewPasswordExecute()
        {
            var id = Preferences.Get("userId", 0);
            User user = await UserService.GetUserByID(id);
            
            user.Avatar = "";
            user.Name = "";
            user.Email = "";


            if (user.Password.Equals(Const.CreateMD5(UserOldPassword)))
            {
                checkNewPassword(user);                  
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Old password is incorrect", "Cancel");
            }

        }

        public async void checkNewPassword(User user)
        {
            if (UserNewPassword.Equals(UserNewPasswordConfirm))
            {
                user.Password = UserNewPassword;
                await UserService.UpdateUser(user);

                await App.Current.MainPage.DisplayAlert("", "Change password successfully!", "OK");
                await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Something went wrong", "Cancel");
            }
        }


        public bool SaveNewPasswordCanExecute()
        {

            if (string.IsNullOrWhiteSpace(UserOldPassword) ||
            string.IsNullOrWhiteSpace(UserNewPassword) ||
            string.IsNullOrWhiteSpace(UserNewPasswordConfirm))
            {
                return false;
            }
            return true;


        }
    }
}
