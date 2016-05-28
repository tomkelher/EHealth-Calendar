package md53af59c1425f5b9d80123cecbe195877e;


public class MyActivityGroup
	extends android.app.ActivityGroup
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Calendar.MyActivityGroup, Calendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyActivityGroup.class, __md_methods);
	}


	public MyActivityGroup () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyActivityGroup.class)
			mono.android.TypeManager.Activate ("Calendar.MyActivityGroup, Calendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public MyActivityGroup (boolean p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MyActivityGroup.class)
			mono.android.TypeManager.Activate ("Calendar.MyActivityGroup, Calendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Boolean, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
