using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;

namespace MonoCross.Utilities.BarcodeScanning
{
    public class BarcodeScannerFactory
    {
        public static IBarcodeScanner Create(Context context)
        {
            return RedLaser.GetInstance(context);
        }
    }
}