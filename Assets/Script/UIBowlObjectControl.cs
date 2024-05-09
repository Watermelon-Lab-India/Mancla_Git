using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBowlObjectControl : MonoBehaviour {

    Text cntText;
    int cnt = 0;
    int id;
    GameObject stoneContents;
	// Use this for initialization
	void Start () {
		
	}

	public void Selected()
	{
		string name = this.gameObject.name;
		string numS = name.Replace ("BowlObject", "");

		int num = int.Parse (numS);

		this.transform.parent.parent.gameObject.GetComponent<UITableManager> ().BowlClicked (num - 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(cnt != stoneContents.transform.childCount)
        {
            cnt = stoneContents.transform.childCount;
            cntText.text = cnt.ToString();
        }
	}

    void Awake()
    {
        cntText = this.transform.Find("Text").gameObject.GetComponent<Text>();
        string idtxt = this.gameObject.name.Replace("BowlObject", "");
        id = int.Parse(idtxt) - 1;
        stoneContents = this.transform.parent.parent.gameObject.GetComponent<UITableManager>().stoneManager.gameObject.transform.Find(id.ToString()).gameObject;
        cnt = stoneContents.transform.childCount;
    }
}
