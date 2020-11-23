package crc649f00aa379e6a39c3;


public class CocosSharpViewRenderer
	extends crc643f46942d9dd1fff9.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CocosSharp.CocosSharpViewRenderer, CocosSharp.Forms", CocosSharpViewRenderer.class, __md_methods);
	}


	public CocosSharpViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CocosSharpViewRenderer.class)
			mono.android.TypeManager.Activate ("CocosSharp.CocosSharpViewRenderer, CocosSharp.Forms", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CocosSharpViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CocosSharpViewRenderer.class)
			mono.android.TypeManager.Activate ("CocosSharp.CocosSharpViewRenderer, CocosSharp.Forms", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CocosSharpViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CocosSharpViewRenderer.class)
			mono.android.TypeManager.Activate ("CocosSharp.CocosSharpViewRenderer, CocosSharp.Forms", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
