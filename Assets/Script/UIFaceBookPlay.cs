using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIFaceBookPlay : MonoBehaviour
{
    public GameObject engine;
    public GameObject scrollContents;
    public GameObject fbFriendObj, scrollViewport;
    public GameObject loadingAnim;
    public List<string> friendsIDList = new List<string>();
    public List<string> friendsNameList = new List<string>();    

    public ScrollRect scroll;

    static public int tableIndex = 0;
    public static UIFaceBookPlay _instance;

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Vector2 pos = scroll.normalizedPosition;

        pos.x = 0;

        scroll.normalizedPosition = pos;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("ui facebok");
    }

    public IEnumerator WaitForLogin()
    {
        yield return new WaitForSeconds(5f);
        //        ShowPlayerList(tableIndex);
    }

    public Image opponentImage;
    public Text oppoName;
    public Button shopButton;

    public void PlayWithFriend()
    {
        int nCount = scrollContents.transform.childCount;
        if (nCount < 1)
            return;

        if (engine.gameObject.GetComponent<UIManager>().playerInfo.coins < 100)
        {
            shopButton.onClick.Invoke();
            return;
        }

        for (int i = 0; i < nCount; i++)
        {
            if (scrollContents.transform.GetChild(i).gameObject.GetComponent<UIFaceBookFirendObject>().playState_1.activeSelf)
            {                
                string friendName = scrollContents.transform.GetChild(i).gameObject.GetComponent<UIFaceBookFirendObject>().playerName.text;
                RawImage rImage = scrollContents.transform.GetChild(i).gameObject.GetComponent<UIFaceBookFirendObject>().character;
                Texture2D TheTexture = rImage.texture as Texture2D;
                Sprite sptImage = Sprite.Create(TheTexture, new Rect(0, 0, 128, 128), new Vector2());
                
                opponentImage.sprite = sptImage;
                oppoName.text = friendName;
                int nIndex = getFriendNameListIndex(friendName);
                if (nIndex >= 0)
                {
                    engine.GetComponent<Engine>().PlayGameWithFaceBook(tableIndex, friendsIDList[nIndex]);
                    Debug.Log("[UIFaceBookPlay::PlayWithFriend] id : " + friendsIDList[nIndex] + ",  name : " + friendName);
                    
//                    engine.GetComponent<Engine>().GamePlayRequest(friendsIDList[nIndex]);
                    loadingAnim.SetActive(true);
                    break;
                }
            }
        }
    }

    public void InviteFriends()
    {
        NativeShare myNativeShare = new NativeShare();
        if (myNativeShare == null)
        {
            Debug.Log("shared failed(?)");
            return;
        }

        string shareText = "";
        shareText += "Android : https://play.google.com/store/apps/details?id=com.water.mancalabestboardgame";
        shareText += "\nIOS : https://apps.apple.com/us/app/mancala-and-friends/id1041732970";
        myNativeShare.SetText(shareText);
        myNativeShare.Share();
    }

    public void ShowPlayerList(int tableId)
    {
        Debug.Log("FB room ShowPlayerList called");
        tableIndex = tableId;

        ResetScrollContents();

        engine.GetComponent<UIFaceBookManager>().isFirstLogin = false;
        engine.GetComponent<UIFaceBookManager>().FaceBookLogin();
    }

    public void ShowOnlinePlayer(List<string> onlinePl)
    {
        int[] ids = new int[onlinePl.Count];

        for(int i = 0; i < onlinePl.Count; i ++)
        {
            for(int j = 0; j < friendsIDList.Count; j ++)
            {
                if(onlinePl[i] == friendsIDList[j])
                {
                    ids[i] = j;
                }
            }
        }

        for(int i = 0; i < onlinePl.Count; i ++)
        {
            for(int j = 0; j < scrollContents.transform.childCount; j ++)
            {
                GameObject gmobj = scrollContents.transform.GetChild(j).gameObject;

                if (gmobj.GetComponent<UIFaceBookFirendObject>().playerName.text == friendsNameList[ids[i]])
                {
                    gmobj.GetComponent<UIFaceBookFirendObject>().InitFriendStat(true, friendsNameList[ids[i]]);
                }
            }
        }
    }

    public void ShowPlayerList()
    {
        int cnt = friendsNameList.Count;
        if (cnt < 1)
            return;
        
        float maxHeight = cnt * 110f;        
        Debug.Log(scrollContents.GetComponent<RectTransform>().sizeDelta);
        Vector2 sDelta = scrollContents.GetComponent<RectTransform>().sizeDelta;
        scrollContents.GetComponent<RectTransform>().sizeDelta = new Vector2(sDelta.x, maxHeight);
        Vector2 pos = scrollContents.GetComponent<RectTransform>().anchoredPosition;
        pos.y = 200 - maxHeight / 2;
        scrollContents.GetComponent<RectTransform>().anchoredPosition = pos;

        for (int i = 0; i < cnt; i++)
        {
            GameObject gmObj = GameObject.Instantiate(fbFriendObj) as GameObject;
            gmObj.SetActive(true);
            gmObj.transform.parent = scrollContents.transform;
            Vector2 sPos = gmObj.GetComponent<RectTransform>().anchoredPosition;
            if(cnt < 5)
            {
                sPos.y = maxHeight / 2 - i * 105 - 50 + pos.y;
            }
            else
            {
                sPos.y = maxHeight / 2 - i * 105 - 50;
            }
            sPos.x = 0;
            gmObj.name = i.ToString();
            gmObj.GetComponent<RectTransform>().anchoredPosition = sPos;
            gmObj.transform.localScale = new Vector3(1f, 1f, 1f);
            gmObj.GetComponent<UIFaceBookFirendObject>().InitFriendStat(false, friendsNameList[i]);
        }

        scrollContents.transform.parent = scrollViewport.transform;



    }


    public void ResetScrollContents()
    {
		
        int nCount = scrollContents.transform.childCount;
        if (nCount < 1)
            return;

        for (int i = 0; i < nCount; i++)
        {
            Destroy(scrollContents.transform.GetChild(i).gameObject);
        }

    }
    
    public void setFriendPicture(string friendID, string picUrl)
    {
        Debug.Log("[UIFaceBookPlay::setFriendPicture] friendID : " + friendID + "\n" + "pictureUrl : " + picUrl);

        int nIndex = getFriendIDListIndex(friendID);
        if (nIndex >= 0)
        {
            StartCoroutine(getTextureFromUrl(nIndex, picUrl));
        }
    }

    public int getFriendIDListIndex(string id)
    {
        int nIndex = -1;
        for (int i = 0; i < friendsIDList.Count; i++)
        {
            if (friendsIDList[i] == id)
            {
                nIndex = i;
                break;
            }
        }

        return nIndex;
    }

    public int getFriendNameListIndex(string name)
    {
        int nIndex = -1;
        for (int i = 0; i < friendsNameList.Count; i++)
        {
            if (friendsNameList[i] == name)
            {
                nIndex = i;
                break;
            }
        }

        return nIndex;
    }

    public IEnumerator getTextureFromUrl(int nIndex, string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("[UIFaceBookPlay::getTextureFromUrl] www.error");
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            var objItem = scrollContents.transform.GetChild(nIndex).gameObject;
            objItem.GetComponent<UIFaceBookFirendObject>().character.GetComponent<RawImage>().texture = myTexture;
        }
    }

    public void PlayerSelected(string name)
    {
        int id = int.Parse(name);
        for (int i = 0; i < scrollContents.transform.childCount; i++)
        {
            if (i == id)
            {
                scrollContents.transform.GetChild(i).gameObject.GetComponent<UIFaceBookFirendObject>().SelectPlayerPlayState(true);
            }
            else
            {
                scrollContents.transform.GetChild(i).gameObject.GetComponent<UIFaceBookFirendObject>().SelectPlayerPlayState(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
