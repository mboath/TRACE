using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtain : MonoBehaviour
{
    Vector3 startScale = new Vector3(0f,0f,0f);
    Vector3 endScale = new Vector3(20f,20f,0f);
    float time = .1f;
    public void setCurtain(bool ac)
    {
        if(ac)
        {
            StartCoroutine(mask(startScale, endScale, true));
        }
        else
        {
            StartCoroutine(mask(endScale, startScale, false));
        }
    }

    IEnumerator mask(Vector3 startScale, Vector3 endScale, bool ac)
    {
        float startTime = Time.time;
        float timer;
        while((timer = Time.time - startTime) <= time)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, timer/time);
            yield return null;
        }
        transform.localScale = endScale;
    }
}
