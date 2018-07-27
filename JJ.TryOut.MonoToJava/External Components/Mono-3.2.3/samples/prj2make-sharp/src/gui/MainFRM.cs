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

using System;
using System.IO;
using Gtk;
using GtkSharp;

namespace Mfconsulting.General.Prj2Make.Gui
{
    public class MainFRM : Gtk.Window 
    {
    	static GLib.GType gtype;
    	public string m_AppName = "prj2make-sharp-gtk";
    	public string m_WorkspaceFileName;
    	public string m_FileNamePath;
    	public string m_OutputMakefile;
    	
    
    	private bool m_IsUnix = false;
    	private bool m_IsMcs = true;
    	private VBox m_FrmPanel = null;
    	private VBox m_FileInfoPanel = null;
    	private VBox m_MenuPanel = null;
    	private MenuBar m_MainMenuBar = null;
    	protected Gtk.Entry m_FileNameEntry = null;
    	protected Gtk.Entry m_FileNameEntry2 = null;
    	protected Gtk.RadioButton m_rdo1 = null; 
    	protected Gtk.RadioButton m_rdo2 = null;
    	protected Gtk.RadioButton m_rdo3 = null; 
    	protected Gtk.RadioButton m_rdo4 = null;
    	protected Gtk.TextView m_TextView = null;
    	protected Gtk.TextBuffer m_TextBuff = null;
    	
    	// Determines the make file type/style
    	public bool IsUnix
    	{
    		get { return m_IsUnix; }
    		set { m_IsUnix = value; }
    	}
    	
    	// Determines what compiler to use MCS or CSC
    	public bool IsMcs
    	{
    		get { return m_IsMcs; }
    		set { m_IsMcs = value; }
    	}
    	
    	public static new GLib.GType GType
    	{
    		get
    		{
    			if (gtype == GLib.GType.Invalid)
    				gtype = RegisterGType (typeof (MainFRM));
    			return gtype;
    		}
    	}
    	
    	protected void MyInitializeComponents()
    	{
    		// Set the Windows characteristics
    		this.Title = m_AppName; 
    		this.SetDefaultSize (400, 300);
    		this.DeleteEvent += new DeleteEventHandler (OnMyWindowDelete);
    
    		// Vertical panel that host all other panels
    		m_FrmPanel = new VBox(false, 2);
    		
    		#region Menus
    		
    		// Vertical panel that will host the menubar
    		m_MenuPanel = new VBox(false, 2);
    
    		// Menu bar configuration
    		m_MainMenuBar = new MenuBar();
    
    		// File menu
    		Menu FileMenu = new Menu();
    		MenuItem FileMenuItem = new MenuItem("_File");
    		FileMenuItem.Submenu = FileMenu;
    		
    		// Open menu item
    		MenuItem OpenMenuItem = new MenuItem("_Open");
    		OpenMenuItem.Activated += new EventHandler(OpenMenuItem_OnActivate);
    		FileMenu.Append(OpenMenuItem);
    		
    		// Save menu item
    		MenuItem SaveMenuItem = new MenuItem("_Save");
    		SaveMenuItem.Activated += new EventHandler(SaveMenuItem_OnActivate);
    		FileMenu.Append(SaveMenuItem);
    		
    		// SaveAs menu item
    		MenuItem SaveAsMenuItem = new MenuItem("Save _As");
    		SaveAsMenuItem.Activated += new EventHandler(SaveAsMenuItem_OnActivate);
    		FileMenu.Append(SaveAsMenuItem);
    		
    		// Separator menu item
    		SeparatorMenuItem Separator1MenuItem = new SeparatorMenuItem();
    		FileMenu.Append(Separator1MenuItem);
    		
    		// Exit menu item
    		MenuItem ExitMenuItem = new MenuItem("E_xit");
    		ExitMenuItem.Activated += new EventHandler(ExitMenuItem_OnActivate);
    		FileMenu.Append(ExitMenuItem);
    		
    		// Help menu
    		Menu HelpMenu = new Menu();
    		MenuItem HelpMenuItem = new MenuItem("_Help");
    		HelpMenuItem.Submenu = HelpMenu;
    		
    		// About menu item
    		MenuItem AboutMenuItem = new MenuItem("_About...");
    		AboutMenuItem.Activated += new EventHandler(AboutMenuItem_OnActivate);
    		HelpMenu.Append(AboutMenuItem);
    
    		// Tools menu
    		Menu ToolsMenu = new Menu();
    		MenuItem ToolsMenuItem = new MenuItem("_Tools");
    		ToolsMenuItem.Submenu = ToolsMenu;
    		
    		// Execute menu item
    		MenuItem ExecuteMenuItem = new MenuItem("E_xecute");
    		ExecuteMenuItem.Activated += new EventHandler(ExecuteMenuItem_OnActivate);
    		ToolsMenu.Append(ExecuteMenuItem);
    
    		// Add the menus to the menubar		
    		m_MainMenuBar.Append(FileMenuItem);
    		m_MainMenuBar.Append(ToolsMenuItem);
    		m_MainMenuBar.Append(HelpMenuItem);
    		
    		// Add the menubar to the panel
    		m_MenuPanel.PackStart(m_MainMenuBar, false, false, 0);
    		
    		#endregion
    		
    		#region FileInfo Panel
    		
    		// Vertical panel that host all file details
    		m_FileInfoPanel = new VBox(false, 2);
    		Gtk.HBox FileNamePanel = new HBox (false, 2);
    		Gtk.HBox RadioButtonsPanel = new HBox (false, 2);
    		Gtk.HBox RadioButtonsPanel2 = new HBox (false, 2);
    		
    		Gtk.Label FileNameLabel = new Gtk.Label("Workspace file name:");
    		m_FileNameEntry = new Gtk.Entry();
    		
    		FileNamePanel.PackStart (FileNameLabel, false, false, 10);
    		FileNamePanel.PackStart (m_FileNameEntry, true, true, 10);
    		
    		m_FileInfoPanel.PackStart (FileNamePanel, false, false, 10);
    
    		// Groupbox and Radio buttons
    		Gtk.HBox GroupBoxPanel = new HBox (false, 2);
    		Gtk.Frame MakeStyleFrame = new Gtk.Frame("Output type");
    		
    		MakeStyleFrame.HeightRequest = 70;
    		MakeStyleFrame.WidthRequest = 80;
    		MakeStyleFrame.BorderWidth = 10;
    		
    		m_rdo1 = new Gtk.RadioButton("nmake");
    		m_rdo2 = new Gtk.RadioButton(m_rdo1, "gmake");
    		
    		m_rdo1.Clicked += new EventHandler(Rdo1_OnClik);
    		m_rdo2.Clicked += new EventHandler(Rdo2_OnClik);
    
    		RadioButtonsPanel.PackStart (m_rdo1, false, false, 2);
    		RadioButtonsPanel.PackStart (m_rdo2, false, false, 2);
    
    		MakeStyleFrame.Add(RadioButtonsPanel);
    
    		Gtk.Frame MakeStyleFrame2 = new Gtk.Frame("Compiler");
    		
    		MakeStyleFrame2.HeightRequest = 70;
    		MakeStyleFrame2.WidthRequest = 80;
    		MakeStyleFrame2.BorderWidth = 10;
    		
    		m_rdo3 = new Gtk.RadioButton("mcs");
    		m_rdo4 = new Gtk.RadioButton(m_rdo3, "csc");
    		
    		m_rdo3.Clicked += new EventHandler(Rdo3_OnClik);
    		m_rdo4.Clicked += new EventHandler(Rdo4_OnClik);
    
    		RadioButtonsPanel2.PackStart (m_rdo3, false, false, 2);
    		RadioButtonsPanel2.PackStart (m_rdo4, false, false, 2);
    
    		MakeStyleFrame2.Add(RadioButtonsPanel2);
    		
    		GroupBoxPanel.PackStart (MakeStyleFrame, true, true, 2); 
    		GroupBoxPanel.PackStart (MakeStyleFrame2, true, true, 2); 
    		m_FileInfoPanel.PackStart (GroupBoxPanel, false, false, 2);
    		
    		#endregion
    		
    		#region TextView initialization
    		
    		Gtk.Frame ScrolledWindowFrm = new Gtk.Frame();
    		Gtk.ScrolledWindow TxtViewWin = new Gtk.ScrolledWindow();
    
    		m_TextView = new Gtk.TextView();
    		m_TextView.WidthRequest = 200;
    		m_TextView.HeightRequest = 150;
    		
    		m_TextBuff = m_TextView.Buffer;
    		
    		TxtViewWin.Add(m_TextView);
    		
    		ScrolledWindowFrm.BorderWidth = 10;
    		ScrolledWindowFrm.Add(TxtViewWin);
    		
    		#endregion
    		
    		#region Output file
    		
    		// Vertical panel that host all file details
    		Gtk.HBox FileNamePanel2 = new HBox (false, 2);
    		
    		Gtk.Label FileNameLabel2 = new Gtk.Label("Makefile name:");
    		m_FileNameEntry2 = new Gtk.Entry();
    		
    		FileNamePanel2.PackStart (FileNameLabel2, false, false, 10);
    		FileNamePanel2.PackStart (m_FileNameEntry2, true, true, 10);
    		
    		// m_FileInfoPanel.PackStart (FileNamePanel, false, false, 10);
    
    		#endregion
    		
    		// Add the panel to the main window
    		m_FrmPanel.PackStart (m_MenuPanel, false, false, 0);
    		m_FrmPanel.PackStart (m_FileInfoPanel, false, false, 2);
    		m_FrmPanel.PackStart (FileNamePanel2, false, false, 2);
    		m_FrmPanel.PackStart (ScrolledWindowFrm, true, true, 2);
    		
    		this.Add(m_FrmPanel);
    	}
    	
   		// Arrange the state of the radio buttons to
   		// match the command line args
    	protected void RadioButtonStateUpdate()
    	{
    		if(IsUnix == false)
    		{
    			m_rdo1.Active = true;
    			m_rdo2.Active = false;
    		}
    		else
    		{
    			m_rdo1.Active = false;
    			m_rdo2.Active = true;
    		}
    		
    		if(IsMcs == true)
    		{
    			m_rdo3.Active = true;
    			m_rdo4.Active = false;
    		}
    		else
    		{
    			m_rdo3.Active = false;
    			m_rdo4.Active = true;
    		}
    	}
    	
    	public MainFRM () : base (GType)
    	{
    		MyInitializeComponents();
    		this.ShowAll ();
    	}
    	
    	// For command line handling
    	public MainFRM (bool isNmake, bool isCsc) : base (GType)
    	{
    		MyInitializeComponents();
    		this.IsMcs = (isCsc == true) ? false : true;
    		this.IsUnix = (isNmake == true) ? false : true;
    		this.ShowAll ();
    		RadioButtonStateUpdate();    		
    	}
    	
    	// For command line handling
    	public MainFRM (bool isNmake, bool isCsc, string WorkspaceFile) : base (GType)
    	{
    		MyInitializeComponents();
    		this.IsMcs = (isCsc == true) ? false : true;
    		this.IsUnix = (isNmake == true) ? false : true;
    		this.ShowAll ();
    		IniFromCommandLine(WorkspaceFile);
    		RadioButtonStateUpdate();    		
    	}
    	
    	void OnMyWindowDelete (object o, DeleteEventArgs args)
    	{
    		Application.Quit ();
    	}
    	
    	#region Menu handlers
    	
    	void AboutMenuItem_OnActivate(object o, EventArgs args)
    	{
            System.Text.StringBuilder AuthorStringBuild = new System.Text.StringBuilder ();
            String []authors = new String[]	{
            	"Paco Martinez <paco@mfcon.com>",
            	"Jaroslaw Kowalski"
            };
            
            AuthorStringBuild.Append ("prj2make-sharp version 0.95\n\n");
            AuthorStringBuild.Append ("Workspace to makefile generator.\n");
            AuthorStringBuild.Append ("Copyright (c) 2004\n\n");
            AuthorStringBuild.AppendFormat (
            	"Authors:\n\t{0}\n\t{1}",
            	authors[0],
            	authors[1]
            	); 
            
            MessageDialog md = new MessageDialog (this,
            	DialogFlags.DestroyWithParent,
            	MessageType.Info,
            	ButtonsType.Ok, 
            	AuthorStringBuild.ToString ()
            	);
            
            int result = md.Run ();
            md.Hide();
            
    		return;
    	}
    	
    	void CreateMakefile()
    	{
    		Maker mkObj = null;
    		string slnFile = null;
    		
    		if (m_FileNameEntry.Text.Length < 1) {
                MessageDialog md = new MessageDialog (this,
    				DialogFlags.DestroyWithParent,
    				MessageType.Warning,
    				ButtonsType.Ok, 
    				"No file has been selected."
    				);
                
                int result = md.Run ();
                md.Hide();
                
    			return;            
    		}
    		else
    			slnFile = m_FileNameEntry.Text;
    		
    		mkObj = new Maker();		
    		m_TextBuff.Text = mkObj.MakerMain(IsUnix, IsMcs, slnFile);
    	}
    
    	void ExecuteMenuItem_OnActivate(object o, EventArgs args)
    	{
    		CreateMakefile();
    	}
    
    	void IniFromCommandLine(string WorkspaceFilename)
    	{
       		m_WorkspaceFileName = WorkspaceFilename;
       		m_FileNamePath = System.IO.Path.GetDirectoryName(m_WorkspaceFileName);
       		m_OutputMakefile = System.IO.Path.Combine(m_FileNamePath, "Makefile");
       
       		if (this.IsUnix)
       			m_OutputMakefile = System.IO.Path.Combine(m_FileNamePath, "Makefile");
       		else
       			m_OutputMakefile = System.IO.Path.Combine(m_FileNamePath, "Makefile.Win32");
       
       		m_FileNameEntry.Text = m_WorkspaceFileName;
       		m_FileNameEntry2.Text = m_OutputMakefile;
       
       		// Actually follow through and genrate the contents of the Makefile
       		// to be placed on the textview
       		CreateMakefile();
    	}

    	void OpenMenuItem_OnActivate(object o, EventArgs args)
    	{
    		bool isUnix = (m_rdo2.Active) ? true : false;
    		FileSelection fDlg = new FileSelection("Choose a project file.");
    		
    		if (m_FileNameEntry.Text.Length > 1)
    			fDlg.Filename = m_FileNameEntry.Text;
    
    		int nRc = fDlg.Run();
    		fDlg.Hide();
    
    		if(nRc == (int)ResponseType.Ok) {
        		m_WorkspaceFileName = fDlg.Filename;
        		m_FileNamePath = System.IO.Path.GetDirectoryName(m_WorkspaceFileName);
        		m_OutputMakefile = System.IO.Path.Combine(m_FileNamePath, "Makefile");
        
        		if (isUnix)
        			m_OutputMakefile = System.IO.Path.Combine(m_FileNamePath, "Makefile");
        		else
        			m_OutputMakefile = System.IO.Path.Combine(m_FileNamePath, "Makefile.Win32");
        
        		m_FileNameEntry.Text = m_WorkspaceFileName;
        		m_FileNameEntry2.Text = m_OutputMakefile;
        
        		// Actually follow through and genrate the contents of the Makefile
        		// to be placed on the textview
        		CreateMakefile();
        		}     
    	}
    	
    	void SaveMenuItem_OnActivate(object o, EventArgs args)
    	{
    		SaveMakefileToDisk ();
    	}
    	
    	void SaveAsMenuItem_OnActivate(object o, EventArgs args)
    	{
    		FileSelection fDlg = new FileSelection("Choose a make file.");
    		
    		fDlg.Filename = m_FileNameEntry2.Text;
    		fDlg.Run();
    		fDlg.Hide();
    		
    		m_FileNameEntry2.Text = fDlg.Filename;
    
    		SaveMakefileToDisk ();		
    	}
    	
    	void Rdo1_OnClik(object o, EventArgs args)
    	{
    		string MakeFileString = m_FileNameEntry2.Text;
    
    		m_IsUnix = false;		
    		try
            {
    			m_FileNameEntry2.Text = System.IO.Path.ChangeExtension(MakeFileString, "Win32");
				if (m_FileNameEntry.Text.Length > 0) {
					CreateMakefile();
				}
            }
            catch (Exception exc)
            {
            	Console.WriteLine (exc.Message);
    			return;        	
            }		
    	}
    	
    	void Rdo2_OnClik(object o, EventArgs args)
    	{
    		string MakeFileString = m_FileNameEntry2.Text;
    		
    		m_IsUnix = true;
    		try
            {
        		m_FileNameEntry2.Text = System.IO.Path.Combine(
        			System.IO.Path.GetDirectoryName (MakeFileString),
        			System.IO.Path.GetFileNameWithoutExtension (MakeFileString)
        			);
    				
				if (m_FileNameEntry.Text.Length > 0) {
					CreateMakefile();
				}
            }
            catch (Exception exc)
            {
            	Console.WriteLine (exc.Message);
    			return;        	
            }		        		
    	}
    	
    	void Rdo3_OnClik(object o, EventArgs args)
    	{
    		m_IsMcs = true;
			if (m_FileNameEntry.Text.Length > 0) {
				CreateMakefile();
			}
		}
    	
    	void Rdo4_OnClik(object o, EventArgs args)
    	{
    		m_IsMcs = false;
			if (m_FileNameEntry.Text.Length > 0) {
				CreateMakefile();
			}
		}
    	
    	void ExitMenuItem_OnActivate(object o, EventArgs args)
    	{ 
    		Application.Quit (); 
    	}
    	
    	#endregion
    	
    	protected void SaveMakefileToDisk ()
    	{
    		string makeFileString;
    		FileStream fs = null;
    		StreamWriter w = null;
    		
    		makeFileString = m_FileNameEntry2.Text;
    		
    		if (makeFileString != null && makeFileString.Length > 1) {
    			fs = new FileStream(makeFileString, FileMode.Create, FileAccess.Write);
    			w = new StreamWriter(fs);
    		}
    		
    		if (w != null) {
    			w.WriteLine (m_TextBuff.Text);
    			w.Close();
    		}
    	}
    	
    }
    
}
