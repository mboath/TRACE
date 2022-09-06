using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeEffect : MonoBehaviour
{
    public float maxdistance = 5f;
    public float lowest = .6f;
    public float endColor = .5f;
    public float waitTime = .5f;
    Material mat;

    public void UpdateColor(Color c, Vector3 pos)
    {
        float distance = (transform.position - pos).magnitude;
        float ratio = lowest + (1-distance/maxdistance)*(1-lowest);
        StartCoroutine(setColor(c*ratio));
    }

    IEnumerator setColor(Color c)
    {
        GetComponent<MeshRenderer>().material.SetColor("_EMISSIVE", c);
        yield return new WaitForSeconds(waitTime);
        GetComponent<MeshRenderer>().material.SetColor("_EMISSIVE", c*endColor);
    }

    void Awake()
    {
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Shader Graphs/emission"));
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
