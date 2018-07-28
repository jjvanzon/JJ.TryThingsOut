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

// project created on 3/13/04 at 5:22 a
using System;
using Gtk;
using Mfconsulting.General.Prj2Make.Cui;

namespace Mfconsulting.General.Prj2Make.Gui
{
    class MainClass 
	{
    	public static void Main(string[] args)
    	{
			// Handle command line arguments    	
    		MainOpts optObj = new MainOpts();    		
    		optObj.ProcessArgs(args);
    		
    		if (optObj.GUIless == true) {
    			if (optObj.RemainingArguments.Length > 0) {
    				new MainMod (optObj.isNmake, optObj.isCsc, optObj.RemainingArguments[0]);
    				return;
    			} else {
    				optObj.DoHelp();
    				return;
    			}
    		}
    		
			// Begin GUI 
    		Application.Init ();
			if (args.Length < 1) {
				new MainFRM ();
				Application.Run ();
			} else {  
				// Asuming residual arguments possibly a file path
				if (optObj.RemainingArguments.Length > 0)
				{
					new MainFRM (optObj.isNmake, optObj.isCsc, optObj.RemainingArguments[0]);
					Application.Run ();
				} else { // No arguments
					new MainFRM (optObj.isNmake, optObj.isCsc);
					Application.Run ();
				}
			}
    	}
    }    
}
