using OrderFoodApp.Assets.Contains;
using OrderFoodApp.Models;
using OrderFoodApp.Services;
using OrderFoodApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrderFoodApp.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        private User customerUser;
        public User CustomerUser
        {
            get => customerUser;
            set
            {
                customerUser = value;
                OnPropertyChanged();
            }
        }


        private string userAvatar;
        public string UserAvatar
        {
            get => userAvatar;
            set
            {
                userAvatar = value;
                OnPropertyChanged();

            }
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        private string userEmail;
        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                OnPropertyChanged();
            }
        }

        private string imagename;
        public string ImageName
        {
            get => imagename;
            set
            {
                imagename = value;
                OnPropertyChanged();
            }
        }
        public byte[] ImageData;
        public Command UploadAvatarCommand { get; set; }
        public Command SaveInfoEdited { get; set; }

        public static readonly string SourceImagePath = Const.Domain + @"Assets/Images/User/";

        public EditProfileViewModel(User user)
        {
            CustomerUser = user;
            LoadData();

            UploadAvatarCommand = new Command(UploadAvatarCommandExecute, () => true);
            SaveInfoEdited = new Command(SaveInfoEditedExecute, () => true);
        }

        public void LoadData()
        {
            UserName = CustomerUser.Name;

            UserEmail = CustomerUser.Email;
            UserAvatar = CustomerUser.Avatar;

            ImageName = CustomerUser.Avatar.Replace(SourceImagePath, "");
        }

       
        public async void UploadAvatarCommandExecute()
        {

            var file = await MediaPicker.PickPhotoAsync();
            if (file == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong!", "Ok");
                return;
            }

            byte[] buffer = File.ReadAllBytes(file.FullPath);
            ImageData = buffer;

            string dateTime = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss");
            ImageName = dateTime + file.FileName;

            UserAvatar = file.FullPath;

        }
        public async void SaveInfoEditedExecute()
        {
            CustomerUser.Name = UserName;
            CustomerUser.Email = UserEmail;
            CustomerUser.Avatar = ImageName;
            CustomerUser.Password = "";

            if (ImageData != null)
            {
                await UserService.UploadImage(ImageData, ImageName);
            }

            if (SaveInfoCustomerEditedCanExecute())
            {
                if (CustomerUser != null)
                {
                    bool updateSuccess = await UserService.UpdateUser(CustomerUser);

                    if (updateSuccess)
                    {
                        Preferences.Set("userAvatar", SourceImagePath + ImageName);
                        await App.Current.MainPage.DisplayAlert("", "Account update successful", "Ok");
                        await App.Current.MainPage.Navigation.PushModalAsync(new HomePage());

                    }
                    else
                    {
                        
                        await App.Current.MainPage.DisplayAlert("", "Something went wrong", "Ok");
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Plese fill information", "Ok");
            }
        }

        public bool SaveInfoCustomerEditedCanExecute()
        {

            if (string.IsNullOrWhiteSpace(UserName) ||
                string.IsNullOrWhiteSpace(UserEmail) ||
                    string.IsNullOrWhiteSpace(ImageName))
            {
                return false;
            }
            return true;
        }
    }
}
