using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdmin.DataModel
{
    public class StoreTime
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "day")]
        public DayOfWeek Day { get; set; }
        [JsonProperty(PropertyName = "openTime")]
        public TimeSpan OpenTime { get; set; }
        [JsonProperty(PropertyName = "closeTime")]
        public TimeSpan CloseTime { get; set; }
        [JsonProperty(PropertyName = "restaurantId")]
        public string RestaurantId { get; set; }

        public override string ToString()
        {
            return String.Format("{0} to {1}", OpenTime, CloseTime);
        }
    }

}
