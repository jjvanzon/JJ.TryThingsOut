package android.dialog;


public class WebContentElement
	extends android.dialog.Element
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Android.Dialog.WebContentElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WebContentElement.class, __md_methods);
	}


	public WebContentElement ()
	{
		super ();
		if (getClass () == WebContentElement.class)
			mono.android.TypeManager.Activate ("Android.Dialog.WebContentElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public WebContentElement (java.lang.String p0)
	{
		super ();
		if (getClass () == WebContentElement.class)
			mono.android.TypeManager.Activate ("Android.Dialog.WebContentElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}

	public WebContentElement (java.lang.String p0, int p1)
	{
		super ();
		if (getClass () == WebContentElement.class)
			mono.android.TypeManager.Activate ("Android.Dialog.WebContentElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
