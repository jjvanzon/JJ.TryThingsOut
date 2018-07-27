using System;
using Gtk;

namespace Mfconsulting.General.PEdit
{
	/// <summary>
	/// Summary description for PEditGtkSharpAbout.
	/// </summary>
	public class PEditGtkSharpAbout : Gtk.Dialog
	{
		private Gtk.Label m_AppNameLabel = null;
		private Gtk.Label m_AuthorsLabel = null;
		private Gtk.Label m_CopyRightLabel = null;
		private Gtk.VBox m_DialogContent = null;

		public string []Authors
		{
			set 
			{ 
				System.Text.StringBuilder strbAuthors = new System.Text.StringBuilder();
				string []straAuthors = value; 
				foreach(string strAuthor in straAuthors)
				{
					strbAuthors.AppendFormat("{0}\n", strAuthor);
				}
				m_AuthorsLabel.Text = strbAuthors.ToString();
			}
		}

		private void InitializeComponents()
		{
			this.Modal = true;
			this.DefaultResponse = Gtk.ResponseType.Ok;
			this.AddButton("gtk-ok", Gtk.ResponseType.Ok);
			this.HeightRequest = 200;
			this.WidthRequest = 210;

			// Application Name
			this.m_AppNameLabel = new Label("PEditGtkSharp");
			
			// Authors
			this.m_AuthorsLabel = new Label();

			// Copy Rght
			this.m_CopyRightLabel = new Label("(c) 2004 MFConsulting\n");

			// Main dialog content container
			m_DialogContent = new VBox(true, 2);
			m_DialogContent.PackStart(m_AppNameLabel, true, true, 1);
			m_DialogContent.PackStart(m_AuthorsLabel, true, true, 1);
			m_DialogContent.PackStart(m_CopyRightLabel, true, true, 1);

			this.VBox.PackStart(this.m_DialogContent, true, true, 0);
		}

		public PEditGtkSharpAbout()
		{
			this.Title = "PEditGtkSharp";
			this.InitializeComponents();
			this.ShowAll();
		}

		public PEditGtkSharpAbout(Gtk.Window winParent) : base ("PEditGtkSharp", winParent, Gtk.DialogFlags.DestroyWithParent)
		{
			this.InitializeComponents();
			this.ShowAll();
		}
	}
}
