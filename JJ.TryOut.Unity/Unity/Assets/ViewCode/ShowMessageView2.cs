using UnityEngine;
using System.Collections;

public class ShowMessageView2 : MonoBehaviour {

	private int _counter = 0;
	private string _text;
	
	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 20, 200, 20), "Show Message")) 
		{
			_counter++;
			
			ClassLibrary1.Class1 obj = new ClassLibrary1.Class1 ();
			_text = obj.Method(_counter);
		}
		
		GUI.Label (new Rect (10, 50, 200, 20), _text);
	}
}
