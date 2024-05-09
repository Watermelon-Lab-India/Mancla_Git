using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using UnityEngine.UI;

public class CodeUILoading : MonoBehaviour
{ 
         public Image spriteImage;
    Quaternion rotQua;
    Vector3 rot;
    float startTime = 0f;
// Start is called before the first frame update
    void Start()
        {
        
        }

    private void OnEnable()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteImage != null)
        {
            rotQua = spriteImage.transform.localRotation;
            rot = rotQua.eulerAngles;
            rot.z -= 4;
            rotQua.eulerAngles = rot;
            spriteImage.transform.localRotation = rotQua;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Time.time - startTime >= 30f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
