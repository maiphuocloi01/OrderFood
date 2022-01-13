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
    public class ProfileViewModel: BaseViewModel
    {
        private User user;
        public User IUser
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private string useravatar;
        public string UserAvatar
        {
            get => useravatar;
            set
            {
                useravatar = value;
                OnPropertyChanged();
            }
        }

        private string username;
        public string UserName
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string useremail;
        public string UserEmail
        {
            get => useremail;
            set
            {
                useremail = value;
                OnPropertyChanged();
            }
        }

        public Command EditProfileCommand { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public Command LogoutCommand { get; set; }

        public ProfileViewModel()
        {
            LoadData();

            EditProfileCommand = new Command(EditProfileCommandExecute, () => true);
            ChangePasswordCommand = new Command(ChangePasswordCommandExecute, () => true);
            
            LogoutCommand = new Command(LogoutCommandExecute, () => true);

        }

        private async void LoadData()
        {
            var id = Preferences.Get("userId", 0);
            if (await UserService.GetUserByID(id) != null)
                IUser = await UserService.GetUserByID(id);

            if (IUser != null)
            {
                UserAvatar = IUser.Avatar;
                UserName = IUser.Name;
                UserEmail = IUser.Email;
            }


        }

        public async void EditProfileCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new EditProfilePage(IUser));
        }

        public async void ChangePasswordCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new PasswordPage());
        }

        public async void LogoutCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
