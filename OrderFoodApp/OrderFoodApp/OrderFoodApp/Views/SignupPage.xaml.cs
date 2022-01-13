using OrderFoodApp.Models;
using OrderFoodApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void TapBackArrow_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if (!EntPassword.Text.Equals(EntConfirmPassword.Text))
            {
                await DisplayAlert("Password mismatch", "Please check your confirm password", "Cancel");
            }
            else
            {
                Register register = new Register()
                {
                    Name = EntName.Text,
                    Email = EntEmail.Text,
                    Password = EntPassword.Text
                };
                var response = await UserService.Register(register);
                if (response)
                {
                    await DisplayAlert("Hi", "Your account has been created", "Alright");
                    await Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Oops", "Something went wrong", "Cancel");
                }
            }
        }

    }
}