using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeTimer : MonoBehaviour {

    public Text timer;
    float defTime = 40f, startTime = 0f;
    bool startCount = false;
	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
        {
        timer.text = "00:40";
        }

    public void StartTimer()
        {
        defTime = 40f;
        startCount = true;
        startTime = Time.time;
        timer.text = "00:40";
        }

    public void StopTimer()
        {
        startCount = false;
        }
	
	// Update is called once per frame
	void Update () {
		if(startCount && Time.time - startTime >= 1f)
            {
            defTime--;
            startTime = Time.time;
            if(defTime >= 0)
                {
                string str = defTime.ToString();
                timer.text = "00:" + str;
                }
            else
                {
                startCount = false;
                timer.text = "00:00";
                this.transform.parent.gameObject.GetComponent<UIChallengeManager>().SendRejectToOpponent();
                }
            }
	}
}
