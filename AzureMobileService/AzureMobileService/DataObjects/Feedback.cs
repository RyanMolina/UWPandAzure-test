using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class Feedback
    {
        public string Id { get; set; }

        public string Review { get; set; }
        public int Rating { get; set; }


        public string RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}