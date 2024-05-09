using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITableSelectionObject : MonoBehaviour {

    public int curTableUnlockMoney = 0;
    public int entryMoney = 0;

    public Text entryText, moneyText;

    public Sprite lockedBack;
    public Sprite unlockedBg;

	// Use this for initialization
	void Start () {
		
	}

    public void UnLockTable()
    {
        entryText.text = "ENTRY";
        moneyText.text = entryMoney.ToString();
        this.gameObject.GetComponent<Image>().sprite = lockedBack;
        this.transform.Find("Image (1)").gameObject.SetActive(false);
        this.transform.Find("Image").gameObject.GetComponent<Image>().sprite = unlockedBg;
    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}


}
