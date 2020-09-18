using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    [Serializable] //可序列化
    public class CartViewModel
    {
        //建構值
        public CartViewModel()
        {
            this.cartItems = new List<CartItemViewModel>();
        }
        
        //儲存所有商品
        public List<CartItemViewModel> cartItems;

        //取得商品總價
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach (var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }
    }

    
}