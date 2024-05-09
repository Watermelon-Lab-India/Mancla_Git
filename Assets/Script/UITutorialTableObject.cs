using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITutorialTableObject : MonoBehaviour {

    Text cntinfo;
    GameObject stoneObj;
	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        int bowlId = int.Parse(this.gameObject.name.Replace("BowlObject", "")) - 1;
        cntinfo = this.transform.Find("Text").gameObject.GetComponent<Text>();
        stoneObj = this.transform.parent.parent.Find("Stones").Find(bowlId.ToString()).gameObject;
    }

    // Update is called once per frame
    void Update () {
        cntinfo.text = stoneObj.transform.childCount.ToString();
	}
}
