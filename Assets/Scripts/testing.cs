using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public GameObject lights;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            lights.SetActive(!lights.activeSelf);
        }
    }
}
