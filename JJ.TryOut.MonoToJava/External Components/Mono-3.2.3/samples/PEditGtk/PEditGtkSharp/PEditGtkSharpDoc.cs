using System;
using System.IO;

namespace Mfconsulting.General.PEdit
{
	public class PEditGtkSharpDoc
	{
		private string m_strFileName;
		private bool m_bIsModified;
		private bool m_bIsNewDocument;
		private StreamReader m_sr = null;

		// Stream reader
		public StreamReader TextStream
		{
			get
			{
				return m_sr;
			}
		}

		// Determines if the document is
		// newly created or pre-existing.
		public bool IsNewDocument
		{
			get { return m_bIsNewDocument; }
			set { m_bIsNewDocument = value; }
		}
		
		// Determines if the document is modified
		// or not
		public bool IsModified
		{
			get { return m_bIsModified; }
			set { m_bIsModified = value; }
		}
		
		// File that contains the data
		public string FileName
		{
			get { return m_strFileName; }
			set { m_strFileName = value; }
		}
		
		public PEditGtkSharpDoc()
		{
			m_bIsModified = false;
			m_bIsNewDocument = true;
			m_strFileName = "";	
		}

		public void SaveDocument(string strFileName, string strContent)
		{
			StreamWriter sw = null;

			// Quick and dirty error checking
			if(strFileName.Length < 1 || strContent.Length < 1)
				return;

			try
			{
				sw = new StreamWriter(strFileName, false);
				sw.Write(strContent);
				sw.Close();
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
		}

		public void OpenDocumentForRead(string strFileName)
		{
			try
			{
				m_sr = File.OpenText(strFileName);
				this.m_strFileName = strFileName;
			}
			catch(Exception exc)
			{
				throw exc;
			}
		}
	}
}

