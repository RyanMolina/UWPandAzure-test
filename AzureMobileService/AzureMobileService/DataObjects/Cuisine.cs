
using AzureMobileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class Cuisine
    {

        public string Id { get; set; }
        public CuisineType CuisineType { get; set; }

        public string RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}