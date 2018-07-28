using System;
using MonoTouch.UIKit;
using LineaSDK;

namespace MonoCross.Utilities.BarcodeScanning
{
	public class LineaProDelegate : LineaSDK.LineaDelegate
	{	
		private UIWebView _webView;
		
		public LineaProDelegate()
		{
			
		}
		
		public LineaProDelegate(UIWebView webView)
		{
			_webView = webView;
		}
		
		public override void ConnectionStateChanged (int state)
		{
			System.Diagnostics.Debug.WriteLine("ConnectionStateChanged: " + state.ToString());
			string stateString = string.Empty;
			switch (state)
			{
				case 0:
					stateString = "DISCONNECTED";
					break;
				case 1:
					stateString = "CONNECTING";
					break;
				case 2:
					stateString = "CONNECTED";
					break;
				default:
					break;
				
			}
			if(_webView != null)
			{
				UIApplication.SharedApplication.InvokeOnMainThread( delegate { _webView.EvaluateJavascript("writeInfo('info','LineaPro status: " + stateString + " " + DateTime.UtcNow.TimeOfDay.ToString() + "');");} );
			}
		}
	
		public override void BarcodeDataReceived (string barcode, int type)
		{
			//switch on Symbology (second param)

			if(LineaPro.Instance != null)
				LineaPro.Instance.TriggerScan(barcode, Symbology.UNKNOWN);	
			
			try
			{
				string writeNow = String.Format("scan.onScanSuccess('{0}','{1}','{2}');", barcode, type, DateTime.Now);
				UIApplication.SharedApplication.InvokeOnMainThread( delegate { _webView.EvaluateJavascript(writeNow);} );			
			}
			catch
			{
				UIApplication.SharedApplication.InvokeOnMainThread( delegate { _webView.EvaluateJavascript("scan.onScanFail();");} );
			}
		}

	}

}

