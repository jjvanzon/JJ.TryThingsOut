package android.dialog;


public class DialogAdapter
	extends android.widget.BaseAdapter
	implements
		mono.android.IGCUserPeer,
		android.widget.AdapterView.OnItemClickListener,
		android.widget.AdapterView.OnItemLongClickListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_isEnabled:(I)Z:GetIsEnabled_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getViewTypeCount:()I:GetGetViewTypeCountHandler\n" +
			"n_getItem:(I)Ljava/lang/Object;:GetGetItem_IHandler\n" +
			"n_areAllItemsEnabled:()Z:GetAreAllItemsEnabledHandler\n" +
			"n_getItemViewType:(I)I:GetGetItemViewType_IHandler\n" +
			"n_getItemId:(I)J:GetGetItemId_IHandler\n" +
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_onItemClick:(Landroid/widget/AdapterView;Landroid/view/View;IJ)V:GetOnItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler:Android.Widget.AdapterView/IOnItemClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onItemLongClick:(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z:GetOnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler:Android.Widget.AdapterView/IOnItemLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Android.Dialog.DialogAdapter, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DialogAdapter.class, __md_methods);
	}


	public DialogAdapter ()
	{
		super ();
		if (getClass () == DialogAdapter.class)
			mono.android.TypeManager.Activate ("Android.Dialog.DialogAdapter, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public DialogAdapter (android.content.Context p0, android.dialog.RootElement p1, android.widget.ListView p2)
	{
		super ();
		if (getClass () == DialogAdapter.class)
			mono.android.TypeManager.Activate ("Android.Dialog.DialogAdapter, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=c4c4237547e4b6cd:Android.Dialog.RootElement, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:Android.Widget.ListView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=c4c4237547e4b6cd", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public boolean isEnabled (int p0)
	{
		return n_isEnabled (p0);
	}

	private native boolean n_isEnabled (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public int getViewTypeCount ()
	{
		return n_getViewTypeCount ();
	}

	private native int n_getViewTypeCount ();


	public java.lang.Object getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native java.lang.Object n_getItem (int p0);


	public boolean areAllItemsEnabled ()
	{
		return n_areAllItemsEnabled ();
	}

	private native boolean n_areAllItemsEnabled ();


	public int getItemViewType (int p0)
	{
		return n_getItemViewType (p0);
	}

	private native int n_getItemViewType (int p0);


	public long getItemId (int p0)
	{
		return n_getItemId (p0);
	}

	private native long n_getItemId (int p0);


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);


	public void onItemClick (android.widget.AdapterView p0, android.view.View p1, int p2, long p3)
	{
		n_onItemClick (p0, p1, p2, p3);
	}

	private native void n_onItemClick (android.widget.AdapterView p0, android.view.View p1, int p2, long p3);


	public boolean onItemLongClick (android.widget.AdapterView p0, android.view.View p1, int p2, long p3)
	{
		return n_onItemLongClick (p0, p1, p2, p3);
	}

	private native boolean n_onItemLongClick (android.widget.AdapterView p0, android.view.View p1, int p2, long p3);

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
