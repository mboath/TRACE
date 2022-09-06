using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject SpartVFX;
    public GameObject LargeSpark;

    public static ResourceManager RManager;
    SparkControll publicSpark;

    public int MaxRedScale = 10;

    public static float maxCamDistance = 3f;

    void Start()
    {
        if(RManager != null)
            Destroy(gameObject);
        RManager = this;
        DontDestroyOnLoad(gameObject);

        publicSpark = Instantiate(LargeSpark, Vector3.zero, Quaternion.identity).GetComponent<SparkControll>();
    }

    public void setPublicSpark(Vector3 pos, Vector3 rotate, float time, int code)
    {
        StartCoroutine(spartEvoke(pos, rotate, time, code));
    }

    IEnumerator spartEvoke(Vector3 pos, Vector3 rotate, float time, int code)
    {
        yield return new WaitForSeconds(time);
        publicSpark.transform.position = pos;
        publicSpark.transform.rotation = Quaternion.LookRotation(rotate);
        publicSpark.GetComponent<SparkControll>().SetColor(code);
        publicSpark.spark.Play();
    }

}
