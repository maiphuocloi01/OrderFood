using OrderFoodApp.Models;
using OrderFoodApp.Services;
using OrderFoodApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {

        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartViewModel();
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            if (LblTotalPrice.Text != "0")
            {
                var totalPrice1 = LblTotalPrice.Text;
                Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(totalPrice1)));
            }
            else
            {
                DisplayAlert("", "Empty shopping cart", "Cancel");
            }
        }
    }
}