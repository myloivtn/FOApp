package crc64391f0f7c6cda8fad;


public class OnMapClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.GoogleMap.OnMapClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMapClick:(Lcom/google/android/gms/maps/model/LatLng;)V:GetOnMapClick_Lcom_google_android_gms_maps_model_LatLng_Handler:Android.Gms.Maps.GoogleMap/IOnMapClickListenerInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("Maui.GoogleMaps.Platforms.Android.Listeners.OnMapClickListener, Maui.GoogleMaps", OnMapClickListener.class, __md_methods);
	}


	public OnMapClickListener ()
	{
		super ();
		if (getClass () == OnMapClickListener.class) {
			mono.android.TypeManager.Activate ("Maui.GoogleMaps.Platforms.Android.Listeners.OnMapClickListener, Maui.GoogleMaps", "", this, new java.lang.Object[] {  });
		}
	}


	public void onMapClick (com.google.android.gms.maps.model.LatLng p0)
	{
		n_onMapClick (p0);
	}

	private native void n_onMapClick (com.google.android.gms.maps.model.LatLng p0);

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
