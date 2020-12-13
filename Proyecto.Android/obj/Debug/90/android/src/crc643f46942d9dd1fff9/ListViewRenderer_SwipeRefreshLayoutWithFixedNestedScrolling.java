package crc643f46942d9dd1fff9;


public class ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling
	extends android.support.v4.widget.SwipeRefreshLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterceptTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.Platform.Android.ListViewRenderer+SwipeRefreshLayoutWithFixedNestedScrolling, Xamarin.Forms.Platform.Android", ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling.class, __md_methods);
	}


	public ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Platform.Android.ListViewRenderer+SwipeRefreshLayoutWithFixedNestedScrolling, Xamarin.Forms.Platform.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Platform.Android.ListViewRenderer+SwipeRefreshLayoutWithFixedNestedScrolling, Xamarin.Forms.Platform.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean onInterceptTouchEvent (android.view.MotionEvent p0)
	{
		return n_onInterceptTouchEvent (p0);
	}

	private native boolean n_onInterceptTouchEvent (android.view.MotionEvent p0);

	private java.util.ArrayList refList;
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
