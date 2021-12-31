using OrderFoodApp.Models;
using OrderFoodApp.Services;
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
        public ObservableCollection<ShoppingCartItem> ShoppingCartCollection;
        public CartPage()
        {
            InitializeComponent();
            ShoppingCartCollection = new ObservableCollection<ShoppingCartItem>();
            GetShoppingCartItems();
            GetTotalPrice();
        }

        private async void GetTotalPrice()
        {
            var id = Preferences.Get("userId", 0);
            var totalPrice = await ShoppingCartItemService.GetCartSubTotal(id);
            LblTotalPrice.Text = totalPrice.subTotal.ToString();
        }

        private async void GetShoppingCartItems()
        {
            var id = Preferences.Get("userId", 0);
            var shoppingCartItems = await ShoppingCartItemService.GetShoppingCartItems(id);
            foreach (var shoppingCart in shoppingCartItems)
            {
                ShoppingCartCollection.Add(shoppingCart);
            }
            LvShoppingCart.ItemsSource = ShoppingCartCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void TapClearCart_Tapped(object sender, EventArgs e)
        {
            var id = Preferences.Get("userId", 0);
            var response = await ShoppingCartItemService.ClearShoppingCart(id);
            if (response)
            {
                await DisplayAlert("", "Your cart has been cleared", "Alright");
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";
            }
            else
            {
                await DisplayAlert("", "Something went wrong", "Cancel");
            }
        }

        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            var totalPrice1 = LblTotalPrice.Text;
            Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(totalPrice1)));
        }
    }
}