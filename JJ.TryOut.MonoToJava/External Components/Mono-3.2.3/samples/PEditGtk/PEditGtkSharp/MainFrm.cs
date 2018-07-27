using System;
using System.Xml;
using Gtk;

namespace Mfconsulting.General.PEdit
{
	public class MainFrm : Gtk.Window
	{
		public string WindowTitle = "PEditGtkSharp";
		private VBox m_MainFrmPanel;
		private VBox m_MenuAndToolbarPanel;
		private MenuBar m_MainMenuBar;
		private Toolbar m_MainToolbar;
		private Statusbar m_StatusBar;
		private AccelGroup m_AccelGroup;
		private PEditGtkSharpView m_EditView;
		private PEditGtkSharpDoc m_Document;
		private int m_nDocNum = 1;
		private Gtk.Label m_CursorPosition;
		
		// The main edit view
		public PEditGtkSharpView EditView
		{
			get { return m_EditView; }
		}
		
		public MainFrm() : base ("PEditGtkSharp")
		{
			InitializeComponents();
			this.ShowAll();
			this.CreateNewDocument();
		}
		
		protected void InitializeComponents()
		{
			// Create a new instance of the global
			// Accelerator group
			m_AccelGroup = new AccelGroup();

			// MainFrm
			// Set the Windows characteristics
			this.Title = this.WindowTitle;
			this.SetDefaultSize (400, 300);
			this.AddAccelGroup(m_AccelGroup);
			this.DeleteEvent += new DeleteEventHandler(OnWindowDelete);

			// MainFrmPanel
			m_MainFrmPanel = new VBox(false, 0);

			// m_MenuAndToolbarPanel
			m_MenuAndToolbarPanel = new VBox(false, 0);
			
			#region Menu bar

			m_MainMenuBar = new Gtk.MenuBar();
			
			// File menu
			Menu FileMenu = new Menu();
			MenuItem FileMenuItem = new MenuItem("_File");
			FileMenuItem.Submenu = FileMenu;
			FileMenu.AccelGroup = m_AccelGroup;

			// Edit menu
			Menu EditMenu = new Menu();
			MenuItem EditMenuItem = new MenuItem("_Edit");
			EditMenuItem.Submenu = EditMenu;
			EditMenu.AccelGroup = m_AccelGroup;
    		
			// Help menu
			Menu HelpMenu = new Menu();
			MenuItem HelpMenuItem = new MenuItem("_Help");
			HelpMenuItem.Submenu = HelpMenu;
			HelpMenu.AccelGroup = m_AccelGroup;
    		
			// File New menu item
			Gtk.ImageMenuItem NewMenuItem = new ImageMenuItem("gtk-new", m_AccelGroup);
			NewMenuItem.Activated += new EventHandler(NewMenuItem_Activated);
			FileMenu.Append(NewMenuItem);
    
			// File Open menu item
			Gtk.ImageMenuItem OpenMenuItem = new ImageMenuItem("gtk-open", m_AccelGroup);
			OpenMenuItem.Activated += new EventHandler(OpenMenuItem_OnActivate);
			FileMenu.Append(OpenMenuItem);

			// File Save menu item
			Gtk.ImageMenuItem SaveMenuItem = new ImageMenuItem("gtk-save", m_AccelGroup);
			SaveMenuItem.Activated += new EventHandler(SaveMenuItem_Activated);
			FileMenu.Append(SaveMenuItem);

			// File SaveAs menu item
			Gtk.ImageMenuItem SaveAsMenuItem = new ImageMenuItem("gtk-save-as", m_AccelGroup);
			SaveAsMenuItem.Activated += new EventHandler(SaveAsMenuItem_Activated);
			FileMenu.Append(SaveAsMenuItem);

			// File Separator menu item
			SeparatorMenuItem Separator1MenuItem = new SeparatorMenuItem();
			FileMenu.Append(Separator1MenuItem);

			// File Exit menu item
			Gtk.ImageMenuItem ExitMenuItem = new ImageMenuItem("gtk-quit", m_AccelGroup);
			ExitMenuItem.Activated += new EventHandler(ExitMenuItem_OnActivate);
			FileMenu.Append(ExitMenuItem);

			// Edit Cut menu item
			Gtk.ImageMenuItem CutMenuItem = new ImageMenuItem("gtk-cut", m_AccelGroup);
			// CutMenuItem.Activated += new EventHandler(CutMenuItem_OnActivate);
			CutMenuItem.Activated += new EventHandler(CutMenuItem_Activated);
			EditMenu.Append(CutMenuItem);

			// Edit Copy menu item
			Gtk.ImageMenuItem CopyMenuItem = new ImageMenuItem("gtk-copy", m_AccelGroup);
			CopyMenuItem.Activated += new EventHandler(CopyMenuItem_Activated);
			EditMenu.Append(CopyMenuItem);

			// Edit Paste menu item
			Gtk.ImageMenuItem PasteMenuItem = new ImageMenuItem("gtk-paste", m_AccelGroup);
			PasteMenuItem.Activated += new EventHandler(PasteMenuItem_OnActivate);
			EditMenu.Append(PasteMenuItem);

			// Help About menu item
			MenuItem AboutMenuItem = new MenuItem("_About...");
			// AboutMenuItem.Activated += new EventHandler(AboutMenuItem_OnActivate);
			AboutMenuItem.Activated += new EventHandler(AboutMenuItem_Activated);
			HelpMenu.Append(AboutMenuItem);

			// Add the menus to the menubar		
			m_MainMenuBar.Append(FileMenuItem);
			m_MainMenuBar.Append(EditMenuItem);
			m_MainMenuBar.Append(HelpMenuItem);

			// Add the menubar to the Menu and Toolbar panel
			m_MenuAndToolbarPanel.PackStart(m_MainMenuBar, false, false, 0);
    		
			#endregion
			
			#region Toolbar
			
			m_MainToolbar = new Toolbar();
			
			// Toolbar buttons
			Gtk.ToolButton NewToolBarBtn = new Gtk.ToolButton("gtk-new");
			NewToolBarBtn.Clicked += new EventHandler(NewToolBarBtn_Clicked);

			Gtk.ToolButton OpenToolBarBtn = new Gtk.ToolButton ("gtk-open");
			OpenToolBarBtn.Clicked +=new EventHandler(OpenToolBarBtn_Clicked);

			Gtk.ToolButton SaveToolBarBtn = new Gtk.ToolButton("gtk-save");
			// SaveToolBarBtn.Clicked +=new EventHandler(SaveToolBarBtn_Clicked);
			SaveToolBarBtn.Clicked += new EventHandler(SaveToolBarBtn_Clicked);

			Gtk.ToolButton CutToolBarBtn = new Gtk.ToolButton("gtk-cut");
			CutToolBarBtn.Clicked += new EventHandler(CutToolBarBtn_Clicked);
			// CutToolBarBtn.Clicked +=new EventHandler(CutToolBarBtn_Clicked);

			Gtk.ToolButton CopyToolBarBtn = new Gtk.ToolButton("gtk-copy");
			CopyToolBarBtn.Clicked += new EventHandler(CopyToolBarBtn_Clicked);

			Gtk.ToolButton PasteToolBarBtn = new Gtk.ToolButton("gtk-paste");
			PasteToolBarBtn.Clicked += new EventHandler(PasteToolBarBtn_Clicked);
			
			// Add the buttons to the toolbar
			
			// File operations buttons
			m_MainToolbar.Add(NewToolBarBtn);
			m_MainToolbar.Add(OpenToolBarBtn);
			m_MainToolbar.Add(SaveToolBarBtn);
			
			// Insert Separator
			m_MainToolbar.Add(new Gtk.SeparatorToolItem());
			
			// Add the editing buttons
			m_MainToolbar.Add(CutToolBarBtn);
			m_MainToolbar.Add(CopyToolBarBtn);
			m_MainToolbar.Add(PasteToolBarBtn);
			
			// Add the toolbar to the Menu and Toolbar panel
			m_MenuAndToolbarPanel.PackStart(m_MainToolbar, false, false, 0);
    		
			#endregion
			
			#region Edit View
			
			m_EditView = new PEditGtkSharpView();
			m_EditView.EditView.Buffer.Changed +=new EventHandler(Buffer_Changed);
			m_EditView.EditView.Buffer.InsertText += new InsertTextHandler(Buffer_InsertText);
			m_EditView.EditView.Buffer.MarkSet += new MarkSetHandler(Buffer_MarkSet);
			m_EditView.EditView.MoveCursor += new MoveCursorHandler(EditView_MoveCursor);

			
			#endregion

			#region Status bar
			
			m_CursorPosition = new Label("Ln 0 Col 0");

			m_StatusBar = new Statusbar();			
			m_StatusBar.PackEnd(m_CursorPosition, false, true, 30);
			
			#endregion
			
			// Add the MenuAndToolBar panel to the Main Form panel
			m_MainFrmPanel.PackStart(m_MenuAndToolbarPanel, false, false, 0);

			// Add the EditView Main Form panel
			m_MainFrmPanel.PackStart(m_EditView, true, true, 2);

			// Add the Status bar to the Main Window
			m_MainFrmPanel.PackStart(m_StatusBar, false, true, 1);
			
			// Add Panels to the Main Window
			this.Add(m_MainFrmPanel);
		}
		
		private void Buffer_InsertText(object o, InsertTextArgs args)
		{
			UpdateCursorPositionIndicator();
		}

		private void Buffer_Changed(object sender, EventArgs e)
		{
			if(this.m_Document != null)
			{
				this.m_Document.IsModified = true;
				SetMainFrmTitle();
			}

			UpdateCursorPositionIndicator();
		}

		private void EditView_MoveCursor(object o, MoveCursorArgs args)
		{
			UpdateCursorPositionIndicator();
		}

		private void Buffer_MarkSet(object o, MarkSetArgs args)
		{
			this.UpdateCursorPositionIndicator();
		}
		
		public void OnWindowDelete(object o, DeleteEventArgs args) 
		{
			CheckIfModifiedAndSave();

			Application.Quit();
			args.RetVal = true;
		}
		
		
		#region Menu event handler methods

		private void NewMenuItem_Activated(object sender, EventArgs e)
		{
			CreateNewDocument();
		}

		private void OpenMenuItem_OnActivate(object o, EventArgs args)
		{
			OpenDocument();
		}		

		private void SaveMenuItem_Activated(object sender, EventArgs e)
		{
			if(this.m_Document.IsNewDocument == false)
			{
				SaveTextBufferContentToFile(this.m_Document.FileName);
				this.m_Document.IsNewDocument = false;
				this.m_Document.IsModified = false;
				SetMainFrmTitle();
			}
			else
			{
				this.SaveAs();
			}
		}

		private void SaveAsMenuItem_Activated(object sender, EventArgs e)
		{
			this.SaveAs();
		}

		private void ExitMenuItem_OnActivate(object o, EventArgs args)
		{
			CheckIfModifiedAndSave();

			Application.Quit();
		}
		
		private void CutMenuItem_Activated(object sender, EventArgs e)
		{
			this.CutToClipboard();
		}

		private void CopyMenuItem_Activated(object sender, EventArgs e)
		{
			this.CopyToClipboard();
		}
		
		private void PasteMenuItem_OnActivate(object sender, EventArgs e)
		{
			PasteFromClipboard();
		}

		private void AboutMenuItem_Activated(object sender, EventArgs e)
		{
			PEditGtkSharpAbout AboutDlg = new PEditGtkSharpAbout(this);
			AboutDlg.Authors = new string[] {"Francisco T. Martinez", "Jose R. Martinez"};
			int nRc = AboutDlg.Run();

			AboutDlg.Hide();

			if(nRc == (int)ResponseType.Ok) 
			{
			}
		}
		
		#endregion
		
		#region Toolbar buttons event handler methods

		private void NewToolBarBtn_Clicked(object sender, EventArgs e)
		{
			CreateNewDocument();
		}
		
		// File Open clicked event
		private void OpenToolBarBtn_Clicked(object sender, EventArgs e)
		{
			OpenDocument();
		}
		
		private void SaveToolBarBtn_Clicked(object sender, EventArgs e)
		{
			if(this.m_Document.IsNewDocument == false)
			{
				SaveTextBufferContentToFile(this.m_Document.FileName);
				this.m_Document.IsNewDocument = false;
				this.m_Document.IsModified = false;
				SetMainFrmTitle();
			}
			else
			{
				this.SaveAs();
			}
		}


		private void CutToolBarBtn_Clicked(object sender, EventArgs e)
		{
			this.CutToClipboard();
		}

		private void CopyToolBarBtn_Clicked(object sender, EventArgs e)
		{
			this.CopyToClipboard();
		}

		private void PasteToolBarBtn_Clicked(object sender, EventArgs e)
		{
			this.PasteFromClipboard();
		}

		#endregion
		
		#region Common methods

		// New Document
		protected void CreateNewDocument()
		{
			if(this.m_Document != null)
			{
				if(this.m_Document.IsModified == true)
				{
					// Offer to save
					// Prompt to save changes
					// Use Yes No Cancel
					MessageDialog md = new MessageDialog(
						this,
						DialogFlags.DestroyWithParent,
						MessageType.Info,
						ButtonsType.YesNo, 
						String.Format("{0} has been modified.\nDo you wish to save?", 
						System.IO.Path.GetFileName(this.m_Document.FileName)
						)
						);
            
					md.Modal = true;
					Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();            
					md.Hide();
					if(result == Gtk.ResponseType.Yes)
					{
						// Save away
						this.SaveTextBufferContentToFile(this.m_Document.FileName);
					}
				}
			}

			LoadNewDocumentAndClearBuffer();
			return;
		}

		// Does the Open Document Logic
		protected void OpenDocument()
		{
			string strFileName = this.SelectFile();
			
			// Simple check to bail or not
			if(strFileName.Length < 1)
			{
				return;
			}

			if(this.m_Document == null)
			{
				this.m_Document = new PEditGtkSharpDoc();
				LoadDocumentAndBuffer(strFileName);
				return;
			}
			else
			{
				this.CheckIfModifiedAndSave();
				LoadDocumentAndBuffer(strFileName);
				return;
			}
		}

		// Set the Main Window Title bar
		protected void SetMainFrmTitle()
		{
			this.Title = String.Format("{0} - {1}{2}",
				WindowTitle,
				this.m_Document.FileName,
				(this.m_Document.IsModified == true) ? "*" : ""
				);
		}
		
		// Selects a file using the FileSelection dialog for
		// the Save As menu functionality
		protected string SaveAsSelectFile(string strFileName)
		{
			FileSelection fDlg = new FileSelection("Save file as");

            fDlg.Filename = strFileName;

			int nRc = fDlg.Run();
			fDlg.Hide();

			if(nRc == (int)ResponseType.Ok) 
			{
				return fDlg.Filename;
			}
			
			return "";
		}

		// Selects a file using the FileSelection dialog 
		protected string SelectFile()
		{
			FileSelection fDlg = new FileSelection("Choose a file");

			int nRc = fDlg.Run();
			fDlg.Hide();

			if(nRc == (int)ResponseType.Ok) 
			{
				return fDlg.Filename;
			}
			
			return "";
		}

		// Save As Logic
		protected void SaveAs()
		{
			string strFileName = SaveAsSelectFile(this.m_Document.FileName);
			if(strFileName.Length > 0)
			{
				SaveTextBufferContentToFile(strFileName);
				this.m_Document.FileName = strFileName;
				this.m_Document.IsNewDocument = false;
				this.m_Document.IsModified = false;
				SetMainFrmTitle();
			}
		}

		// Saves the contents of the TextBuffer to the
		// specified file.
		protected void SaveTextBufferContentToFile(string strFileName)
		{
			try
			{
				this.m_Document.SaveDocument(
					strFileName,
					this.EditView.EditView.Buffer.Text
					);
				this.m_Document.IsModified = false;
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
			}

			return;
		}

		protected void CheckIfModifiedAndSave()
		{
			if(this.m_Document.IsModified == true)
			{
				// Offer to save
				// Prompt to save changes
				// Use Yes No Cancel
				MessageDialog md = new MessageDialog(
					this,
					DialogFlags.DestroyWithParent,
					MessageType.Info,
					ButtonsType.YesNo, 
					String.Format("{0} has been modified.\nDo you wish to save?", 
					System.IO.Path.GetFileName(this.m_Document.FileName)
					)
					);
            
				md.Modal = true;
				Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();            
				md.Hide();
				if(result == Gtk.ResponseType.Yes)
				{
					// Save away
					this.SaveTextBufferContentToFile(this.m_Document.FileName);
				}
			}
		}

		// Loads a new document and clears the buffer
		protected void LoadNewDocumentAndClearBuffer()
		{
			this.m_EditView.EditView.Buffer.Clear();
			this.m_Document = new PEditGtkSharpDoc();
			this.m_Document.FileName = String.Format(
				"document{0}.asc",
				this.m_nDocNum++
				);
			this.m_Document.IsNewDocument = true;
			SetMainFrmTitle();
		}

		// Opens the specified file as a UTF-8 text file
		// and loads it in the TextView's Buffer
		protected void LoadDocumentAndBuffer(string strFileName)
		{
			this.m_Document.OpenDocumentForRead(strFileName);
                
			this.EditView.EditView.Buffer.Text = this.m_Document.TextStream.ReadToEnd();
			this.m_Document.IsNewDocument = false;
			this.m_Document.IsModified = false;

			this.m_Document.TextStream.Close();

			SetMainFrmTitle();
			return;
		}

		// Update the status bar display to reflect the cursor's position
		public void UpdateCursorPositionIndicator()
		{
			Gtk.TextIter curTextIter = this.m_EditView.EditView.Buffer.GetIterAtMark(this.m_EditView.EditView.Buffer.InsertMark);
			this.SetLineCol(curTextIter.Line + 1,
				curTextIter.LineOffset + 1
				);
		}

		protected void SetLineCol(int Line, int Column)
		{
			this.m_CursorPosition.Text = String.Format("Ln {0} Col {1}", Line, Column);
		}

		protected void CutToClipboard()
		{
			Gtk.TextBuffer buff = this.m_EditView.EditView.Buffer;
			Gtk.Clipboard clp = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", true));

			buff.CutClipboard(clp, true);
		}

		protected void CopyToClipboard()
		{
			Gtk.TextBuffer buff = this.m_EditView.EditView.Buffer;
			Gtk.Clipboard clp = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", true));

			buff.CopyClipboard(clp);
		}

		protected void PasteFromClipboard()
		{
			Gtk.TextBuffer buff = this.m_EditView.EditView.Buffer;
			Gtk.Clipboard clp = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", true));

			buff.PasteClipboard(clp);
		}

		#endregion

	}
}
