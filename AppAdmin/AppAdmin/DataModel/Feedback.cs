using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdmin.DataModel
{
    public class Feedback
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "review")]
        public string Review { get; set; }
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
        public string RestaurantId { get; set; }

    }
}
