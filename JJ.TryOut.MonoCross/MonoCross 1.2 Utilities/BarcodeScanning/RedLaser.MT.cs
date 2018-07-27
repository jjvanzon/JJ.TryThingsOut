using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RedLaser;

namespace MonoCross.Utilities.BarcodeScanning
{
    public class RedLaserMT : IBarcodeScanner
    {
        private static RedLaserMT _instance = null;
        
        public static RedLaserMT GetInstance()
        {
            if (_instance == null)
                _instance = new RedLaserMT();
            return _instance;
        }

        public static RedLaserMT Instance
        {
            get { return _instance; }
        }

        private RedLaserMT()
        {
            _available = new HashSet<Symbology>();
            _available.Add(Symbology.UPCA);
            _available.Add(Symbology.UPCE);
            _available.Add(Symbology.EAN8);
            _available.Add(Symbology.EAN13);
            _available.Add(Symbology.Code39);
            _available.Add(Symbology.Code93);
            _available.Add(Symbology.Code128);
            _available.Add(Symbology.Sticky);

            _enabled = _available;

        }

        public bool NeedsStartToScan
        {
            get { return true; }
        }
    }}
