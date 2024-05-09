using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DecideDeviceType : MonoBehaviour
{

    public GameObject ipad, phone;

    public bool musicState = true, soundState = true;

    public GameObject soundObjectAndroid, soundObjectIpad;

    const string GAME_BACK_MUSIC = "BACK_MUSIC";
    const string GAME_SOUND = "GAME_SOUND";

    public GameObject musicStateIcon, soundStateIcon, soundStateIcon1;
    public string privacyURL = "";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClickEvent()
    {
        if (soundState)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }

    }

    public void OpenPrivacyPolicyURL()
    {
        Application.OpenURL(privacyURL);
    }

    public void SetSoundState()
    {
        soundState = !soundState;
        if (soundState)
        {
            PlayerPrefs.SetInt(GAME_SOUND, 1);
        }
        else
        {
            PlayerPrefs.SetInt(GAME_SOUND, 0);
        }
    }

    public void SetGameMusicState()
    {
        if (musicState)
        {
            if (phone.activeSelf)
            {
                soundObjectAndroid.GetComponent<AudioSource>().enabled = true;
            }
            else
            {
                soundObjectIpad.GetComponent<AudioSource>().enabled = true;
            }
        }
        else
        {
            if (phone.activeSelf)
            {
                soundObjectAndroid.GetComponent<AudioSource>().enabled = false;
            }
            else
            {
                soundObjectIpad.GetComponent<AudioSource>().enabled = false;
            }
        }
    }

    public void GameMusicState()
    {
        musicState = !musicState;

        if (musicState)
        {
            PlayerPrefs.SetInt(GAME_BACK_MUSIC, 1);
        }
        else
        {
            PlayerPrefs.SetInt(GAME_BACK_MUSIC, 0);
        }

        SetGameMusicState();
    }

    private void Awake()
    {
        Resolution curResolution = Screen.currentResolution;
        Debug.Log(Application.platform);

        int curWidth = curResolution.width;
        int curHeight = curResolution.height;

        float sizeInfo;

        if (curWidth > curHeight)
        {
            sizeInfo = (float)curHeight / (float)curWidth;
        }
        else
        {
            sizeInfo = (float)curWidth / (float)curHeight;
        }

        if (Application.platform == RuntimePlatform.Android)
        {

            ipad.SetActive(false);
            phone.SetActive(true);


        }
        else
        {
            ipad.SetActive(false);
            phone.SetActive(true);


            //if (sizeInfo > 0.72f)
            //            {
            //             ipad.SetActive(true);
            //             phone.SetActive(false);
            //            }
            //        	else
            //            {
            //             ipad.SetActive(false);
            //             phone.SetActive(true);
            //            }


        }
        if (PlayerPrefs.HasKey(GAME_BACK_MUSIC))
        {
            int state = PlayerPrefs.GetInt(GAME_BACK_MUSIC);
            if (state > 0)
            {
                musicState = true;
                musicStateIcon.SetActive(false);

            }
            else if (state == 0)
            {
                musicState = false;
                musicStateIcon.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt(GAME_BACK_MUSIC, 1);
            musicState = true;
            musicStateIcon.SetActive(false);
        }

        if (PlayerPrefs.HasKey(GAME_SOUND))
        {

            int state = PlayerPrefs.GetInt(GAME_SOUND);
            if (state > 0)
            {
                soundState = true;
                soundStateIcon.SetActive(false);
                soundStateIcon1.SetActive(true);
            }
            else if (state == 0)
            {
                soundState = false;
                soundStateIcon.SetActive(true);
                soundStateIcon1.SetActive(false);
            }

        }
        else
        {
            PlayerPrefs.SetInt(GAME_SOUND, 1);
            soundState = true;
            soundStateIcon.SetActive(false);
            soundStateIcon1.SetActive(true);
        }
        SetGameMusicState();
    }
}
