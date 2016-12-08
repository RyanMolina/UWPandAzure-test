
using AzureMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class CuisineDTO : EntityData
    {
        public CuisineType CuisineType { get; set; }
    }
}