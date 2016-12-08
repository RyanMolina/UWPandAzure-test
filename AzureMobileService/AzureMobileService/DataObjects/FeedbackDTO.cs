using AzureMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class FeedbackDTO : EntityData
    {
        public string Review { get; set; }
        public int Rating { get; set; }

    }
}