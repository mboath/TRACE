using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapCheck : MonoBehaviour
{
    public Transform groundDetect;
    public float groundDistance;
    public GameObject currentTap = null;
    public LayerMask tapLayer;
    float radius = 0.3f;
    public int tapchecknum;
    
    
    void tapCheck()
    {
        /*bool isTap = Physics.CheckSphere(groundDetect.position, groundDistance, tapLayer);
        if(isTap)
        {
            if(currentObject == null)
            {
                currentObject.GetComponent<tap>().evokeTap();
            }
        }
        else
        {
            if(currentObject != null)
            {
                currentObject.GetComponent<tap>().disableTap();
                currentObject = null;
            }           
        }*/
        Collider[] taps = Physics.OverlapSphere(groundDetect.position, radius, tapLayer);
        tapchecknum = taps.Length;
        if(taps.Length == 0)
        {
            if(currentTap != null)
            {
                currentTap.GetComponent<tap>().disableTap();
                currentTap.GetComponent<tap>().controlled = false;
                currentTap = null;
            }
            return;
        }
        tap tmp = taps[0].GetComponent<tap>();
        if(currentTap == tmp.gameObject)
            return;
        else
            if(currentTap != null)
            {
                currentTap.GetComponent<tap>().controlled = false;
                currentTap.GetComponent<tap>().disableTap();
            }
                
        currentTap = tmp.gameObject;
        if(tmp == null)
        {
            Debug.Log("tap check error");
            return;
        }
        tmp.evokeTap();
        tmp.controlled = true;

    }

    void Update()
    {
        tapCheck();
    }
}
