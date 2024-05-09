using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEmotionManage : MonoBehaviour {

	bool moveDirection = false;
	bool moveState = false;
	Vector3 curpos;
	int movedDelta = 0;
	float dTime = 0f;

    public Sprite[] emotionImages;

    public GameObject myEmotion, oppoEmotion, engine;
	// Use this for initialization
	void Start () {

	}

	public void OnArrowClick(GameObject chObj)
	{
		if (moveState)
			return;
		curpos = this.gameObject.transform.localPosition;
		if (chObj.transform.GetChild (0).gameObject.activeSelf) {
			chObj.transform.GetChild (0).gameObject.SetActive (false);
			chObj.transform.GetChild (1).gameObject.SetActive (true);
			moveDirection = false;
			moveState = true;
		} else {
			chObj.transform.GetChild (0).gameObject.SetActive (true);
			chObj.transform.GetChild (1).gameObject.SetActive (false);
			moveDirection = true;
			moveState = true;
		}
	}

    public void EmotionClicked(int id)
    {
        GameObject emotionObj = GameObject.Instantiate(myEmotion) as GameObject;

        emotionObj.transform.parent = this.gameObject.transform;
        emotionObj.transform.localPosition = myEmotion.transform.localPosition;

        emotionObj.SetActive(true);
        emotionObj.GetComponent<Image>().sprite = emotionImages[id];
        emotionObj.GetComponent<Image>().SetNativeSize();

        emotionObj.GetComponent<UIEmotionObject>().StartMove();

        engine.GetComponent<Engine>().SendEmotionToOtherPlayer(id);
    }

    public void ReceiveEmotion(int id)
    {
        GameObject emotionObj = GameObject.Instantiate(oppoEmotion) as GameObject;

        emotionObj.transform.parent = this.gameObject.transform;

        emotionObj.transform.localPosition = oppoEmotion.transform.localPosition;

        emotionObj.SetActive(true);
        emotionObj.GetComponent<Image>().sprite = emotionImages[id];
        emotionObj.GetComponent<Image>().SetNativeSize();

        emotionObj.GetComponent<UIEmotionObject>().StartMove();
    }

	// Update is called once per frame
	void Update () {
        if(this.transform.parent.gameObject.name == "Ipad")
        {
            if (moveState && movedDelta < 280 && Time.time - dTime >= 0.02f)
            {
                dTime = Time.time;
                if (moveDirection)
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 20;
                    if (movedDelta >= 280)
                    {
                        moveState = false;
                        movedDelta = 0;
                    }
                    else
                    {
                        tmpPos.x += movedDelta;
                        this.gameObject.transform.localPosition = tmpPos;
                    }
                }
                else
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 20;
                    if (movedDelta >= 280)
                    {
                        moveState = false;
                        movedDelta = 0;
                    }
                    else
                    {
                        tmpPos.x -= movedDelta;
                        this.gameObject.transform.localPosition = tmpPos;
                    }
                }
            }
        }
        else
        {
            if (moveState && movedDelta < 190 && Time.time - dTime >= 0.02f)
            {
                dTime = Time.time;
                if (moveDirection)
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 10;
                    if (movedDelta >= 190)
                    {
                        moveState = false;
                        movedDelta = 0;
                    }
                    else
                    {
                        tmpPos.x += movedDelta;
                        this.gameObject.transform.localPosition = tmpPos;
                    }
                }
                else
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 10;
                    if (movedDelta >= 190)
                    {
                        moveState = false;
                        movedDelta = 0;
                    }
                    else
                    {
                        tmpPos.x -= movedDelta;
                        this.gameObject.transform.localPosition = tmpPos;
                    }
                }
            }
        }
	}
}
