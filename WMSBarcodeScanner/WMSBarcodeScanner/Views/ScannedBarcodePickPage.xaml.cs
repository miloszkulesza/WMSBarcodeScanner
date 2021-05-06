﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedBarcodePickPage : ContentPage
    {
        public ScannedBarcodePickPage(string barcode)
        {
            InitializeComponent();
            BindingContext = new ScannedBarcodePickViewModel(barcode);
        }
    }
}