using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIStonObjectController : MonoBehaviour {
    /*
    public Vector3 targetPos;
    Vector3 startPos;
    float angle;
    public bool isMoving = false;
    float dTime = 0f, xDelta, yDelta, journeyLength, xPlusDelta , yPlusDelta;
    float startTime, speed = 600.0f;
    bool moveType = false;
    int cnt = 0;
	// Use this for initialization
	void Start () {
		
	}

    public void MoveToPosition(Vector3 tgPos, bool mType)
    {
        moveType = mType;
        isMoving = true;
        startPos = this.transform.localPosition;
        targetPos = tgPos;

        journeyLength = Vector3.Distance(startPos, targetPos);
        startTime = Time.time;

        xDelta = targetPos.x - startPos.x;
        yDelta = targetPos.y - startPos.y;

        if(xDelta == 0)
        {
            angle = 0f;
        }
        else if(yDelta == 0)
        {
            angle = 90f;
        }
        else
        {
            angle = Mathf.Sin(Mathf.Abs(xDelta) / Mathf.Sqrt(xDelta * xDelta + yDelta * yDelta));
        }

        xPlusDelta = Mathf.Sin(angle) * 50;
        yPlusDelta = Mathf.Cos(angle) * 50;

        speed = journeyLength * 2f;

//        Vector3 centerPos = 
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;

            Vector3 center = (startPos + targetPos) * 0.5f;
            center -= new Vector3(0, 0.2f, 0);

            Vector3 rCenter = startPos - center;
            Vector3 sCenter = targetPos - center;

            Vector3 curPos, curpos1;
            float fracJourney = distCovered / journeyLength;
            if(moveType)
                {
//                curPos = Vector3.Lerp(rCenter, sCenter, fracJourney);
                curPos = Vector3.Lerp(startPos, targetPos, fracJourney);
            }
            else
                {
                curPos = Vector3.Lerp(startPos, targetPos, fracJourney);
                }

            float Dist = Vector3.Distance(startPos, curPos);
            float scaleFactor;
            if (Dist >= journeyLength / 2)
            {
                scaleFactor = (journeyLength - Dist) / (journeyLength / 2);
            }
            else
            {
                scaleFactor = Dist / (journeyLength / 2);
            }

 //           float xCurPlusDelta = scaleFactor * xPlusDelta;
//            float yCurPlusDelta = scaleFactor * yPlusDelta;
//            Debug.Log("---x --" + xCurPlusDelta + "--- y ---" + yCurPlusDelta);
//            curPos.x += xCurPlusDelta;
//            curPos.y += yCurPlusDelta;

            this.transform.localPosition = curPos;
/*            if(moveType)
                {
                this.transform.localPosition += center;
                }
*/
    //            this.transform.localPosition = (curpos1 - curPos) * 0.5f;*/
    /*
                cnt++;

                if(Vector3.Distance(transform.localPosition, targetPos) < 10f)
                {
                    Debug.Log("during time ----" + (Time.time - startTime) + "--- dist ---" + Vector3.Distance(startPos, targetPos));
                    isMoving = false;
    //                this.transform.parent.parent.gameObject.GetComponent<UIStoneManager>().stoneMovingState = false;
                }
            }
        }*/
    
    public Vector3 targetPos, defaultSPos;
    Vector3 startPos;
    Vector3 centerPos;
    float angle, totalAngle, radious;
    public bool isMoving = false;
    float dTime = 0f, xDelta, yDelta, journeyLength, xPlusDelta , yPlusDelta;
    float startTime, speed = 600.0f;
    bool moveType = false;
    int cnt = 0;
	// Use this for initialization
	void Start () {
		
	}

    Vector3 tempScale = new Vector3(1.5f, 1.5f, 1.5f);
    float tempTime = 0.005f;
    public float MoveToPosition(Vector3 tgPos, bool mType)
    {
        float dis = Vector3.Distance(transform.position,tgPos);

        float time = 0.42f;//dis * tempTime;//0.6f
        //time = Mathf.Clamp(time,0.4f,0.65f);

        float ontTime = (15f * time) / 100f;
        float fullTime = time - ontTime;

        //Debug.LogError("dis " + dis);
        //Debug.LogError("tempTime " + tempTime);
        //Debug.LogError("dis * tempTime " + dis * tempTime);
        //Debug.LogError("___________");
       // transform.DOMove(tgPos, time);
        transform.DOJump(tgPos, 10f, 1, time); //ashvin 100 is jump power . Reduce it to get low height in result.
        transform.DOScale(tempScale, ontTime);
        transform.DOScale(Vector3.one, fullTime).SetDelay(ontTime);
        return time;
        //change
        /*
                moveType = mType;
                isMoving = true;
                startPos = this.transform.localPosition;
                targetPos = tgPos;

                journeyLength = Vector3.Distance(startPos, targetPos);
                startTime = Time.time;

                xDelta = targetPos.x - startPos.x;
                yDelta = targetPos.y - startPos.y;

                if(xDelta == 0)
                {
                    angle = 0f;
                }
                else if(yDelta == 0)
                {
                    angle = 90f;
                }
                else
                {
                    angle = Mathf.Sin(Mathf.Abs(xDelta) / Mathf.Sqrt(xDelta * xDelta + yDelta * yDelta));
                }

                float nAngle = Mathf.Cos(50f / Mathf.Sqrt((journeyLength / 2) * (journeyLength / 2) + 2500f));
                radious = (50f / Mathf.Sin(nAngle)) / 2;

                totalAngle = Mathf.Sin((radious - 50f) / radious) * 2;

                Vector3 tmpPos = (startPos + targetPos) / 2;

                float nXdelta = Mathf.Sin(angle) * (radious - 50);
                float nYdelta = Mathf.Cos(angle) * (radious - 50);

                centerPos.x = tmpPos.x - nXdelta;
                centerPos.y = tmpPos.y - nXdelta;

                xPlusDelta = Mathf.Sin(angle) * 50;
                yPlusDelta = Mathf.Cos(angle) * 50;

                speed = journeyLength * 2f;
                */
        //        Vector3 centerPos = 
    }

    bool startPlaySound = false;
	
	// Update is called once per frame
	void Update () {
    if (isMoving)
    {
        float distCovered = (Time.time - startTime) * speed;

        Vector3 center = (startPos + targetPos) * 0.5f;
        center -= new Vector3(0, 0.2f, 0);

        Vector3 rCenter = startPos - center;
        Vector3 sCenter = targetPos - center;

        Vector3 curPos, curpos1;
        float fracJourney = distCovered / journeyLength;
        if(moveType)
        {
            curPos = Vector3.Lerp(startPos, targetPos, fracJourney);
        }
        else
        {
           curPos = Vector3.Lerp(startPos, targetPos, fracJourney);
        }

        float Dist = Vector3.Distance(startPos, curPos);
        float scaleFactor;
        if (Dist >= journeyLength / 2)
        {
            scaleFactor = (journeyLength - Dist) / (journeyLength / 2);
        }
        else
        {
            scaleFactor = Dist / (journeyLength / 2);
        }

//           float xCurPlusDelta = scaleFactor * xPlusDelta;
//            float yCurPlusDelta = scaleFactor * yPlusDelta;
//            Debug.Log("---x --" + xCurPlusDelta + "--- y ---" + yCurPlusDelta);
//            curPos.x += xCurPlusDelta;
//            curPos.y += yCurPlusDelta;

        this.transform.localPosition = curPos;
/*            if(moveType)
            {
            this.transform.localPosition += center;
            }
*/
//            this.transform.localPosition = (curpos1 - curPos) * 0.5f;
    
            cnt++;
            if(!startPlaySound && (Time.time - startTime) >= 0.28)
            {
                startPlaySound = true;
                int soundState = PlayerPrefs.GetInt("GAME_SOUND");
                if (soundState > 0)
                {
                    this.gameObject.GetComponent<AudioSource>().Play();
                }
            }

            if(Vector3.Distance(transform.localPosition, targetPos) < 10f)
            {
                Debug.Log("during time ----" + (Time.time - startTime) + "--- dist ---" + Vector3.Distance(startPos, targetPos));
                isMoving = false;
                startPlaySound = false;
//                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
