using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeValLife.Core.Models.Users
{
    public  class TempCart
    {
        public int TempCartId {  get; set; }

        public string SessionId {  get; set; }

        public List<TempCartItem> TempCartItems { get; set; }
    }
}
