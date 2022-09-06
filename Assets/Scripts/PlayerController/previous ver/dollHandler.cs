using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollHandler : MonoBehaviour
{
    List<keyRecorder> keyTape;
    float timer = 0f;
    public float period;
    Vector3 startPos;
    Quaternion startAngle;

    string inputlist{
        get{return GetComponent<playerController>().inputList;}
        set{GetComponent<playerController>().inputList = value;}
    }

    public void setTape(List<keyRecorder> input)
    {
        keyTape = new List<keyRecorder>(input);
        foreach(var item in keyTape)
        {
            Debug.Log(item.keycode + " " + item.startTime + " " + item.endTime);
        }
    }

    void readTape()
    {
        inputlist = "";
        foreach (var item in keyTape)
        {
            float currentTime = Time.time - timer;
            if (MathTool.interval(currentTime, item.startTime, item.endTime))
                inputlist += item.keycode;
        }
    }

    void Awake()
    {
        startPos = transform.position;
        startAngle = transform.rotation;
        timer = Time.time;
    }

    void Start()
    {
        
    }

    void Update()
    {
        readTape();
        if(Time.time - timer > period)
        {
            GameObject Doll =  Instantiate(Resources.Load("Doll"), startPos, startAngle) as GameObject;
            Doll.GetComponent<dollHandler>().setTape(keyTape);
            Doll.GetComponent<dollHandler>().period = period;
            Destroy(gameObject);
        }
            
    }
}
