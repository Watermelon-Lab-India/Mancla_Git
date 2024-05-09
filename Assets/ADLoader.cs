using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADLoader : MonoBehaviour
{

    public bool top;
    public bool bottom;

    private void OnEnable()
    {
        //Debug.Log("Enable ADLoader : "+this.transform.parent.name);
        if (top)
        {
            GoogleMobileAdsDemoScript.Instance.ShowBanner();
        }
        else {
            GoogleMobileAdsDemoScript.Instance.HideBanner();
        }

        if (bottom)
        {
            GoogleMobileAdsDemoScript.Instance.ShowBannerBottom();
        }
        else {
            GoogleMobileAdsDemoScript.Instance.HideBannerBottom();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDisable()
    {
        //Debug.Log("Disable ADLoader");
        /*GoogleMobileAdsDemoScript.Instance.HideBanner();
        GoogleMobileAdsDemoScript.Instance.HideBannerBottom();*/
        /*GoogleMobileAdsDemoScript.Instance.DestroyBanner();
        GoogleMobileAdsDemoScript.Instance.DestroyBannerBottom();*/
    }

}
