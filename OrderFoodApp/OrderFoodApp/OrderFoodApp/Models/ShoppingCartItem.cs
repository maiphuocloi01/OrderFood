using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace OrderFoodApp.Models
{
    public class ShoppingCartItem : INotifyPropertyChanged
    {
        public int id { get; set; }
        public double price { get; set; }
        public double totalAmount { get; set; }
        private int qty { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public string image { get; set; }
        public string sumary { get; set; }

        public int Qty
        {
            get => qty;
            set
            {
                qty = value;
                OnPropertyChanged();
            }
        }

        public double TotalAmount
        {
            get => totalAmount;
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
