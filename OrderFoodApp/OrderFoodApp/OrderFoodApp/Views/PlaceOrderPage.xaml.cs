using OrderFoodApp.Models;
using OrderFoodApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        private double _totalPrice;
        public PlaceOrderPage(double totalPrice)
        {
            InitializeComponent();
            _totalPrice = totalPrice;
        }

        private async void BtnPlaceOrder_Clicked(object sender, EventArgs e)
        {
            var id = Preferences.Get("userId", 0);
            var order = new Order();
            order.fullName = EntName.Text;
            order.phone = EntPhone.Text;
            order.address = EntAddress.Text;
            order.userId = 1;
            order.orderTotal = _totalPrice;

            var response = await OrderService.PlaceOrder(order);
            if (response != null)
            {
                await DisplayAlert("", "Your Order Id is " + response.orderId, "Alright");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}