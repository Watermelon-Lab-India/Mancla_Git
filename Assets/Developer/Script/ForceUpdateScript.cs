using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parse
{
    public class ForceUpdateScript : MonoBehaviour
    {

        public GameObject GameUpdateData;
        public GameObject updateobj;
        public GameObject Exitobj;
        public GameObject Cancel;

        // Start is called before the first frame update
        void Start()
        {
            QueryData();

            StartCoroutine(QueryData());

            Debug.Log("Application Version : " + Application.version);

        }

        // Update is called once per frame
        void Update()
        {

        }


        public IEnumerator QueryData()
        {
            string version = "";
            int visible = 0;
            int force = 0;

            if (Application.platform == RuntimePlatform.Android)
            {
                var query = ParseObject.GetQuery("APP_INFO")
            .WhereEqualTo("platform", "Android");
                //change platform value for ios "iOS"
                query.FirstAsync().ContinueWith(t =>
                {
                    ParseObject obj = t.Result;
                    //version = "2.0";
                    //visible = 0;
                    //force = 1;
                    version = obj.Get<string>("version");
                    visible = obj.Get<int>("visible");
                    force = obj.Get<int>("Force");
                    Debug.Log("Visible:-" + visible + "Version-" + version);
                    Debug.Log("Force:-" + force);
                });
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                var query = ParseObject.GetQuery("APP_INFO")
            .WhereEqualTo("platform", "iOS");
                //change platform value for ios "iOS"
                query.FirstAsync().ContinueWith(t =>
                {
                    ParseObject obj = t.Result;
                    //version = "2.0";
                    //visible = 0;
                    //force = 1;
                    version = obj.Get<string>("version");
                    visible = obj.Get<int>("visible");
                    force = obj.Get<int>("Force");
                    Debug.Log("Visible:-" + visible + "Version-" + version);
                    Debug.Log("Force:-" + force);
                });
            }

            yield return new WaitForSeconds(2f);

            Debug.Log("Visible:-" + visible + "Version-" + version);
            Debug.Log("Force:-" + force);

            if (Application.version != version)
            {

                Debug.Log("data");
                GameUpdateData.SetActive(true);

                if (visible == 1)
                {
                    if (force == 0)
                    {
                        updateobj.SetActive(true);
                        Cancel.SetActive(true);
                        Exitobj.SetActive(false);
                    }
                    else
                    {
                        updateobj.SetActive(true);
                        Cancel.SetActive(false);
                        Exitobj.SetActive(true);
                    }

                }
                else
                {
                    GameUpdateData.SetActive(false);
                }

            }
            else
            {
                GameUpdateData.SetActive(false);
            }

        }

        public void Exit()
        {
            Application.Quit();


        }

        public void ok()
        {
#if UNITY_ANDROID
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.water.mancalabestboardgame");
#elif UNITY_IOS
            Application.OpenURL("https://apps.apple.com/us/app/mancala-and-friends/id1041732970");
#endif
        }
    }
}
