using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITimmerController : MonoBehaviour {

    public int defaultSeconds = 45;
    public bool isCounting = false;
    float dtime;
    public Text timeDisp;
    int curSec;
	// Use this for initialization
	void Start () {
		
	}

    public void StopCounting()
    {
        isCounting = false;
    }
	
    public void StartTimeCount()
    {
        dtime = Time.time;
        isCounting = true;
        curSec = defaultSeconds;
        timeDisp.text = "00:45";
    }

	// Update is called once per frame
	void Update () {
        if(isCounting && timeDisp.text == "00:00")
        {
            isCounting = false;
            this.transform.parent.parent.Find("Engine").gameObject.GetComponent<UIManager>().ConnectionTimeOut();
        }

		if(isCounting && Time.time - dtime >= 1f)
        {
            dtime = Time.time;

            curSec--;

            if (curSec == 0)
            {
                timeDisp.text = "00:00";
                isCounting = false;

                this.transform.parent.parent.Find("Engine").gameObject.GetComponent<UIManager>().ConnectionTimeOut();
            }
            else
            {
                int min = curSec / 60;

                string minstr = "0" + min.ToString();

                int sec = curSec % 60;
                string secStr;
                if (sec >= 10)
                {
                    secStr = sec.ToString();
                }
                else
                {
                    secStr = "0" + sec.ToString();
                }

                timeDisp.text = minstr + ":" + secStr;
            }
        }
	}
}
