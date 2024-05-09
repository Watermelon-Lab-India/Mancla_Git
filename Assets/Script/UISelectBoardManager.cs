using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISelectBoardManager : MonoBehaviour {

	public ScrollRect scroll;
    public GameObject engine;

    public GameObject[] tableObjects;

    public int[] tableMoney = new int[6];
	// Use this for initialization
	void Start () {
        tableMoney[0] = 100;
        tableMoney[1] = 300;
        tableMoney[2] = 500;
        tableMoney[3] = 700;
        tableMoney[4] = 900;
        tableMoney[5] = 1100;
	}

	public void MoveRight()
	{
		UnityEngine.Debug.Log (scroll.normalizedPosition.x);
		if (scroll.normalizedPosition.x < 0)
			scroll.normalizedPosition = Vector2.zero;
		if (scroll.normalizedPosition.x <= 0.87f) {
			Vector2 pos = scroll.normalizedPosition;
			pos.x += 0.33f;
			if (pos.x > 0.3f && pos.x < 0.6f) {
				pos.x = 0.33f;
			}
			if (pos.x > 0.6f && pos.x < 0.9f) {
				pos.x = 0.66f;
			}
			if (pos.x > 0.9f) {
				pos.x = 1f;
			}
			scroll.normalizedPosition = pos;
		}
		UnityEngine.Debug.Log (scroll.normalizedPosition.x);
	}

	public void MoveLeft()
	{
		UnityEngine.Debug.Log ("---!!!---" + scroll.normalizedPosition.x);
		if (scroll.normalizedPosition.x > 1)
			scroll.normalizedPosition = new Vector2(1f, 0f);
		if (scroll.normalizedPosition.x >= 0.13f) {
			Vector2 pos = scroll.normalizedPosition;
			pos.x -= 0.33f;
			if (pos.x > 0.3f && pos.x < 0.6f) {
				pos.x = 0.33f;
			}
			if (pos.x > 0.6f && pos.x < 0.9f) {
				pos.x = 0.66f;
			}
			if (pos.x > 0f && pos.x < 0.3) {
				pos.x = 0f;
			}
			scroll.normalizedPosition = pos;
		}
		UnityEngine.Debug.Log ("---!!!---" + scroll.normalizedPosition.x);
	}
	// Update is called once per frame
	void Update ()
    {
      //  GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
    }

    public void OnEnable()
    {
        UIManager._instance.playTypeBtnGrp.SetActive(false);
        Vector2 pos = scroll.normalizedPosition;
        pos.x = 0;
        scroll.normalizedPosition = pos;

//        int unlockedTableId = engine.GetComponent<UIManager>().playerInfo.tableOpenState;
        //set talbe state to select game
        for(int i = 1; i < 6; i ++)
        {
            if(engine.GetComponent<UIManager>().playerInfo.tableOpenState[i] == 1)
            {
                tableObjects[i].GetComponent<Image>().sprite = tableObjects[i].GetComponent<UITableSelectionObject>().lockedBack;
                tableObjects[i].GetComponent<UITableSelectionObject>().entryText.text = "ENTRY";
                tableObjects[i].GetComponent<UITableSelectionObject>().moneyText.text = tableObjects[i].GetComponent<UITableSelectionObject>().entryMoney.ToString();
                tableObjects[i].transform.Find("Image (1)").gameObject.SetActive(false);
                tableObjects[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite = tableObjects[i].GetComponent<UITableSelectionObject>().unlockedBg;
            }
        }

       // GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();

    }

    public void OpenprivateMode(int tbId)
    {
        Debug.Log(" OpenprivateMode game mode : " + UIManager._instance.game_Mode);
        if (UIManager._instance.game_Mode == 1)
        {
            if (CheckTableToPlayGame(tbId))
            {
                UIManager._instance.PrivatePlayScreen.SetActive(true);
                UIManager._instance.selectBoardGroup.SetActive(false);
                PlayerPrefs.SetInt("tbid", tbId);
            }
        }
        else
        {
            PlayerPrefs.SetInt("tbid", tbId);
            SelectTableToPlayGame(tbId);
        }
    }

    public bool CheckTableToPlayGame(int tbId)
    {
        //        int unlockedTableId = engine.GetComponent<UIManager>().playerInfo.tableOpenState;
        bool tempreturn=false;
        Debug.Log("s" + tbId);

        if (engine.GetComponent<UIManager>().playerInfo.tableOpenState[tbId] > 0)
        {
            Debug.Log("ddd === " + engine.GetComponent<UIManager>().playerInfo.coins + "========" + tableMoney[tbId]);
            if (engine.GetComponent<UIManager>().playerInfo.coins < tableMoney[tbId])
            {
                shopButton.onClick.Invoke();
                dataobjectfalse();
                Debug.Log("ddddd");
                tempreturn = false;
            }
            else
            {
                tempreturn = true;
            }
            //this.transform.parent.Find("LoadingAnimation").gameObject.SetActive(true);
            //engine.GetComponent<UIManager>().TableSelected(tbId);
        }
        else
        {
            tempreturn = false;
            int unLockMoney = 0;

            switch (tbId)
            {
                case 1:
                    unLockMoney = 2000;
                    break;
                case 2:
                    unLockMoney = 5000;
                    break;
                case 3:
                    unLockMoney = 8000;
                    break;
                case 4:
                    unLockMoney = 99999;
                    break;
                case 5:
                    unLockMoney = 999999;
                    break;
            }

            if (unLockMoney < 10000)
            {
                if (engine.GetComponent<UIManager>().playerInfo.coins >= unLockMoney)
                {
                    engine.GetComponent<UIManager>().BuyUnlockedTableWithCoins(unLockMoney, tbId);

                    tableObjects[tbId].GetComponent<UITableSelectionObject>().UnLockTable();
                    Debug.Log("Unlocked");
                }
                else
                {
                    shopButton.onClick.Invoke();
                }
            }
            else if (unLockMoney == 99999)
            {
                engine.GetComponent<PurchaserLogic>().BuyProductID("table_5");
            }
            else if (unLockMoney == 999999)
            {
                engine.GetComponent<PurchaserLogic>().BuyProductID("table_6");
            }
            else
            {
                //to unlock the table you must pay money
                //				Debug.Log("unlocked the dta");

            }
        }
        return tempreturn;
    }


    public void CreateFBRoom()
    {
        Debug.Log("FB room creation");
        UIManager._instance.CreateFBRoom();
    }

    public void CreateMyRoom()
    {
        Debug.Log(" create my room game mode : " + UIManager._instance.game_Mode);
        int tbId =PlayerPrefs.GetInt("tbid", 0);
        PlayerPrefs.SetInt("privateRoom",1);
        //SelectTableToPlayGame(tbId);
        this.transform.parent.Find("LoadingAnimation").gameObject.SetActive(true);
        engine.GetComponent<UIManager>().TableSelected(tbId);
    }

    public void JoinRoomWithCode()
    {
        int tbId = PlayerPrefs.GetInt("tbid", 0);
        SelectTableToPlayGameCode(tbId);
    }

    public void SelectTableToPlayGameCode(int tbId)
    {
        //        int unlockedTableId = engine.GetComponent<UIManager>().playerInfo.tableOpenState;

        Debug.Log(engine.GetComponent<UIManager>().playerInfo.tableOpenState[tbId] + "s" + tbId);

        if (engine.GetComponent<UIManager>().playerInfo.tableOpenState[tbId] > 0)
        {
            Debug.Log("ddd");
            if (engine.GetComponent<UIManager>().playerInfo.coins < tableMoney[tbId])
            {
                shopButton.onClick.Invoke();
                dataobjectfalse();
                Debug.Log("ddd");
                return;
            }
            this.transform.parent.Find("LoadingAnimation").gameObject.SetActive(true);
            engine.GetComponent<UIManager>().TableSelectedByCode(tbId);
        }
        else
        {
            int unLockMoney = 0;

            switch (tbId)
            {
                case 1:
                    unLockMoney = 2000;
                    break;
                case 2:
                    unLockMoney = 5000;
                    break;
                case 3:
                    unLockMoney = 8000;
                    break;
                case 4:
                    unLockMoney = 99999;
                    break;
                case 5:
                    unLockMoney = 999999;
                    break;
            }

            if (unLockMoney < 10000)
            {
                if (engine.GetComponent<UIManager>().playerInfo.coins >= unLockMoney)
                {
                    engine.GetComponent<UIManager>().BuyUnlockedTableWithCoins(unLockMoney, tbId);

                    tableObjects[tbId].GetComponent<UITableSelectionObject>().UnLockTable();
                    Debug.Log("Unlocked");
                }
                else
                {
                    shopButton.onClick.Invoke();
                }
            }
            else if (unLockMoney == 99999)
            {
                engine.GetComponent<PurchaserLogic>().BuyProductID("table_5");
            }
            else if (unLockMoney == 999999)
            {
                engine.GetComponent<PurchaserLogic>().BuyProductID("table_6");
            }
            else
            {
                //to unlock the table you must pay money
                //				Debug.Log("unlocked the dta");

            }
        }
    }


    public Button shopButton;

    public void SelectTableToPlayGame(int tbId)
    {
//        int unlockedTableId = engine.GetComponent<UIManager>().playerInfo.tableOpenState;

		Debug.Log("s"+tbId);

        if(engine.GetComponent<UIManager>().playerInfo.tableOpenState[tbId] > 0)
        {
			Debug.Log("ddd === "+ engine.GetComponent<UIManager>().playerInfo.coins+"========"+ tableMoney[tbId]);
            if(engine.GetComponent<UIManager>().playerInfo.coins < tableMoney[tbId])
            {
                shopButton.onClick.Invoke();
				dataobjectfalse();
				Debug.Log("ddddd");
                return;
            }
           
            this.transform.parent.Find("LoadingAnimation").gameObject.SetActive(true);
            engine.GetComponent<UIManager>().TableSelected(tbId);
        }
        else
        {
            int unLockMoney = 0;

            switch(tbId)
            {
                case 1:
                    unLockMoney = 2000;
                    break;
                case 2:
                    unLockMoney = 5000;
                    break;
                case 3:
                    unLockMoney = 8000;
                    break;
                case 4:
                    unLockMoney = 99999;
                    break;
                case 5:
                    unLockMoney = 999999;
                    break;
            }

            if(unLockMoney < 10000)
            {
                if(engine.GetComponent<UIManager>().playerInfo.coins >= unLockMoney)
                {
                    engine.GetComponent<UIManager>().BuyUnlockedTableWithCoins(unLockMoney, tbId);

                    tableObjects[tbId].GetComponent<UITableSelectionObject>().UnLockTable();
					Debug.Log("Unlocked");
                }
				else
				{
					shopButton.onClick.Invoke();
				}
            }
            else if(unLockMoney == 99999)
            {
                engine.GetComponent<PurchaserLogic>().BuyProductID("table_5");
            }
            else if(unLockMoney == 999999)
            {
                engine.GetComponent<PurchaserLogic>().BuyProductID("table_6");
            }
            else
            {
                //to unlock the table you must pay money
//				Debug.Log("unlocked the dta");

            }
        }
    }


	public void SelectTableToPlayGameFB(int tbId)
	{
		//        int unlockedTableId = engine.GetComponent<UIManager>().playerInfo.tableOpenState;

		Debug.Log("s"+tbId);


			int unLockMoney = 0;

			switch(tbId)
			{
			case 1:
				unLockMoney = 2000;
				break;
			case 2:
				unLockMoney = 5000;
				break;
			case 3:
				unLockMoney = 8000;
				break;
			case 4:
				unLockMoney = 99999;
				break;
			case 5:
				unLockMoney = 999999;
				break;
			}

			if(unLockMoney < 10000)
			{
				if(engine.GetComponent<UIManager>().playerInfo.coins >= unLockMoney)
				{
					engine.GetComponent<UIManager>().BuyUnlockedTableWithCoins(unLockMoney, tbId);

					tableObjects[tbId].GetComponent<UITableSelectionObject>().UnLockTable();
					Debug.Log("Unlocked");
				}
				else
				{
					shopButton.onClick.Invoke();
				}
			}
			else if(unLockMoney == 99999)
			{
				engine.GetComponent<PurchaserLogic>().BuyProductID("table_5");
			}
			else if(unLockMoney == 999999)
			{
				engine.GetComponent<PurchaserLogic>().BuyProductID("table_6");
			}
			else
			{
				//to unlock the table you must pay money
				//				Debug.Log("unlocked the dta");

			}

	}



    public void UnLockedSelectionTable(int tableId)
    {
        tableObjects[tableId].GetComponent<UITableSelectionObject>().UnLockTable();
    }
 
    
	void Awake()
	{
	}

	void dataobjectfalse()
	{
		engine.GetComponent<UIManager>().backButton3.SetActive(true);

	}
}
