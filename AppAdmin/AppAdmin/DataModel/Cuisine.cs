using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdmin.DataModel
{
    public class Cuisine
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "cuisineType")]
        public CuisineType CuisineType { get; set; }
        public string RestaurantId { get; set; }
    }
}
