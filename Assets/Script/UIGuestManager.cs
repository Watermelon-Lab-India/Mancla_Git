using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuestManager : MonoBehaviour {

	public Sprite[] characters;
	public InputField characterName;
	int characId = 0;
	public Image characterImage;
	// Use this for initialization
	void Start () {
		
	}

	public void Leftcharacter()
	{
		if (characId > 0) {
			characId--;
			characterImage.sprite = characters [characId];
			characterImage.SetNativeSize();
		}

        Vector3 pos = characterImage.gameObject.transform.localPosition;

        switch (characId)
        {
            case 0:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 1:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 2:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 3:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 4:
                pos.y = -16;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 5:
                pos.y = -21;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 6:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 7:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
        }
	}

	public void RightCharacter()
	{
		if (characId < 7) {
			characId++;
			characterImage.sprite = characters [characId];
			characterImage.SetNativeSize();
		}

        Vector3 pos = characterImage.gameObject.transform.localPosition;

        switch (characId)
        {
            case 0:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 1:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 2:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 3:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 4:
                pos.y = -16;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 5:
                pos.y = -21;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 6:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
            case 7:
                pos.y = -10;
                characterImage.gameObject.transform.localPosition = pos;
                break;
        }
    }

	public void SubmitProfile()
	{
		if (characterName.text == "" || characterName.text == null)
			return;

        //Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.RegistrationComplete);
        //myEvent.name = "Login as Guest";
        //Kochava.Tracker.SendEvent(myEvent);

        Firebase.Analytics.FirebaseAnalytics.LogEvent(
          Firebase.Analytics.FirebaseAnalytics.EventLogin,
          new Firebase.Analytics.Parameter[] {
            new Firebase.Analytics.Parameter(
              Firebase.Analytics.FirebaseAnalytics.ParameterMethod, "Guest"),
          }
        );

        string characAvatar = characId.ToString();
		this.transform.parent.gameObject.GetComponent<UIRegisterManage> ().RegisterUser (characterName.text, characAvatar);
        GoogleMobileAdsDemoScript.Instance.RequestTopBanner();
	}

	void Awake()
	{
		characterImage.sprite = characters [characId];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
