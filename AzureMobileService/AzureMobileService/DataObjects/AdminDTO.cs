using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class AdminDTO : EntityData
    {
        public string Password { get; set; }

        public string Username { get; set; }

        public virtual ICollection<RestaurantDTO> Restaurant { get; set; }
    }
}