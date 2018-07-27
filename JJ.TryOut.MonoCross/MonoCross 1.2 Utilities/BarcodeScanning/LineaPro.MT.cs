using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.UIKit;

using LineaSDK;

namespace MonoCross.Utilities.BarcodeScanning
{
	public class LineaPro : IBarcodeScanner
	{
		private HashSet<Symbology> _available;
        private HashSet<Symbology> _enabled;
		        
		private const String DO_UPCE = "do_upce";
        private const String DO_EAN8 = "do_ean8";
        private const String DO_EAN13 = "do_ean13";
        private const String DO_CODE128 = "do_code128";
        private const String DO_CODE39 = "do_code39";
        private const String DO_CODE93 = "do_code93";
        private const String DO_STICKY = "do_sticky";
        private const String DO_BROADCAST_TO = "do_broadcast_to";

        //private const String DO_QRCODE = "do_qrcode";
        //private const String DO_DATAMATRIX = "do_datamatrix";
        //private const String DO_RSS14 = "do_rss14";
        //private const String DO_ITF = "do_itf";

        public const int NONE = 0;
        public const int EAN13 = 1;
        public const int UPCE = 2;
        public const int EAN8 = 4;
        public const int STICKY = 8;
        public const int QRCODE = 16;
        public const int CODE128 = 32;
        public const int CODE39 = 64;
        public const int DATAMATRIX = 128;
        public const int ITF = 256;
        public const int CODE93 = 512;
        public const int RSS14 = 1024;
		
		LineaSDK.Linea Linea;
		LineaProDelegate LineaDelegate;
		
		public static LineaPro GetInstance()
        {
            if (_instance == null)
                _instance = new LineaPro();
            return _instance;
        }
		
		public static LineaPro GetInstance(UIWebView webView)
        {
            if (_instance == null)
                _instance = new LineaPro(webView);
            return _instance;
        }
		
        public static LineaPro Instance
        {
            get { return _instance; }
        }
		
        private static LineaPro _instance = null;
		
		public LineaPro()
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
			
			Linea = new LineaSDK.Linea();
		    LineaDelegate = new LineaProDelegate();
		    Linea.Delegate = LineaDelegate;
			Linea.Connect();
			
		}
		
		public LineaPro(UIWebView webView)
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
			
			Linea = new LineaSDK.Linea();
		    LineaDelegate = new LineaProDelegate(webView);
		    Linea.Delegate = LineaDelegate;
			Linea.Connect();
			
		}
//		public LineaPro(UIWebView webView)
//		{
//			_available = new HashSet<Symbology>();
//            _available.Add(Symbology.UPCA);
//            _available.Add(Symbology.UPCE);
//            _available.Add(Symbology.EAN8);
//            _available.Add(Symbology.EAN13);
//            _available.Add(Symbology.Code39);
//            _available.Add(Symbology.Code93);
//            _available.Add(Symbology.Code128);
//            _available.Add(Symbology.Sticky);
//
//            _enabled = _available;
//			
//			Linea = new LineaSDK.Linea();
//		    LineaDelegate = new LineaProDelegate(webView);
//		    Linea.Delegate = LineaDelegate;	
//			Linea.Connect();
//		}
		
		internal void TriggerScan(String data, Symbology symbology)
        {
            if (BarcodeScan != null)
                BarcodeScan(this, new BarcodeScanEvent(data, DateTime.Now, symbology));
        }

		public bool Initialize()
        {
            if (BarcodeScannerReady != null)
                BarcodeScannerReady(this, EventArgs.Empty);

            return true;
        }

        public void Start()
        {
			//wire up symbologies
        }

        public void Stop()
        {
        }

        public void Terminate()
        {
        }
        public bool NeedsStartToScan
        {
            get { return false; }
        }

        public event EventHandler BarcodeScannerReady;
        public event EventHandler<BarcodeScanEvent> BarcodeScan;

        public bool IsSymbologyAvailable(Symbology symbology)
        {
            return _available.Contains(symbology);
        }

        public bool EnableSymbology(Symbology symbology)
        {
            if (_available.Contains(symbology)) {
                _enabled.Add(symbology);
                return true;
            }

            return false;
        }

        public bool DisableSymbology(Symbology symbology)
        {
            if (_available.Contains(symbology))
            {
                _enabled.Remove(symbology);
                return true;
            }

            return false;
        }

        public bool IsSymbologyEnabled(Symbology symbology)
        {
            return _enabled.Contains(symbology);
        }

        public bool EnableSymbologies(List<Symbology> symbologies)
        {
            if (symbologies.Count == 0)
            {
                throw new ArgumentException("You haven't specified any symbologies", "symbologies");
            }

            _enabled.Clear();
            foreach (Symbology symbology in symbologies)
            {
                if (_available.Contains(symbology))
                    _enabled.Add(symbology);
            }

            return true;
        }
    }
}

