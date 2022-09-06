using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SparkControll : MonoBehaviour
{
    public float destoryTime;
    float startTime;

    public VisualEffect spark;
    public Gradient sparkColor;

    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        sparkColor = spark.GetGradient("_EMISSIVE");
    }

    public void SetColor(int code)
    {
        Color hdr = ColorManager.PalatteToColor(code);
        GradientColorKey[] keys = sparkColor.colorKeys;
        keys[0] = new GradientColorKey(hdr, sparkColor.colorKeys[0].time);
        sparkColor.SetKeys(keys, sparkColor.alphaKeys);
        spark.SetGradient("_EMISSIVE", sparkColor);
    }

    void testGradient()
    {
        GradientColorKey[] keys = sparkColor.colorKeys;
        GradientAlphaKey[] alphakeys = sparkColor.alphaKeys;

        foreach(var a in keys)
            Debug.Log("colorkey" + a.color + " " + a.time);
        foreach(var a in alphakeys)
            Debug.Log("alphakey" + a.alpha + " " + a.time);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
