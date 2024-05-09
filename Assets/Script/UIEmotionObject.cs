using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEmotionObject : MonoBehaviour {

    bool isMoving = false;
    float moveStart = 0f, dTime = 0f, endTime = 0f;
    float movedDelta = 0f;
    // Use this for initialization
    public Vector3 targetPos;
    float xDelta, yDelta, journeyLength;
    float startTime, speed = 600.0f;
    Vector3 startPos;
    void Start () {
		
	}

    public void StartMove()
    {
        moveStart = Time.time;
        startTime = moveStart;
        dTime = Time.time;
        isMoving = true;
        targetPos = this.transform.localPosition;
        startPos = this.transform.localPosition;
        targetPos.y = 0;
        journeyLength = Vector3.Distance(startPos, targetPos);
        speed = journeyLength / 3f;
        this.transform.localScale = Vector3.zero;

    }
	
	// Update is called once per frame
	void Update () {
		if(isMoving && Time.time - dTime > 0.01f )
        {
            dTime = Time.time;

            Vector3 scaleInfo = this.transform.localScale;

            if(scaleInfo.x < 0.5)
            {
                scaleInfo.x += 0.02f;
                scaleInfo.y += 0.02f;              
            }
            else if(scaleInfo.x < 1f)
            {
                scaleInfo.x += 0.02f;
                scaleInfo.y += 0.02f;
            }
            else
            {
                scaleInfo.x = 1f;
                scaleInfo.y = 1f;
            }

            this.transform.localScale = scaleInfo;            
        }
        if(isMoving)
            {
            float distCovered = (Time.time - startTime) * speed;
            Vector3 curPos;
            float fracJourney = distCovered / journeyLength;
            curPos = Vector3.Lerp(startPos, targetPos, fracJourney);
            this.transform.localPosition = curPos;

            if (Vector3.Distance(transform.localPosition, targetPos) < 10f)
                {
                Debug.Log("during time ----" + (Time.time - startTime) + "--- dist ---" + Vector3.Distance(startPos, targetPos));
                isMoving = false;
                endTime = Time.time;
                }
            }

        if(isMoving == false && Time.time - endTime > 0.5f)
            {
            Destroy(this.gameObject);
            }
        }
}
