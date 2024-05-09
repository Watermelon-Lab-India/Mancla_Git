using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeManager : MonoBehaviour {

    float startTime = 0f;
    bool isCounting = false;
    GameObject loadingObj;
    GameObject timerObj;
    GameObject rejectButton;
	// Use this for initialization
	void Start () {
        timerObj = this.transform.Find("timer").gameObject;
        loadingObj = this.transform.Find("LoadingAnimation").gameObject;
        rejectButton = this.transform.Find("Reject").gameObject;
	}

    private void OnEnable()
    {
        isCounting = true;
        startTime = Time.time;
        if (loadingObj == null)
            {
            loadingObj = this.transform.Find("LoadingAnimation").gameObject;
            }
        if(timerObj == null)
            {
            timerObj = this.transform.Find("timer").gameObject;
            }
        if(rejectButton == null)
            {
            rejectButton = this.transform.Find("Reject").gameObject;
            }
        timerObj.GetComponent<UIChallengeTimer>().StartTimer();
        loadingObj.SetActive(false);
    }

    private void OnDisable()
        {
        if (loadingObj == null)
            {
            loadingObj = this.transform.Find("LoadingAnimation").gameObject;
            }
        loadingObj.SetActive(false);
        }

    public void SendRejectToOpponent()
        {
        rejectButton.GetComponent<Button>().onClick.Invoke();
        isCounting = false;
        this.gameObject.SetActive(false);
        }

    // Update is called once per frame
    void Update () {
		if(isCounting && Time.time - startTime >= 50f)
            {
//            isCounting = false;
//           this.gameObject.SetActive(false);
            }
	}
}
