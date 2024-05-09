using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWifiConnection : MonoBehaviour {

    // Use this for initialization
    public GameObject wifiAnimation;
	void Start () {
		
	}

    public void ShowWiFiAnimation()
    {
        wifiAnimation.SetActive(true);
        wifiAnimation.GetComponent<Animator>().Play("PlayAnim");
        StartCoroutine(StopAnim());
    }

    public IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(0.8f);
        wifiAnimation.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
