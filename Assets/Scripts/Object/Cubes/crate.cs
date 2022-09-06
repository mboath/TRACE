using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour
{
    public MeshRenderer dyeMesh;
    GameObject currentTap;
    GameObject currentDye;

    public void setColor(int code)
    {
        Material[] tmp = dyeMesh.materials;
        tmp[1] = ColorManager.LoadMaterial(code, true);
        dyeMesh.materials = tmp;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject != currentTap && other.gameObject.tag == "tap")
        {
            currentTap = other.gameObject;
            setColor(currentTap.GetComponent<tap>().colorCode);
            currentTap.GetComponent<dye>().onDyeEvoke += setColor;
        }
        if(other.gameObject != currentDye && other.gameObject.GetComponent<DyableObject>() != null)
        {
            currentDye = other.gameObject;
            currentTap.GetComponent<dye>().onDyeEvoke += setColor;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject == currentTap)
        {
            currentTap.GetComponent<dye>().onDyeEvoke -= setColor;
            currentTap = null;
            setColor(0);
        }
    }

}
