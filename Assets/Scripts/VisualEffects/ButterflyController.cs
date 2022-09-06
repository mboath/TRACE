using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    public DyableObject tmp;

    public GameObject[] butterflies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tmp.color_code != 0)
        {
            foreach(var a in butterflies)
                a.SetActive(true);
        }
    }
}
