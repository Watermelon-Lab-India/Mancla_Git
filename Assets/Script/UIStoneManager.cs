using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStoneManager : MonoBehaviour {

	public GameObject[] defStones = new GameObject[4];
	public GameObject[] bowls = new GameObject[14];

	int oppoBowlId;

    public int moveStoneCount = 0;

    public bool stoneMovingState = false;
	// Use this for initialization
	void Start () {
        
        //		InitGameArea ();
    }

    


    public void InitGameArea()
	{
        //this.transform.parent.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        
        stoneMovingState = false;

        /*for (int i = 0; i < 6; i++)
        {

            for (int j = 0; j < 14; j++)
            {
                GameObject gmobj = this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.tables[i].transform.Find("Stones").gameObject.transform.Find(j.ToString()).gameObject;
                for (int k = gmobj.transform.childCount - 1; k >= 0; k--)
                {
                    DestroyImmediate(gmobj.transform.GetChild(k).gameObject);
                }
            }

            if (i == this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.curSelTable)
            {
                this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.tables[i].SetActive(true);
            }
            else
            {
                this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.tables[i].SetActive(false);
            }
        }*/

        for (int i = 0; i < 14; i++)
        {
            GameObject gmobj = this.transform.Find(i.ToString()).gameObject;
            for (int j = gmobj.transform.childCount - 1; j >= 0; j--)
            {
                DestroyImmediate(gmobj.transform.GetChild(j).gameObject);
            }
        }

        for (int i = 0; i < 48; i++) {

			GameObject newStone = GameObject.Instantiate (defStones [i % 4]) as GameObject;
			newStone.name = i.ToString ();

			int stoneIndex;
			if (i == 0) {
				stoneIndex = 0;
			} else {
				stoneIndex = i / 4;
			}

			if (i >= 24)
				stoneIndex ++;

			newStone.transform.parent = this.gameObject.transform.Find(stoneIndex.ToString());

			newStone.transform.localPosition = Vector3.zero;
			newStone.transform.localScale = new Vector3(1f, 1f, 1f);
			Vector3 stonePos = bowls [stoneIndex].transform.localPosition;

            int xRand = Random.Range (-25, 25);
			int yRand = Random.Range (-25, 25);

			stonePos.x += xRand;
			stonePos.y += yRand;

			newStone.transform.localPosition = stonePos;
            newStone.SetActive(true);
        }

        for (int i = 0; i < 6; i++)
        {
            UnityEngine.Debug.Log("UIStoneManager InitGameArea stones i : "+ i+" "+ this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.tables[i].transform.Find("Stones").gameObject.activeInHierarchy);
        }

    }

    public void BowlSelected(int index)
    {
        if (this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.mtType == Match_Type.MT_PvsNET || this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.mtType == Match_Type.MT_PvsCPU)
        {
            this.transform.parent.parent.Find("timer").gameObject.GetComponent<UITimmerController>().StopCounting();
        }
        
        this.gameObject.SetActive(true);

        Debug.Log(this.gameObject.name+ " Active? " + this.gameObject.activeInHierarchy);
        Debug.Log(this.transform.parent.gameObject.name + " Active? " + this.transform.parent.gameObject.activeInHierarchy);
        
        StartCoroutine(SelectionBowlAct(index));
//        SelectionBowlAct(index);
    }

    public void OnMoveFinished()
        {

        }
    float stoneMoveTime = 0.6f;
    public IEnumerator SelectionBowlAct(int index)
	{
        Debug.Log("UIStoneManager SelectionBowlAct " + index);

        stoneMovingState = true;
		int selIndex = index;

        int otherPlayerBox;
        if (selIndex < 6)
            otherPlayerBox = 13;
        else
            otherPlayerBox = 6;
		GameObject selBowlObject = this.gameObject.transform.Find (selIndex.ToString ()).gameObject;

		int stoneCnt = selBowlObject.transform.childCount;

		int targetBowlIdx = 0;
        int targetBowlContaining = 0;
        GameObject selStone = null;

        for (int i = 0; i < stoneCnt; i++) {

            bool moveType = false;
            if(i == 0)
                {
                moveType = true;
                }

            if(selStone != null && selStone.GetComponent<UIStonObjectController>().isMoving)
            {
                float distNew = Vector3.Distance(selStone.GetComponent<UIStonObjectController>().targetPos, selStone.transform.localPosition);
                float newWaitTime = (float)(distNew / 600f) * 1f;
                yield return new WaitForSeconds(newWaitTime);
            }

			targetBowlIdx = (selIndex + i + 1) % 14;

            if (targetBowlIdx == otherPlayerBox && targetBowlContaining == 0)
            {
                targetBowlContaining = 1;
            }

            targetBowlIdx = (targetBowlIdx + targetBowlContaining) % 14;

			selStone = selBowlObject.transform.GetChild (0).gameObject;
			selStone.transform.parent = this.gameObject.transform.Find (targetBowlIdx.ToString ());

			Vector3 targetPos = bowls [targetBowlIdx].transform.position;//.localPosition;;

            int xDelta = Random.Range(-25, 25);
            int yDelta = Random.Range(-25, 25);

            if (targetBowlIdx == 6 || targetBowlIdx == 13)
                {
                xDelta = Random.Range(-50, 50);
                yDelta = Random.Range(-170, 170);
                }

			targetPos.x += xDelta;
			targetPos.y += yDelta;

            float waitTime = selStone.GetComponent<UIStonObjectController>().MoveToPosition(targetPos, moveType);

//            selStone.GetComponent<UIStoneCurvedMove>().Shoot(targetPos, OnMoveFinished);

            //float waitTime = (Vector3.Distance(targetPos, this.transform.localPosition) / 600f) * 1f;
            Debug.Log("---wait time ---" + waitTime);

            if (i == stoneCnt - 1)
            {
                stoneMoveTime = waitTime;
            }
            else
            {
                stoneMoveTime = waitTime-0.08f;
            }
            
            yield return new WaitForSeconds(stoneMoveTime);
            //            selStone.transform.localPosition = targetPos;

            if (i == stoneCnt - 1) {

			}
		}
        
        if (CheckArrivalBowlIsEmpty((targetBowlIdx), selIndex))
        {
            EmptyTwoBowls((targetBowlIdx), oppoBowlId);

            int soundState = PlayerPrefs.GetInt("GAME_SOUND");
            if (soundState > 0)
            {
                this.transform.parent.parent.gameObject.GetComponent<AudioSource>().Play();
            }
            

            float newWaitTime = (Vector3.Distance(bowls[targetBowlIdx].transform.localPosition, bowls[oppoBowlId].transform.localPosition) / 600f) * 1f;

            yield return new WaitForSeconds(stoneMoveTime);
        }
        stoneMovingState = false;

        if(CheckGameOverState())
        {
            MoveAllStonesToBox();
        }       
        if(this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.mtType == Match_Type.MT_PvsNET || this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.mtType == Match_Type.MT_PvsCPU)
        {
            this.transform.parent.parent.Find("timer").gameObject.GetComponent<UITimmerController>().StartTimeCount();
        }

        yield return new WaitForSeconds(0f);
    }

    public bool CheckGameOverState()
    {
        bool overState = false, otherOver = false;
        for(int i = 0; i < 6; i ++)
        {
            int chCnt = this.transform.Find(i.ToString()).childCount;
            if(chCnt > 0)
            {
                overState = true;
            }
        }

        for (int i = 7; i < 13; i++)
        {
            int chCnt = this.transform.Find(i.ToString()).childCount;
            if (chCnt > 0)
            {
                otherOver = true;
            }
        }

        if(otherOver == false || overState == false)
        {
            return true;
        }
        return false;
    }

    /*
    public IEnumerator SelectionBowlAct(int index)
    {
        int selIndex = index;
        int otherPlayerBox;
        if (selIndex < 6)
            otherPlayerBox = 13;
        else
            otherPlayerBox = 6;
        GameObject selBowlObject = this.gameObject.transform.Find(selIndex.ToString()).gameObject;

        stoneMovingState = true;
        moveStoneCount = selBowlObject.transform.childCount;

        int stoneCnt = selBowlObject.transform.childCount;

        int targetBowlIdx;
        int targetBowlContaining = 0;
        for (int i = 0; i < stoneCnt; i++)
        {
            targetBowlIdx = (selIndex + i + 1) % 14;

            if (targetBowlIdx == otherPlayerBox && targetBowlContaining == 0)
            {
                targetBowlContaining = 1;
            }

            targetBowlIdx = (targetBowlIdx + targetBowlContaining) % 14;

            GameObject selStone = selBowlObject.transform.GetChild(0).gameObject;
            selStone.transform.parent = this.gameObject.transform.Find(targetBowlIdx.ToString());

            Vector3 targetPos = bowls[targetBowlIdx].transform.localPosition;

            int xDelta = Random.Range(-50, 50);
            int yDelta = Random.Range(-50, 50);

            targetPos.x += xDelta;
            targetPos.y += yDelta;

            selStone.GetComponent<UIStonObjectController>().MoveToPosition(targetPos);

            float waitTime = (Vector3.Distance(targetPos, this.transform.localPosition) / 400f) * 1f;

            yield return new WaitForSeconds(waitTime);
            //            selStone.transform.localPosition = targetPos;

            if (i == stoneCnt - 1)
            {
                if (CheckArrivalBowlIsEmpty((targetBowlIdx), selIndex))
                {
                    EmptyTwoBowls((targetBowlIdx), oppoBowlId);
                }
            }
        }
        //        stoneMovingState = false;

        for (int i = 0; i < 14; i++)
        {
            if (this.transform.Find(i.ToString()).childCount != this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.b.board[i])
            {
                Debug.LogError(i + "---" + this.transform.Find(i.ToString()).childCount + "------" + this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.b.board[i]);
            }
        }

        yield return new WaitForSeconds(0f);
    }

    */
    public void EmptyTwoBowls(int firestBowlId, int secondBowlId)
	{
		int targetBowlIdx;
		if (firestBowlId > 6) {
			targetBowlIdx = 13;
		} else {
			targetBowlIdx = 6;
		}

		GameObject firstBowlObj = this.gameObject.transform.Find (firestBowlId.ToString ()).gameObject;
		GameObject oppoBowlObj = this.gameObject.transform.Find (secondBowlId.ToString ()).gameObject;

		int firstStoneCnt = firstBowlObj.transform.childCount;
		int secondStoneCnt = oppoBowlObj.transform.childCount;

		for (int i = 0; i < firstStoneCnt; i++) {
			GameObject selStone = firstBowlObj.transform.GetChild (0).gameObject;
			selStone.transform.parent = this.gameObject.transform.Find (targetBowlIdx.ToString ());

            Vector3 targetPos = bowls[targetBowlIdx].transform.position;//.localPosition;

			int xDelta = Random.Range (-50, 50);
			int yDelta = Random.Range (-170, 170);

			targetPos.x += xDelta;
			targetPos.y += yDelta;

            selStone.GetComponent<UIStonObjectController>().MoveToPosition(targetPos, false);
            //            selStone.transform.localPosition = targetPos;
        }

		for (int i = 0; i < secondStoneCnt; i++) {
			GameObject selStone = oppoBowlObj.transform.GetChild (0).gameObject;
			selStone.transform.parent = this.gameObject.transform.Find (targetBowlIdx.ToString ());

            Vector3 targetPos = bowls[targetBowlIdx].transform.position;//.localPosition;

			int xDelta = Random.Range (-50, 50);
			int yDelta = Random.Range (-170, 170);

			targetPos.x += xDelta;
			targetPos.y += yDelta;

            selStone.GetComponent<UIStonObjectController>().MoveToPosition(targetPos, false);
            //            selStone.transform.localPosition = targetPos;
        }

        for (int i = 0; i < 14; i++)
        {
            if (this.transform.Find(i.ToString()).childCount != this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.b.board[i])
            {
                Debug.LogError(i + "---" + this.transform.Find(i.ToString()).childCount + "------" + this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.b.board[i]);
            }
        }
    }

	public bool CheckArrivalBowlIsEmpty(int targetBowlId, int selId)
	{
		if (((selId >= 0 && selId < 6) && (targetBowlId >= 0 && targetBowlId < 6)) || ((selId >= 7 && selId < 13) && (targetBowlId >= 7 && targetBowlId < 13))) {
			GameObject targetBowlStonesCointainer = this.transform.Find (targetBowlId.ToString ()).gameObject;

			int targetBowlStoneCnt = targetBowlStonesCointainer.transform.childCount;

			if (targetBowlStoneCnt != 1) {
				return false;
			} else {
				int oppoIdx;
				if (targetBowlId < 6) {
					oppoIdx = 6 - targetBowlId + 6;
				} else {
					oppoIdx = 6 - (targetBowlId - 6);
				}
				GameObject oppoObj = this.transform.Find (oppoIdx.ToString ()).gameObject;
				int oppoChildCnt = oppoObj.transform.childCount;

				if (oppoChildCnt == 0) {
					return false;
				} else {
					oppoBowlId = oppoIdx;
					return true;
				}
			}
		} else {
			return false;
		}
		return false;
	}

	public void MoveAllStonesToBox()
	{
		for (int i = 0; i < 13; i++) {
			if (i != 6 && i != 13) {
				GameObject curBowl = this.transform.Find (i.ToString ()).gameObject;

				if (curBowl.transform.childCount > 0) {
					int targetBox;
					if (i < 6) {
					    targetBox = 6;
					}else{
                        targetBox = 13;
                    }
                    Debug.Log("move all ---" + i.ToString());
					for (int j = curBowl.transform.childCount - 1; j >= 0; j--)
                    {
                        GameObject stoneObj = curBowl.transform.GetChild(j).gameObject;

                        stoneObj.transform.parent = this.transform.Find(targetBox.ToString());

                        int xDelta = Random.Range(-50, 50);
                        int yDelta = Random.Range(-170, 170);

                        Vector3 pos = bowls[targetBox].transform.position;//.localPosition;
                        pos.x += xDelta;
                        pos.y += yDelta;

                        stoneObj.GetComponent<UIStonObjectController>().MoveToPosition(pos, false);
                        //                        stoneObj.transform.localPosition = pos;
                    }
				}
			}
		}

        for (int i = 0; i < 14; i++)
        {
            if (this.transform.Find(i.ToString()).childCount != this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.b.board[i])
            {
                Debug.LogError(i + "---" + this.transform.Find(i.ToString()).childCount + "------" + this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.b.board[i]);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
	void Awake()
	{
        Debug.Log("UIStoneManager awake");
        for (int i = 0; i < 4; i++)
        {
            defStones[i] = this.transform.GetChild(0).GetChild(i).gameObject;
        }

        this.transform.parent.gameObject.GetComponent<UITableManager>().enginObj.uStoneManager = this;
    }
}
