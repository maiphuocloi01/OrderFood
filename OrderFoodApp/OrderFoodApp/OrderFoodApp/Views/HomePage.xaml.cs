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
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<PopularProduct> ProductsCollection;
        public ObservableCollection<Category> CategoriesCollection;
        public ObservableCollection<Advertisement> Advertisements;
        public HomePage()
        {
            InitializeComponent();
            ProductsCollection = new ObservableCollection<PopularProduct>();
            CategoriesCollection = new ObservableCollection<Category>();
            Advertisements = new ObservableCollection<Advertisement>();
            LoadData();
            GetPopularProducts();
            GetCategories();
            
            BindingContext = new HomeViewModel();
            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                CarouselViewer.Position = (CarouselViewer.Position + 1) % 3;
                return true;
            }));
        }

        private async void LoadData()
        {
            var advertises = await ProductService.GetAllAdvertisement();
            foreach (var advertise in advertises)
            {
                Advertisements.Add(advertise);
            }
            CarouselViewer.ItemsSource = Advertisements;
        }

        private async void GetCategories()
        {
            var categories = await CategoryService.GetCategories();
            foreach (var category in categories)
            {
                CategoriesCollection.Add(category);
            }
            CvCategories.ItemsSource = CategoriesCollection;
        }

        private async void GetPopularProducts()
        {
            var products = await ProductService.GetPopularProducts();
            foreach (var product in products)
            {
                ProductsCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductsCollection;
        }

        private async void ImgMenu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private async void TapSeeMore_Tapped(object sender, EventArgs e)
        {
            var resultList = await ProductService.GetAllProduct();
            await Navigation.PushModalAsync(new SearchPage(new ObservableCollection<Product>(resultList)));
        }

        private void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            CloseHamBurgerMenu();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var id = Preferences.Get("userId", 0);
            var response = await ShoppingCartItemService.GetTotalCartItems(id);
            LblTotalItems.Text = response.totalItems.ToString();

            LblUserName.Text = Preferences.Get("userName", string.Empty);
            imgAvt.Source = new UriImageSource
            {
                Uri = new Uri(Preferences.Get("userAvatar", string.Empty)),
                //CachingEnabled = true,
                //CacheValidity = new TimeSpan(5, 0, 0, 0)
            };
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CloseHamBurgerMenu();
        }

        private async void CloseHamBurgerMenu()
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }


        private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new ProductListPage(currentSelection.id, currentSelection.name));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currectSelection = e.CurrentSelection.FirstOrDefault() as PopularProduct;
            if (currectSelection == null) return;
            Navigation.PushModalAsync(new ProductDetailPage(currectSelection.id));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void TapCartIcon_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CartPage());
        }

        private void TapOrders_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new OrdersPage());
        }

        private void TapContact_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProfilePage());
        }

        private void TapCart_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CartPage());
        }

        private void TapLogout_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}