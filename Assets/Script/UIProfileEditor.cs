using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIProfileEditor : MonoBehaviour {

	int curSelCharacter = 0;

	public InputField characterName;

	public Image character;

	public Sprite[] characterList;
    public Image[] CharacterImages;

	public GameObject engine;
	// Use this for initialization
	void Start () {
		
	}

	public void SetCurrentInfo()
	{
        curSelCharacter = int.Parse(engine.GetComponent<UIManager>().playerInfo.characterName);

		character.sprite = characterList [curSelCharacter];

        for(int i = 0; i < 8; i ++)
        {
            if(i == curSelCharacter)
            {
                CharacterImages[i].gameObject.SetActive(true);
            }
            else
            {
                CharacterImages[i].gameObject.SetActive(false);
            }
        }

        //		character.SetNativeSize ();

        characterName.text = engine.GetComponent<UIManager>().playerInfo.playerName;
	}

	public void SubmitProfileInfo()
	{
		if (characterName.text == null || characterName.text == "")
			return;
		
		engine.GetComponent<UIManager> ().SubmitPlayerProfile (characterName.text, curSelCharacter.ToString ());
	}

	public void ChooseNext()
	{
		if (curSelCharacter < 7) {
			curSelCharacter++;

            for (int i = 0; i < 8; i++)
            {
                if (i == curSelCharacter)
                {
                    CharacterImages[i].gameObject.SetActive(true);
                }
                else
                {
                    CharacterImages[i].gameObject.SetActive(false);
                }
            }

            //			character.sprite = characterList [curSelCharacter];
            //			character.SetNativeSize ();
        }
    }

	public void ChoosePrev()
	{
		if (curSelCharacter > 0) {
			curSelCharacter--;

            for (int i = 0; i < 8; i++)
            {
                if (i == curSelCharacter)
                {
                    CharacterImages[i].gameObject.SetActive(true);
                }
                else
                {
                    CharacterImages[i].gameObject.SetActive(false);
                }
            }
		}
    }

	void Awake()
	{
	}

	// Update is called once per frame
	void Update () {
		
	}
}
