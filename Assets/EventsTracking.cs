using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class EventsTracking : MonoBehaviour {

    static AndroidJavaObject trackingManagerAccesser;//= new AndroidJavaObject ("com.digitalmedia.hunt.TrackingManagerAccesser");

    static EventsTracking mInstance;

    void Awake()
    {
        if(mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(this.gameObject);      
            
            trackingManagerAccesser = new AndroidJavaObject("com.digitalmedia.hunt.TrackingManagerAccesser");

            init();

            invalidate();
        }
    }

	public static void trackEvent(string eventName,string eventValue)
	{
		Debug.Log("Unity trackEvent");
		//trackingManagerAccesser.CallStatic<AndroidJavaObject>("trackEvent",eventName,eventValue);
        trackingManagerAccesser.CallStatic("trackEvent", eventName, eventValue);

    }

    public static void init()
	{
		Debug.Log("Unity init");
		trackingManagerAccesser.CallStatic("init");
	}

	public static void disableAndroidIdFallbackOnNoAdvertisementId(bool shouldDisable)
	{
		Debug.Log("Unity disableAndroidIdFallbackOnNoAdvertisementId");
		trackingManagerAccesser.CallStatic("disableAndroidIdFallbackOnNoAdvertisementId",shouldDisable);
	}
	public static void invalidate()
	{
		Debug.Log("Unity invalidate");
		trackingManagerAccesser.CallStatic("invalidate");
	}
    
}
