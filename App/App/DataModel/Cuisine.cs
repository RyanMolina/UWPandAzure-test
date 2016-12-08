using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModel
{
    public class Cuisine
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "cuisineType")]
        public CuisineType CuisineType { get; set; }
        public string RestaurantId { get; set; }

        public override string ToString()
        {
            return CuisineType.ToString();
        }
    }
}
