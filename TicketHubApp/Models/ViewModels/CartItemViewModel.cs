using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class CartItemViewModel
    {
        //商品編號
        public int Id { get; set; }
        //商品名稱
        public string Name { get; set; }
        //商品購買價格
        public decimal Price { get; set; }
        //商品圖示
        public string Img { set; get; }
        //商品購買數量
        public int Quantity { get; set; }
        //商品小計
        public decimal Amount {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }
}