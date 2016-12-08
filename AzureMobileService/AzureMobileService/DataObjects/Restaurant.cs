
using AzureMobileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class Restaurant
    {
        public Restaurant()
        {
            this.Feedback = new List<Feedback>();
            this.Cuisine = new List<Cuisine>();
            this.StoreTime = new List<StoreTime>();
            this.Menu = new List<Menu>();
        }

        public string Id { get; set; }
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

        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Cuisine> Cuisine { get; set; }
        public virtual ICollection<StoreTime> StoreTime { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
        

        public string AdminId { get; set; }
        public virtual Admin Admin { get; set; }
    }
}