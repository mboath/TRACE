using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpCamera : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    // Start is called 0before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            b.SetActive(!b.activeSelf);
            FindObjectOfType<FPController>().enabled = ! FindObjectOfType<FPController>().enabled;
            a.SetActive(!a.activeSelf);
        }
    }
}
