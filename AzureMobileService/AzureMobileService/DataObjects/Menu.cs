using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class Menu
    {
        public string Id { get; set; }
        public byte[] MenuImage { get; set; }

        public string RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}