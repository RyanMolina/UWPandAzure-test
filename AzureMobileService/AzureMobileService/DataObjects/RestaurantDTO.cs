
using AzureMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class RestaurantDTO : EntityData
    {


        public string Name { get; set; }
        public string Description { get; set; }
        public EstablishmentType Type { get; set; }
        public string ContactNo { get; set; }
        public int Rating { get; set; }

        public string Address { get; set; }

        public byte[] Image { get; set; }
        public int Cost { get; set; }
        public bool CreditCard { get; set; }
        public bool Wifi { get; set; }
        public bool OutdoorSeating { get; set; }
        public bool PetFriendly { get; set; }

        public virtual ICollection<FeedbackDTO> Feedback { get; set; }
        public virtual ICollection<CuisineDTO> Cuisine { get; set; }
        public virtual ICollection<StoreTimeDTO> StoreTime { get; set; }

        public virtual ICollection<MenuDTO> Menu { get; set; }



    }
}