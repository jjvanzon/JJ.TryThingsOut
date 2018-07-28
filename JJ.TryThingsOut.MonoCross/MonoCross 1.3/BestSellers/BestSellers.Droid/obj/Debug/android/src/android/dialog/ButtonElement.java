package android.dialog;


public class ButtonElement
	extends android.dialog.StringElement
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Android.Dialog.ButtonElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ButtonElement.class, __md_methods);
	}


	public ButtonElement ()
	{
		super ();
		if (getClass () == ButtonElement.class)
			mono.android.TypeManager.Activate ("Android.Dialog.ButtonElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ButtonElement (java.lang.String p0)
	{
		super ();
		if (getClass () == ButtonElement.class)
			mono.android.TypeManager.Activate ("Android.Dialog.ButtonElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}

	public ButtonElement (java.lang.String p0, int p1)
	{
		super ();
		if (getClass () == ButtonElement.class)
			mono.android.TypeManager.Activate ("Android.Dialog.ButtonElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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
