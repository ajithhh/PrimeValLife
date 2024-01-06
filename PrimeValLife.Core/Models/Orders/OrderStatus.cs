using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimeValLife.Core.Models.Orders
{
    public enum OrderStatus
    {
        [EnumMember(Value ="NEW")]
        NEW,
        [EnumMember(Value = "PENDING")]
        PENDING,
        [EnumMember(Value = "APPROVED")]
        APPROVED,
    }
}
