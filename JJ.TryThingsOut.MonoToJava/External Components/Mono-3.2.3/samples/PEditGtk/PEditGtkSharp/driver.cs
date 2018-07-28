using System;
using System.Xml;
using Gtk;

namespace Mfconsulting.General.PEdit
{
	public class Driver
	{
		private MainFrm m_windMainFrm = null;
		
		Driver()
		{
			Application.Init();
			m_windMainFrm = new MainFrm();
			
			Application.Run();			
		}
		
		public static void Main(string []args)
		{
			new Driver();
			return;
		}
	}
}
