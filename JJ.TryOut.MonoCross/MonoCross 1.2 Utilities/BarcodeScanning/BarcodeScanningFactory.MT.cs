using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.UIKit;
using System.Runtime.InteropServices;

namespace MonoCross.Utilities.BarcodeScanning
{
    public class BarcodeScannerFactory
    {
		
        public static IBarcodeScanner Create ()
        {
            DeviceHardware dhw = new DeviceHardware ();
   
			var d = DeviceHardware.Version;
   
			switch (d)
			{
            case DeviceHardware.HardwareVersion.iPod2G:
            case DeviceHardware.HardwareVersion.iPod3G:
                return LineaPro.GetInstance ();
                break;
            default:
                //return RedLaserMT.GetInstance();
                return null;
			}

        }
		public static IBarcodeScanner Create (UIWebView webView)
        {
		    DeviceHardware dhw = new DeviceHardware ();

			var d = DeviceHardware.Version;

			switch (d)
			{
		    case DeviceHardware.HardwareVersion.iPod2G:
		    case DeviceHardware.HardwareVersion.iPod3G:
		        return LineaPro.GetInstance (webView);
		        break;
		    default:
                return null;
				//return RedLaserMT.GetInstance();
				//break;
			}

        }
		
	}
	
public class DeviceHardware
{
	public const string HardwareProperty = "hw.machine";

	public enum HardwareVersion
	{
		iPhone1G,
		iPhone2G,
		iPhone3G,
		iPhone4G,
		iPod1G,
		iPod2G,
		iPod3G,
		iPod4G,
		Simulator,
		Unknown
	}

	[DllImport(MonoTouch.Constants.SystemLibrary)]
	internal static extern int sysctlbyname( [MarshalAs(UnmanagedType.LPStr)] string property, // name of the property
	                                        IntPtr output, // output
	                                        IntPtr oldLen, // IntPtr.Zero
	                                        IntPtr newp, // IntPtr.Zero
	                                        uint newlen // 0
	                                       );

	public static HardwareVersion Version
	{
		get
		{
			// get the length of the string that will be returned
			var pLen = Marshal.AllocHGlobal(sizeof(int));
			sysctlbyname(DeviceHardware.HardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);

			var length = Marshal.ReadInt32(pLen);

			// check to see if we got a length
			if (length == 0)
			{
				Marshal.FreeHGlobal(pLen);
				return HardwareVersion.Unknown;
			}

			// get the hardware string
			var pStr = Marshal.AllocHGlobal(length);
			sysctlbyname(DeviceHardware.HardwareProperty, pStr, pLen, IntPtr.Zero, 0);

			// convert the native string into a C# string
			var hardwareStr = Marshal.PtrToStringAnsi(pStr);
			var ret = HardwareVersion.Unknown;

			// determine which hardware we are running
			if (hardwareStr == "iPhone1,1")
				ret = HardwareVersion.iPhone1G;
			else if (hardwareStr == "iPhone1,2")
				ret = HardwareVersion.iPhone2G;
			else if (hardwareStr == "iPhone2,1")
				ret = HardwareVersion.iPhone3G;
			else if (hardwareStr == "iPhone2,2")
				ret = HardwareVersion.iPhone3G;
			else if (hardwareStr == "iPod1,1")
				ret = HardwareVersion.iPod1G;
			else if (hardwareStr == "iPod2,1")
				ret = HardwareVersion.iPod2G;
			else if (hardwareStr == "iPod3,1")
				ret = HardwareVersion.iPod3G;
			else if (hardwareStr == "i386")
				ret = HardwareVersion.Simulator;

			// cleanup
			Marshal.FreeHGlobal(pLen);
			Marshal.FreeHGlobal(pStr);

			return ret;
		}
	}
}

}

