using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IOSLocalNotificatiom : MonoBehaviour
{
    public Text freeCoinTimer;
    public bool stop;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //        NotificationServices.CancelAllLocalNotifications ();      // To cancel all Notifications (If Needed).
#if UNITY_IOS
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);

        //UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert);
        //UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Badge);
        //UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Sound);
#endif
        yield return new WaitForSeconds(10f);

    }
    public void CoinNotification()
    {
        if (!freeCoinTimer.gameObject.activeSelf)
        {
                #if UNITY_IOS
                            ScheduleNotificationForiOSWithMessage("Free coins available!", System.DateTime.Now.AddSeconds(7200f));
                #endif

            
        }
    }
    void ExitNotification()
    {

        #if UNITY_IOS
                ScheduleNotificationForiOSWithMessage("Opponent is waiting...", System.DateTime.Now.AddSeconds(1200f));
        #endif
    }
    void ScheduleNotificationForiOSWithMessage(string text, System.DateTime fireDate)
    {
#if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            print("Scheduling.....");
            UnityEngine.iOS.LocalNotification notification = new UnityEngine.iOS.LocalNotification();
            notification.fireDate = fireDate;
            notification.alertAction = "Alert";
            notification.alertBody = text;
            notification.hasAction = false;
            UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notification);
        }
#endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        if (!stop)
        {
            Debug.Log("Stop");

            ExitNotification();

            stop = true;

        }
    }
}
