using System;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.SimpleAndroidNotifications
{
    public class NotificationExample : MonoBehaviour
    {


		public Text freeCoinTimer;
		public bool stop;

		void Start()
		{
			
		}

        public void Rate()
        {
            Application.OpenURL("http://u3d.as/y6r");
        }

        public void OpenWiki()
        {
            Application.OpenURL("https://github.com/hippogamesunity/SimpleAndroidNotificationsPublic/wiki");
        }


        public void ScheduleSimple()
        {
            NotificationManager.Send(TimeSpan.FromSeconds(60), "Simple notification", "Customize icon and color", new Color(1, 0.3f, 0.15f));
        }

        public void ScheduleNormal()
        {
			
			if(!freeCoinTimer.gameObject.activeSelf)
			{
				NotificationManager.SendWithAppIcon(TimeSpan.FromHours(2), "Free Coins", "Free coin available", new Color(0, 0.6f, 1), NotificationIcon.Message);  //FromHours(2)
			}
        }

		public void ExitGameNotification()
		{
			
			NotificationManager.SendWithAppIcon(TimeSpan.FromMinutes(20), "Opponent is waiting", "Opponent is waiting", new Color(0, 0.6f, 1), NotificationIcon.Message);  //FromHours(2)  //20 minutes


		}

        public void ScheduleCustom()
        {
            var notificationParams = new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(5),
                Title = "Custom notification",
                Message = "Message",
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Heart,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

			NotificationManager.SendCustom(notificationParams);

        }

        public void CancelAll()
        {
            NotificationManager.CancelAll();
        }

		void OnApplicationQuit()
		{
//			ExitGameNotification();
		}

		void OnApplicationPause(bool pause)
		{
			if(!stop)
			{
				Debug.Log("Stop");

				ExitGameNotification();

				stop = true;

			}
		}


		public void MoreGame()
		{
#if UNITY_ANDROID
            Application.OpenURL("https://play.google.com/store/apps/developer?id=Watermelon+Lab+IND");
#elif UNITY_IOS
            Application.OpenURL("https://apps.apple.com/us/developer/vishal-sanap/id1041732969");
#endif
        }

        public void InterstailSet3Time()
		{
			GoogleMobileAdsDemoScript.Instance.InterstailSet3Time();
		
		}

    }



}