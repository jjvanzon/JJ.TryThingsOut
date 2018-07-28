package android.dialog;


public class DialogActivity
	extends android.app.ListActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onRetainNonConfigurationInstance:()Ljava/lang/Object;:GetOnRetainNonConfigurationInstanceHandler\n" +
			"";
		mono.android.Runtime.register ("Android.Dialog.DialogActivity, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DialogActivity.class, __md_methods);
	}


	public DialogActivity ()
	{
		super ();
		if (getClass () == DialogActivity.class)
			mono.android.TypeManager.Activate ("Android.Dialog.DialogActivity, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.Object onRetainNonConfigurationInstance ()
	{
		return n_onRetainNonConfigurationInstance ();
	}

	private native java.lang.Object n_onRetainNonConfigurationInstance ();

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
