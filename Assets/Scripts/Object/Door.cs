using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    //Vector3 pos1 = new Vector3(15.63f,-2.45f,-10.72f);
    //Vector3 pos2 = new Vector3(15.63f,-2.45f,-10.35f);
    Vector3 moveToward = new Vector3(1.5f, 0f, 0f);
    Vector3 endPos;
    Vector3 originPos;
    

    public bool is_opened = false;
    public bool moving = false;

    public GameObject childDoor;
    const float moveSpeed = 20f;
    public bool closeable = true;

    
    public MeshRenderer mr;
    public int color_code;

    public tap RootTap;

    void setcolor(int code)
    {
        if(color_code == code)
            return;
        //Debug.Log(code);
        //mr.material = ColorManager.LoadMaterial(code, true);
        mr.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(code));
        color_code = code;

    }

    public void setopen()
    {
        if(is_opened)
            return;
        else
            is_opened = true;
        mr.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code) * 4f);
        StopAllCoroutines();
        StartCoroutine(openDoor());
    }

    public void setclose()
    {
        if(!closeable)
            return;
        if(!is_opened)
            return;
        else
            is_opened = false;
        mr.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code));
        StopAllCoroutines();
        StartCoroutine(closeDoor());
    }

    IEnumerator openDoor()
    {
        moving = true;
        float openTime = (endPos-transform.localPosition).magnitude / moveSpeed;
        childDoor.transform.DOLocalMove(endPos, openTime);
        yield return new WaitForSeconds(openTime);
        moving = false;
    }

    IEnumerator closeDoor()
    {
        float closeTime = (originPos-transform.localPosition).magnitude / moveSpeed;
        childDoor.transform.DOLocalMove(originPos, closeTime);
        yield return new WaitForSeconds(closeTime);
        moving = false;
    }

    void Awake()
    {
        originPos = childDoor.transform.localPosition;
        endPos = originPos + moveToward;
        /* dye[] components = GetComponentsInChildren<dye>();
        //Debug.Log(components.Length);
        foreach(var a in components)
            a.onDyeEvoke += setcolor; */
        //setcolor(0);
        
    }

    void Start()
    {
        RootTap.onTapEvoke += setopen;
        RootTap.onTapDisable += setclose;
        mr.material = new Material(Shader.Find("HDRP/Lit"));
        mr.material.SetColor("_BaseColor", Color.black);
        mr.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code));
    }
}
