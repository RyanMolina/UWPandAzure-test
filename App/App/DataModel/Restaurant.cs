
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModel
{
    public class Restaurant
    {


        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; set; }
        [JsonProperty(PropertyName = "creditCard")]
        public bool CreditCard { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "type")]
        public EstablishmentType Type { get; set; }
        [JsonProperty(PropertyName = "contactNo")]
        public string ContactNo { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "wifi")]
        public bool Wifi { get; set; }
        [JsonProperty(PropertyName = "outdoorSeating")]
        public bool OutdoorSeating { get; set; }
        [JsonProperty(PropertyName = "petFriendly")]
        public bool PetFriendly { get; set; }
        [JsonProperty(PropertyName = "image")]
        public byte[] Image { get; set; }


        [JsonProperty(PropertyName = "feedback")]
        public ObservableCollection<Feedback> Feedback { get; set; }
        [JsonProperty(PropertyName = "cuisine")]
        public ICollection<Cuisine> Cuisine { get; set; }
        [JsonProperty(PropertyName = "storeTime")]
        public ICollection<StoreTime> StoreTime { get; set; }

        [JsonProperty(PropertyName = "menu")]
        public ICollection<Menu> Menu { get; set; }

        public string AdminId { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string CuisineString
        {
            get { return string.Join(", ", Cuisine); }
        }

    }
}
