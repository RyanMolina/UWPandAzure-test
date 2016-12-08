using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureMobileService.DataObjects
{
    public class MenuDTO : EntityData
    {
        public byte[] MenuImage { get; set; }
    }
}