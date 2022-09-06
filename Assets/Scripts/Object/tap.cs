using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class tap : MonoBehaviour
{

    public GameObject coreTap;
    Vector3 boxPos = new Vector3(0f,0.15f,0f);
    Vector3 halfEx = new Vector3(0.6f, .05f, .6f);
    public int colorCode;
    public MeshRenderer dyeMesh;
    public bool state = false;


    public event Action onTapEvoke;
    public event Action onTapDisable;

    List<Collision> touchingObject = new List<Collision>();

    public bool controlled = false;

    void Awake()
    {
        //GetComponent<dye>().onDyeEvoke += setColor;
        
        dyeMesh.material = new Material(Shader.Find("HDRP/Lit"));
        dyeMesh.material.SetColor("_BaseColor", Color.black);
        setColor(colorCode);
    }

    public void evokeTap()
    {
        //ColorManager.instance.colorEvoke(colorCode);
        if(state)
            return;
        dyeMesh.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(colorCode)*4f);
        if(onTapEvoke != null)
            onTapEvoke();
        state = true;
        
        
    }

    public void disableTap()
    {
        if(!state)
            return;
        dyeMesh.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(colorCode));
        state = false;
        if(onTapDisable != null)
            onTapDisable();
        //ColorManager.instance.colorDisable(colorCode);
    }

    public void setColor(int code)
    {
        
        colorCode = code;
        Color c = ColorManager.PalatteToColor(code);
        if(state)
        {
            if(colorCode != 0)
                c *= 4f;
        }
        //dyeMesh.material = ColorManager.LoadMaterial(code, true);

        dyeMesh.material.SetColor("_EmissiveColor", c);
            
    }

    void Update()
    {
        checkTap();
        /* if(touchingObject.Count > 0)
            evokeTap();
        else
            disableTap(); */
        /*if(controlled)
            return;
        if(touchingObject.Count > 0 || FindObjectOfType<FPController>().currentObject == gameObject)
            evokeTap();
        else
            disableTap();
        if(colorCode == 0 && rootCube.color_code != 0)
            setColor(rootCube.color_code);*/
    }

    public LayerMask evokeTapLayer;

    public void checkTap()
    {
        Collider[] tapCheck = Physics.OverlapBox(transform.position + new Vector3(0f, .16f, 0f), new Vector3(.5f, .1f, .5f), Quaternion.identity,  evokeTapLayer);
        if(tapCheck.Length > 0)
            evokeTap();
        else
            disableTap();
    }

    /* private void OnCollisionEnter(Collision Other)
    {
        Debug.Log(Other.gameObject.name);

        if(Other.gameObject.tag == "bullet" || Other.gameObject.tag == "Ground")
            return;
        foreach(var a in touchingObject)
        {
            if(Other == a)
                return;
        }
        touchingObject.Add(Other);
    }

    private void OnCollisionExit(Collision Other)
    {
        touchingObject.Remove(Other);
    } */


}
