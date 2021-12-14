using OrderFoodApp.Models;
using OrderFoodApp.Services;
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
    public partial class OrderDetailPage : ContentPage
    {
        public ObservableCollection<OrderDetail> OrderDetailCollection;
        public OrderDetailPage(int orderId)
        {
            InitializeComponent();
            OrderDetailCollection = new ObservableCollection<OrderDetail>();
            GetOrderDetail(orderId);
        }
        private async void GetOrderDetail(int ID)
        {
            var orderDetails = await OrderService.GetOrderDetails(ID);
            //var orderDetails = orders[0].orderDetails;
            //foreach (var item in orderDetails)
            //{
            //    OrderDetailCollection.Add(item);
            //}
            double total = 0;
            foreach (var order in orderDetails)
            {
                OrderDetailCollection.Add(order);
                total += order.totalAmount;
            }

            LvOrderDetail.ItemsSource = OrderDetailCollection;

            //LblTotalPrice.Text = orders[0].orderTotal + " $ ";
            LblTotalPrice.Text = total + " $ ";
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}