package md52915aa36b16555e7579b3eb52a274973;


public class TimerBackgroundService
	extends mono.android.app.IntentService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onHandleIntent:(Landroid/content/Intent;)V:GetOnHandleIntent_Landroid_content_Intent_Handler\n" +
			"n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.CommonCore.TimerBackgroundService, couchdbDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TimerBackgroundService.class, __md_methods);
	}


	public TimerBackgroundService (java.lang.String p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == TimerBackgroundService.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.TimerBackgroundService, couchdbDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public TimerBackgroundService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TimerBackgroundService.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.CommonCore.TimerBackgroundService, couchdbDemo.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onHandleIntent (android.content.Intent p0)
	{
		n_onHandleIntent (p0);
	}

	private native void n_onHandleIntent (android.content.Intent p0);


	public android.os.IBinder onBind (android.content.Intent p0)
	{
		return n_onBind (p0);
	}

	private native android.os.IBinder n_onBind (android.content.Intent p0);

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
