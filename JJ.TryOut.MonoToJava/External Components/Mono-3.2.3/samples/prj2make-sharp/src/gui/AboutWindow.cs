// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Library General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.

// created on 3/15/04 at 1:50 a
using System;
using Gtk;
using GtkSharp;

namespace Mfconsulting.General.Prj2Make.Gui
{
    public class AboutWindow : Window
    {
    	static GLib.GType gtype;
    		
    	public static new GLib.GType GType
    	{
    		get
    		{
    			if (gtype == GLib.GType.Invalid)
    				gtype = RegisterGType (typeof (AboutWindow));
    			return gtype;
    		}
    	}
    	
    	protected void MyInitializeComponents()
    	{
    		System.Text.StringBuilder AuthorStringBuild = new System.Text.StringBuilder ();
    		String []authors = new String[]	{
    			"Paco Martinez",
    			"Matt Gutierrez"
    		};
    		
    
    		AuthorStringBuild.Append ("prj2make-sharp\n\n");
    		AuthorStringBuild.Append ("Workspace to makefile generator.\n");
    		AuthorStringBuild.Append ("MFConsulting (c) 2004\n\n");
    		AuthorStringBuild.AppendFormat (
    			"Authors:\n\t{0}\n\t{1}",
    			authors[0], authors[1]
    			); 
    		
    		this.WidthRequest = 200;
    		this.HeightRequest = 200;
    		this.WindowPosition = WindowPosition.CenterOnParent;
    		this.AllowGrow = false;
    		this.AllowShrink = false;
    		this.DestroyWithParent = true;
    		
    		Gtk.VBox ControlPanel = new Gtk.VBox (false, 2);
    		Gtk.Label authNameLabel = new Gtk.Label (AuthorStringBuild.ToString ());
    
    		Gtk.Button okButton = new Gtk.Button ("_OK");
    		okButton.Clicked += new EventHandler (OnokButton_Click);
    		
    		ControlPanel.PackStart (authNameLabel, true, false, 10);
    		ControlPanel.PackStart (okButton, true, false, 10);
    		
    		this.Add (ControlPanel);
    	}
    	
    	void OnokButton_Click(object o, EventArgs args)
    	{
    		this.Destroy ();
    	}
    
    	public AboutWindow () : base (GType)
    	{
    		MyInitializeComponents();
    		// this.ShowAll ();
    	}	
    }   
}