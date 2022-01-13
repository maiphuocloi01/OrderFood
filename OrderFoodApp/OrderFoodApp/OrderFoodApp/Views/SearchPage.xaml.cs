using OrderFoodApp.Models;
using OrderFoodApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public ObservableCollection<Product> ProductCollection;

        public SearchPage(ObservableCollection<Product> products)
        {
            InitializeComponent();
            BindingContext = new SearchViewModel(products);
            ProductCollection = new ObservableCollection<Product>();
            GetProducts(products);
        }

        private void GetProducts(ObservableCollection<Product> products)
        {

            foreach (var product in products)
            {
                ProductCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currectSelection = e.CurrentSelection.FirstOrDefault() as Product;
            if (currectSelection == null) return;
            Navigation.PushModalAsync(new ProductDetailPage(currectSelection.id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}