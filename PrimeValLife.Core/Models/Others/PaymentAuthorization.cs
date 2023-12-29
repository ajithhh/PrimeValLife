using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeValLife.Core.Models.Others
{
    public enum PaymentAuthorization
    {
        COD,
        PREPAID_INITIATED,
        PREPAID_PENDING,
        PREPAID_REJECTED,
        PREPAID_AUTHORIZED
    }
}
