using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    /// <summary>
    /// An address belonging to a business contact.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/common-data-model/schema/core/applicationcommon/address"/>
    public class BusinessContactAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
    }
}
