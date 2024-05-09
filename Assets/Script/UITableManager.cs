using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITableManager : MonoBehaviour {

	public Engine enginObj;

	public GameObject[] bowls;
	public GameObject stoneManager;

    Image myImage, oppoImage;

    Text myCntText, oppCntText;
    int myCnt, oppoCnt;

    public GameObject myBowl, oppoBowl;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if(myCnt != myBowl.transform.childCount)
        {
            myCnt = myBowl.transform.childCount;
            myCntText.text = myCnt.ToString();
        }
        if(oppoCnt != oppoBowl.transform.childCount)
        {
            oppoCnt = oppoBowl.transform.childCount;
            oppCntText.text = oppoCnt.ToString();
        }
	}

	public void InitGameArea()
	{
		
	}

	public void BowlSelected(int bowlNum)
	{
		
	}

	public void BowlClicked(int bowlNum)
	{
		enginObj.BowlSelected (bowlNum);
	}

	void Awake()
    {

    }

    void OnEnable()
	{
        myImage = this.transform.parent.Find("OwnInfo").Find("character").gameObject.GetComponent<Image>();
        oppoImage = this.transform.parent.Find("Opponent").Find("character").gameObject.GetComponent<Image>();
        
        if(enginObj.GetComponent<UIManager>().playerInfo.logInWithFB == 1)
            myImage.sprite = enginObj.GetComponent<UIManager>().characterImage_facebook.sprite;
        else
            myImage.sprite = enginObj.GetComponent<UIManager>().charactersImages[int.Parse(enginObj.GetComponent<UIManager>().playerInfo.characterName)].sprite;

        myCntText = this.transform.parent.Find("OwnInfo").Find("Count").gameObject.GetComponent<Text>();
        oppCntText = this.transform.parent.Find("Opponent").Find("Count").gameObject.GetComponent<Text>();

        if (enginObj.gameObject.GetComponent<UIManager>().myPositionInfo)
        {
            myBowl =  stoneManager.gameObject.transform.Find("13").gameObject;
            oppoBowl = stoneManager.gameObject.transform.Find("6").gameObject;

            myCnt = myBowl.transform.childCount;
            oppoCnt = oppoBowl.transform.childCount;

            myCntText.text = myCnt.ToString();
            oppCntText.text = oppoCnt.ToString();
        }
        else
        {
            oppoBowl = stoneManager.gameObject.transform.Find("13").gameObject;
            myBowl = stoneManager.gameObject.transform.Find("6").gameObject;

            myCnt = myBowl.transform.childCount;
            oppoCnt = oppoBowl.transform.childCount;

            myCntText.text = myCnt.ToString();
            oppCntText.text = oppoCnt.ToString();
        }
	}
}
