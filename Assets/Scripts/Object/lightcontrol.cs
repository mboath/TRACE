using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontrol : MonoBehaviour
{
    public dye key;
    public MeshRenderer mr;
    Color c;
    public GameObject pointLight;

    void Awake()
    {
        mr.material = new Material(Shader.Find("Shader Graphs/emission"));
        c = ColorManager.PalatteToColor(0);
        //mr.material.SetColor("_EMISSIVE", c);
        pointLight.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        key.onDyeEvoke += checkKey;
    }

    void checkKey(int code)
    {
        Debug.Log("lantern evoked");
        if(code!=0)
        {
            enlightenLight();
        }
    }



    void enlightenLight()
    {
        
        mr.material.SetColor("_EMISSIVE", c*4f);
        pointLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
