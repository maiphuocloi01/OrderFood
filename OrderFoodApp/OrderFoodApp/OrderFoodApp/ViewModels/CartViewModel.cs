using OrderFoodApp.Models;
using OrderFoodApp.Services;
using OrderFoodApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrderFoodApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private ObservableCollection<ShoppingCartItem> cartProducts;

        public ObservableCollection<ShoppingCartItem> CartProducts
        {
            get => cartProducts;
            set
            {
                cartProducts = value;
                OnPropertyChanged();
            }
        }

        private int totalPrice;

        public int TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        public Command DecreaseCommand { get; set; }
        public Command IncreaseCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command DeleteAllCommand { get; set; }
        public Command ProductDetailOnClick { get; set; }

        public CartViewModel()
        {
            LoadData();

            DecreaseCommand = new Command<ShoppingCartItem>(DecreaseCommandExecute, c => c != null);
            IncreaseCommand = new Command<ShoppingCartItem>(IncreaseCommandExecute, c => c != null);
            DeleteCommand = new Command<ShoppingCartItem>(DeleteCommandExecute, c => c != null);
            DeleteAllCommand = new Command(DeleteAllCommandExecute, () => true);
            //ProductDetailOnClick = new Command<ShoppingCartItem>(ProductDetailOnClickExcute, product => product != null);
        }

        public async void LoadData()
        {
            TotalPrice = 0;
            var id = Preferences.Get("userId", 0);
            CartProducts = new ObservableCollection<ShoppingCartItem>(await ShoppingCartItemService.GetShoppingCartItems(id));
            if (CartProducts != null && cartProducts.Count > 0)
            {
                foreach (var c in CartProducts)
                {
                    TotalPrice += (int)c.TotalAmount;
                }
            }
        }

        async void DecreaseCommandExecute(ShoppingCartItem cartItem)
        {
            if (cartItem.Qty > 1)
            {
                cartItem.Qty--;
                cartItem.TotalAmount = cartItem.Qty*cartItem.price;
                TotalPrice -= (int)cartItem.price;
                var response = await ShoppingCartItemService.UpdateQuantity(cartItem.id, cartItem.Qty);
                if (response)
                {
                    //LoadData();
                }

            }
        }

        async void IncreaseCommandExecute(ShoppingCartItem cartItem)
        {
            cartItem.Qty++;
            TotalPrice += (int)cartItem.price;
            cartItem.TotalAmount = cartItem.Qty * cartItem.price;
            var response = await ShoppingCartItemService.UpdateQuantity(cartItem.id, cartItem.Qty);
            if (response)
            {
                //LoadData();
            }
        }

        async void DeleteCommandExecute(ShoppingCartItem cartItem)
        {

            CartProducts.Remove(cartItem);
            TotalPrice -= (int)cartItem.TotalAmount;

            var delete = await ShoppingCartItemService.ClearShoppingCartByID(cartItem.id);
            if (delete)
            {

            }
        }

        public async void DeleteAllCommandExecute()
        {
            var id = Preferences.Get("userId", 0);
            var answer = await App.Current.MainPage.DisplayAlert("", "Do you want to clear your cart?", "Yes", "No");
            if (answer)
            {
                var response = await ShoppingCartItemService.ClearShoppingCart(id);
                if (response)
                {

                    CartProducts = null;
                    TotalPrice = 0;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Something went wrong", "Cancel");
                }
            }
        }

        public async void ProductDetailOnClickExcute(ShoppingCartItem product)
        {
            //await App.Current.MainPage.DisplayAlert("t", product.ID.ToString(), "OK");
            await App.Current.MainPage.Navigation.PushModalAsync(new ProductDetailPage(product.productId), true);
        }
    }
}
