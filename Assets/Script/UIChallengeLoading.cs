using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using UnityEngine.UI;

public class UIChallengeLoading : MonoBehaviour {

    public Image spriteImage;
    Quaternion rotQua;
    Vector3 rot;
    float startTime = 0f;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
        }
    private void OnEnable()
        {
        startTime = Time.time;
        }

    // Update is called once per frame
    void Update () {
        if (spriteImage != null)
            {
            rotQua = spriteImage.transform.localRotation;
            rot = rotQua.eulerAngles;
            rot.z -= 4;
            rotQua.eulerAngles = rot;
            spriteImage.transform.localRotation = rotQua;
            }

        if (Input.GetKeyDown(KeyCode.Escape) || Time.time - startTime >= 40f)
            {
            //            if(PhotonNetwork.CurrentRoom != null)
            //                {
            //                PhotonNetwork.LeaveRoom();
            //                }

            //            PhotonNetwork.Disconnect();
            //            this.gameObject.transform.parent.parent.Find("Engine").gameObject.GetComponent<NetworkingManager>().DisconnectCurrentConnection();
            this.gameObject.SetActive(false);
            }
        }
}
