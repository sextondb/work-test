using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    /// <summary>
    /// A user of the system, who has business contacts, whos information is stored in business contact records
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
