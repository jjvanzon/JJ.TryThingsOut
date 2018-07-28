package bestsellers.droid.views;


public class BookView
	extends monocross.droid.MXDialogActivityView_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BestSellers.Droid.Views.BookView, BestSellers.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BookView.class, __md_methods);
	}


	public BookView ()
	{
		super ();
		if (getClass () == BookView.class)
			mono.android.TypeManager.Activate ("BestSellers.Droid.Views.BookView, BestSellers.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
