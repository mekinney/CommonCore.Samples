package md52915aa36b16555e7579b3eb52a274973;


public class PhoneCallListener
	extends android.telephony.PhoneStateListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCallStateChanged:(ILjava/lang/String;)V:GetOnCallStateChanged_ILjava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.CommonCore.PhoneCallListener, couchdbDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PhoneCallListener.class, __md_methods);
	}


	public PhoneCallListener ()
	{
		super ();
		if (getClass () == PhoneCallListener.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.PhoneCallListener, couchdbDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCallStateChanged (int p0, java.lang.String p1)
	{
		n_onCallStateChanged (p0, p1);
	}

	private native void n_onCallStateChanged (int p0, java.lang.String p1);

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
