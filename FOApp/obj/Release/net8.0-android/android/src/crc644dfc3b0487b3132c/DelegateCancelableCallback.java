package crc644dfc3b0487b3132c;


public class DelegateCancelableCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.GoogleMap.CancelableCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCancel:()V:GetOnCancelHandler:Android.Gms.Maps.GoogleMap/ICancelableCallbackInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"n_onFinish:()V:GetOnFinishHandler:Android.Gms.Maps.GoogleMap/ICancelableCallbackInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("Maui.GoogleMaps.Android.Callbacks.DelegateCancelableCallback, Maui.GoogleMaps", DelegateCancelableCallback.class, __md_methods);
	}


	public DelegateCancelableCallback ()
	{
		super ();
		if (getClass () == DelegateCancelableCallback.class) {
			mono.android.TypeManager.Activate ("Maui.GoogleMaps.Android.Callbacks.DelegateCancelableCallback, Maui.GoogleMaps", "", this, new java.lang.Object[] {  });
		}
	}


	public void onCancel ()
	{
		n_onCancel ();
	}

	private native void n_onCancel ();


	public void onFinish ()
	{
		n_onFinish ();
	}

	private native void n_onFinish ();

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
