using System;
using Gtk;

namespace Mfconsulting.General.PEdit
{
	public class PEditGtkSharpView : Gtk.ScrolledWindow
	{
		private Gtk.TextView m_EditView = null;
		private PEditGtkSharpDoc m_Document = null;
		
		public PEditGtkSharpDoc Document
		{
			get { return m_Document; }
			set { m_Document = value; }
		}
		
		public Gtk.TextView EditView
		{
			get { return m_EditView; }
		}
		
		// Initialization of compnents and their
		// preliminary attribute settings
		protected void InitializeComponents()
		{
			// Instantiation of the TextView
			m_EditView = new Gtk.TextView();
			
			// Settings of Height and Width
			m_EditView.WidthRequest = 400;
			m_EditView.HeightRequest = 300;
			
			// Embedd in the ScrollWindow
			this.Add(m_EditView);
		}
	
		public PEditGtkSharpView()
		{
			InitializeComponents();
		}
	}
}

