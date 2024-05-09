using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialManager : MonoBehaviour {

    int tutorialId = 0;
    bool isReady = false;
    public GameObject[] defStones, bowls;
    public GameObject stones, freeturn,pointer;

    public GameObject[] tutorialGuides;
    bool stoneMovingState = false;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetString(Constants.KEY_TUTORIAL, "false");
        pointer.SetActive(false);
    }
    
    public void ClearBox()
    {
        for (int i = 0; i < 14; i++)
        {
            GameObject stonesGroup = stones.transform.Find(i.ToString()).gameObject;
            for (int j = stonesGroup.transform.childCount - 1; j >= 0; j--)
            {
                Destroy(stonesGroup.transform.GetChild(j).gameObject);
            }
        }
    }

    public void StartTutorial()
    {  
        tutorialId = 1;
        ClearBox();

        for(int i = 0; i < 5; i ++)
        {
            if(i == 0)
            {
                tutorialGuides[i].SetActive(true);
            }
            else
            {
                tutorialGuides[i].SetActive(false);
            }
        }

        for(int i = 0; i < 4; i ++)
        {
            GameObject stoneObj = GameObject.Instantiate(defStones[i]) as GameObject;

            stoneObj.SetActive(true);
            stoneObj.name = i.ToString();

            stoneObj.transform.parent = stones.transform.Find("3");

            Vector3 pos = bowls[3].transform.localPosition;

            int xRand = Random.Range(-50,50);
            int yRand = Random.Range(-50, 50);

            pos.x += xRand;
            pos.y += yRand;

            stoneObj.transform.localPosition = pos;
            stoneObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }


    }

    public void BowlSelected(GameObject gmObj)
    {
        int bowlId = int.Parse(gmObj.name.Replace("BowlObject", "")) - 1;
        if (tutorialId == 1 && bowlId == 3)
        {
            FirstTutorialObjectClicked();
        }
        else if (tutorialId == 2 && isReady && bowlId == 2)
        {
            StartCoroutine(MoveStones(2));
        }
        else if (tutorialId == 3 && isReady && (bowlId == 3 || bowlId == 4 || bowlId == 5))
        {
            StartCoroutine(MoveStones(bowlId));
        }
        else if(tutorialId == 4 && isReady && bowlId == 2)
        {
            StartCoroutine(MoveBothStones());
        }
        else if(tutorialId == 5 && isReady && bowlId == 5)
        {
            StartCoroutine(MoveStones(bowlId));
        }
    }

    public IEnumerator MoveBothStones()
    {
        stoneMovingState = true;

        GameObject mStone1 = stones.transform.Find("2").gameObject.transform.GetChild(0).gameObject;
        GameObject targetBowl1 = stones.transform.Find("3").gameObject;
        mStone1.transform.parent = targetBowl1.transform;

        Vector3 pos1 = bowls[3].transform.localPosition;

        int xRand1 = Random.Range(-50, 50);
        int yRand1 = Random.Range(-50, 50);

        pos1.x += xRand1;
        pos1.y += yRand1;

        mStone1.GetComponent<UITutorialStoneObject>().MoveToPosition(pos1);

        yield return new WaitForSeconds(0.55f);

        int selBowl = 9;

        GameObject mBowl = stones.transform.Find("9").gameObject;
        int chCnt = mBowl.transform.childCount;

        int cnt = 1;

        for (int i = chCnt - 1; i >= 0; i--)
        {
            GameObject mStone = mBowl.transform.GetChild(i).gameObject;

            //            GameObject targetBowl = stones.transform.Find((selBowl + cnt).ToString()).gameObject;
            GameObject targetBowl = stones.transform.Find("6").gameObject;

            mStone.transform.parent = targetBowl.transform;

            Vector3 pos = bowls[6].transform.localPosition;

            int xRand = Random.Range(-50, 50);
            int yRand = Random.Range(-50, 50);

            pos.x += xRand;
            pos.y += yRand;

            mStone.GetComponent<UITutorialStoneObject>().MoveToPosition(pos);

            cnt++;
        }

        mStone1 = stones.transform.Find("3").gameObject.transform.GetChild(0).gameObject;
        targetBowl1 = stones.transform.Find("6").gameObject;
        mStone1.transform.parent = targetBowl1.transform;

        pos1 = bowls[6].transform.localPosition;

        xRand1 = Random.Range(-50, 50);
        yRand1 = Random.Range(-50, 50);

        pos1.x += xRand1;
        pos1.y += yRand1;

        mStone1.GetComponent<UITutorialStoneObject>().MoveToPosition(pos1);

        stoneMovingState = false;

        yield return new WaitForSeconds(1.5f);

        isReady = false;
        tutorialId++;
        Debug.Log(tutorialId);
    }

    public void FirstTutorialObjectClicked()
    {
        StartCoroutine(MoveStones(3));
//        MoveStones(3);
    }

    public IEnumerator MoveStones(int bowlId)
    {
        stoneMovingState = true;

        int selBowl = bowlId;

        GameObject mBowl = stones.transform.Find(bowlId.ToString()).gameObject;
        int chCnt = mBowl.transform.childCount;

        int cnt = 1;

        for(int i = chCnt - 1; i >= 0; i --)
        {
            GameObject mStone = mBowl.transform.GetChild(i).gameObject;

            GameObject targetBowl = stones.transform.Find((selBowl + cnt).ToString()).gameObject;

            mStone.transform.parent = targetBowl.transform;

            Vector3 pos = bowls[selBowl + cnt].transform.localPosition;

            int xRand = Random.Range(-50, 50);
            int yRand = Random.Range(-50, 50);

            pos.x += xRand;
            pos.y += yRand;

            mStone.GetComponent<UITutorialStoneObject>().MoveToPosition(pos);

            cnt++;

            yield return new WaitForSeconds(0.55f);
        }

        stoneMovingState = false;

        yield return new WaitForSeconds(1.5f);

        isReady = false;
        tutorialId++;
        Debug.Log(tutorialId);
    }

    public IEnumerator ShowFreeTurn()
    {
        yield return new WaitForSeconds(3f);
        freeturn.SetActive(false);
    }

    public void StartSecondTutorial()
    {
        ClearBox();

        for (int i = 0; i < 5; i++)
        {
            if (i == 1)
            {
                tutorialGuides[i].SetActive(true);
            }
            else
            {
                tutorialGuides[i].SetActive(false);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject stoneObj = GameObject.Instantiate(defStones[i]) as GameObject;

            stoneObj.SetActive(true);
            stoneObj.name = i.ToString();

            stoneObj.transform.parent = stones.transform.Find("2");

            Vector3 pos = bowls[2].transform.localPosition;

            int xRand = Random.Range(-50, 50);
            int yRand = Random.Range(-50, 50);

            pos.x += xRand;
            pos.y += yRand;

            stoneObj.transform.localPosition = pos;
            stoneObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        isReady = true;
    }

    public void StartThirdTutorial()
    {
        freeturn.SetActive(true);
        StartCoroutine(ShowFreeTurn());
        isReady = true;
    }

    public void StartFourthTutorial()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 2)
            {
                tutorialGuides[i].SetActive(true);
            }
            else
            {
                tutorialGuides[i].SetActive(false);
            }
        }
        ClearBox();
        for (int i = 0; i < 4; i++)
        {
            GameObject stoneObj = GameObject.Instantiate(defStones[i]) as GameObject;
            
            stoneObj.SetActive(true);
            stoneObj.name = i.ToString();

            stoneObj.transform.parent = stones.transform.Find("9");

            Vector3 pos = bowls[9].transform.localPosition;

            int xRand = Random.Range(-50, 50);
            int yRand = Random.Range(-50, 50);

            pos.x += xRand;
            pos.y += yRand;

            stoneObj.transform.localPosition = pos;
            stoneObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        GameObject stoneObj1 = GameObject.Instantiate(defStones[0]) as GameObject;
        stoneObj1.SetActive(true);
        stoneObj1.name = "0";

        stoneObj1.transform.parent = stones.transform.Find("2");

        Vector3 pos1 = bowls[2].transform.localPosition;

        int xRand1 = Random.Range(-50, 50);
        int yRand1 = Random.Range(-50, 50);

        pos1.x += xRand1;
        pos1.y += yRand1;

        stoneObj1.transform.localPosition = pos1;
        stoneObj1.transform.localScale = new Vector3(1f, 1f, 1f);
        isReady = true;
    }

    public void StartFivethTutorial()
    {
        ClearBox();

        for (int i = 0; i < 5; i++)
        {
            if (i == 3)
            {
                tutorialGuides[i].SetActive(true);
            }
            else
            {
                tutorialGuides[i].SetActive(false);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject stoneObj = GameObject.Instantiate(defStones[i]) as GameObject;
            stoneObj.SetActive(true);
            stoneObj.name = i.ToString();

            stoneObj.transform.parent = stones.transform.Find("11");

            Vector3 pos = bowls[11].transform.localPosition;

            int xRand = Random.Range(-50, 50);
            int yRand = Random.Range(-50, 50);

            pos.x += xRand;
            pos.y += yRand;

            stoneObj.transform.localPosition = pos;
            stoneObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        GameObject stoneObj1 = GameObject.Instantiate(defStones[0]) as GameObject;
        stoneObj1.SetActive(true);
        stoneObj1.name = "0";

        stoneObj1.transform.parent = stones.transform.Find("5");

        Vector3 pos1 = bowls[5].transform.localPosition;

        int xRand1 = Random.Range(-50, 50);
        int yRand1 = Random.Range(-50, 50);

        pos1.x += xRand1;
        pos1.y += yRand1;

        stoneObj1.transform.localPosition = pos1;
        stoneObj1.transform.localScale = new Vector3(1f, 1f, 1f);
        isReady = true;
    }

    public void StartSixthTutorial()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                tutorialGuides[i].SetActive(true);
            }
            else
            {
                tutorialGuides[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if(tutorialId == 1)
        {
            tutorialGuides[0].SetActive(true);
        }
        else if(tutorialId == 2 && isReady == false)
        {
            StartSecondTutorial();
        }
        else if(tutorialId == 3 && isReady == false)
        {
            StartThirdTutorial();
        }
        else if(tutorialId == 4 && isReady == false)
        {
            StartFourthTutorial();
        }
        else if(tutorialId == 5 && isReady == false)
        {
            StartFivethTutorial();
        }
        else if(tutorialId == 6 && isReady == false)
        {
            StartSixthTutorial();
        }
        else if(tutorialId == 7 && isReady == false)
        {

        }
	}
}
