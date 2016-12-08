﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace AppAdmin.DataModel
{
    public class MenuImage
    {
        public string Id { get; set; }
        public BitmapImage Image { get; set; }
        public StorageFile File { get; set; }
    }
}
