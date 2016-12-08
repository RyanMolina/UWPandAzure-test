using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class Admin
    {

        public Admin()
        {
            this.Restaurant = new List<Restaurant>();
        }

        public string Id { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public virtual ICollection<Restaurant> Restaurant { get; set; }

    }
}