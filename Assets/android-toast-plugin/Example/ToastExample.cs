using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToastPlugin;

public class ToastExample : MonoBehaviour {

	public static ToastExample Instance = null;


	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}

	}
	// Use this for initialization
	void Start () 
	{
        //Show a short toast message

	}

	public void Toast(){
		ToastHelper.ShowToast("Ads Is Not Available.", false);
	}
}
