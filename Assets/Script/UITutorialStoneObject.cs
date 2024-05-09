using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialStoneObject : MonoBehaviour {

    public Vector3 targetPos;
    Vector3 startPos;
    public bool isMoving = false;
    float dTime = 0f, xDelta, yDelta, journeyLength;
    float startTime, speed = 600.0f;
    // Use this for initialization
    void Start()
    {

    }

    public void MoveToPosition(Vector3 tgPos)
    {
        isMoving = true;
        startPos = this.transform.localPosition;
        targetPos = tgPos;

        journeyLength = Vector3.Distance(startPos, targetPos);
        startTime = Time.time;
        speed = journeyLength * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fracJourney = distCovered / journeyLength;

            transform.localPosition = Vector3.Lerp(startPos, targetPos, fracJourney);

            if (Vector3.Distance(transform.localPosition, targetPos) < 10f)
            {
                Debug.Log("during time ----" + (Time.time - startTime) + "--- dist ---" + Vector3.Distance(startPos, targetPos));
                isMoving = false;
            }
        }
    }
}
