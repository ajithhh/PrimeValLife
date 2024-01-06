using PrimeValLife.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeValLife.Core.Utilities
{
    public class TempCart2Cart
    {
        public static Cart ConvertTempCart2Cart(TempCart tempcart)
        {
            return new Cart()
            {
                CartItems = ConvertTempCartItem2CartItem(tempcart.TempCartItems),
            };
        }

        public static List<CartItem> ConvertTempCartItem2CartItem(List<TempCartItem> tempcartItems)
        {
            List<CartItem > cartItems = new List<CartItem>();
            foreach (var tempCartItem in tempcartItems)
            {
                cartItems.Add(new CartItem() {ProductId= tempCartItem.ProductId,Quantity=tempCartItem.Quantity,Product=tempCartItem.Product});
            }
            return cartItems;

        }

    }
}
