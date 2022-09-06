using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCube : MonoBehaviour
{
    public GameObject butterflies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FPController>()!=null)
        {
            other.GetComponent<FPController>().recorderModeActivate(false);
            if(!butterflies.activeSelf)
            {
                butterflies.SetActive(true);
            }
        }
    }
}
