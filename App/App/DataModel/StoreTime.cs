using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModel
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

        public string RestaurantId { get; set; }

        public override string ToString()
        {
            DateTime time = DateTime.Today.Add(OpenTime);
            string openTime = time.ToString("hh:mm tt");

            time = DateTime.Today.Add(CloseTime);

            string closeTime = time.ToString("hh:mm tt");

            return String.Format("{0} to {1}", openTime, closeTime);
        }
    }

}
