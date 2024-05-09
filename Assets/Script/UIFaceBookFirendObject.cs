using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFaceBookFirendObject : MonoBehaviour
{

    public GameObject onlineState, onLineState_1, playState, playState_1;
    public Text playerName;
    public RawImage character;

    // Use this for initialization
    void Start()
    {

    }

    public void InitFriendStat(bool onlineState, string plName)
    {
        if (onlineState)
        {
            onLineState_1.SetActive(true);
        }
        else
        {
            onLineState_1.SetActive(false);
        }

        playState_1.SetActive(false);

        playerName.text = plName;
    }

    public void SelectPlayer()
    {
        if (!playState_1.activeSelf)
        {
            playState_1.SetActive(true);
            this.transform.parent.parent.parent.gameObject.GetComponent<UIFaceBookPlay>().PlayerSelected(this.gameObject.name);
        }
        else
        {
            playState_1.SetActive(false);
        }
    }

    public void SelectPlayerPlayState(bool state)
    {
        if (state)
        {
            playState_1.SetActive(true);
        }
        else
        {
            playState_1.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
