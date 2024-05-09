using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIChatManage : MonoBehaviour
{

    bool moveDirection = false;
    bool moveState = false;
    Vector3 curpos;
    int movedDelta = 0;
    float dTime = 0f;

    public GameObject gamePlayUI, engine, chatPopup;
    public InputField chatInputText;

    // Use this for initialization
    void Start()
    {

    }


    private void OnEnable()
    {
        chatPopup.SetActive(false);
        chatInputText.text = "";
    }
    public void OnArrowClick(GameObject chObj)
    {
        if (moveState)
            return;
        curpos = this.gameObject.transform.localPosition;
        if (chObj.transform.GetChild(0).gameObject.activeSelf)
        {
            chObj.transform.GetChild(0).gameObject.SetActive(false);
            chObj.transform.GetChild(1).gameObject.SetActive(true);
            moveDirection = true;
            moveState = true;
        }
        else
        {
            chObj.transform.GetChild(0).gameObject.SetActive(true);
            chObj.transform.GetChild(1).gameObject.SetActive(false);
            moveDirection = false;
            moveState = true;
        }
    }

    public void ChatButtonClicked(int id)
    {
        string ChatText = "";

        switch (id)
        {
            case 0:
                ChatText = this.gameObject.GetComponent<UILgChat>().tgoodLuck.text;
                break;
            case 1:
                ChatText = this.gameObject.GetComponent<UILgChat>().tThanks.text;
                break;
            case 2:
                ChatText = this.gameObject.GetComponent<UILgChat>().tGoodGame.text;
                break;
            case 3:
                ChatText = this.gameObject.GetComponent<UILgChat>().tWellPlayed.text;
                break;
            case 4:
                ChatText = this.gameObject.GetComponent<UILgChat>().tWow.text;
                break;
            case 5:
                ChatText = this.gameObject.GetComponent<UILgChat>().tOpps.text;
                break;
            case 6:
                ChatText = chatInputText.text;
                chatInputText.text = "";
                break;
        }

        if (ChatText == "" || ChatText == null)
            return;

        gamePlayUI.GetComponent<UIGamePlayManager>().Mychat(ChatText);

        engine.GetComponent<Engine>().SendMyChatToOtherPlayer(ChatText);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.gameObject.name == "Ipad")
        {
            if (moveState && movedDelta < 330 && Time.time - dTime >= 0.02f)
            {
                dTime = Time.time;
                if (moveDirection)
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 20;
                    if (movedDelta >= 335)
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
                    if (movedDelta >= 335)
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
            if (moveState && movedDelta < 195 && Time.time - dTime >= 0.02f)
            {
                dTime = Time.time;
                if (moveDirection)
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 10;
                    if (movedDelta >= 195)
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
                    if (movedDelta >= 195)
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



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIChatManage : MonoBehaviour
{

    bool moveDirection = false;
    bool moveState = false;
    Vector3 curpos;
    int movedDelta = 0;
    float dTime = 0f;

    public GameObject gamePlayUI, engine;
    // Use this for initialization
    void Start()
    {

    }

    public void OnArrowClick(GameObject chObj)
    {
        if (moveState)
            return;
        curpos = this.gameObject.transform.localPosition;
        if (chObj.transform.GetChild(0).gameObject.activeSelf)
        {
            chObj.transform.GetChild(0).gameObject.SetActive(false);
            chObj.transform.GetChild(1).gameObject.SetActive(true);
            moveDirection = true;
            moveState = true;
        }
        else
        {
            chObj.transform.GetChild(0).gameObject.SetActive(true);
            chObj.transform.GetChild(1).gameObject.SetActive(false);
            moveDirection = false;
            moveState = true;
        }
    }

    public void ChatButtonClicked(int id)
    {
        string ChatText = "";

        switch(id)
        {
            case 0:
                ChatText = this.gameObject.GetComponent<UILgChat>().tgoodLuck.text;
                break;
            case 1:
                ChatText = this.gameObject.GetComponent<UILgChat>().tThanks.text;
                break;
            case 2:
                ChatText = this.gameObject.GetComponent<UILgChat>().tGoodGame.text;
                break;
            case 3:
                ChatText = this.gameObject.GetComponent<UILgChat>().tWellPlayed.text;
                break;
            case 4:
                ChatText = this.gameObject.GetComponent<UILgChat>().tWow.text;
                break;
            case 5:
                ChatText = this.gameObject.GetComponent<UILgChat>().tOpps.text;
                break;
        }

        gamePlayUI.GetComponent<UIGamePlayManager>().Mychat(ChatText);

        engine.GetComponent<Engine>().SendMyChatToOtherPlayer(ChatText);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.gameObject.name == "Ipad")
        {
            if (moveState && movedDelta < 330 && Time.time - dTime >= 0.02f)
            {
                dTime = Time.time;
                if (moveDirection)
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 20;
                    if (movedDelta >= 335)
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
                    if (movedDelta >= 335)
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
            if (moveState && movedDelta < 195 && Time.time - dTime >= 0.02f)
            {
                dTime = Time.time;
                if (moveDirection)
                {
                    Vector3 tmpPos = curpos;
                    movedDelta += 10;
                    if (movedDelta >= 195)
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
                    if (movedDelta >= 195)
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
}*/
