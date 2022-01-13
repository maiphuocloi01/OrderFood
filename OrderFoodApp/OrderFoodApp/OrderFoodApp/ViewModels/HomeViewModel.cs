using OrderFoodApp.Models;
using OrderFoodApp.Services;
using OrderFoodApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace OrderFoodApp.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {

        public Command SearchCommand { get; set; }
        public HomeViewModel()
        {
            SearchCommand = new Command<string>(SearchCommandExecute, (s) => true);
        }

        public async void SearchCommandExecute(string searchText)
        {
            var s = searchText;
            var resultList = await ProductService.GetProductBySearchText(s);
            await App.Current.MainPage.Navigation.PushModalAsync(new SearchPage(new ObservableCollection<Product>(resultList)));
            //await App.Current.MainPage.Navigation.PushModalAsync(new ProductListPage(new ObservableCollection<Product>(resultList)));
        }
    }
}
