using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class StoreTime
    {
        public string Id { get; set; }

        public DayOfWeek Day { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }

        public string RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}