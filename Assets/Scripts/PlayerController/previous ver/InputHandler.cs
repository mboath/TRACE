using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct keyRecorder
{
    public float startTime;
    public float endTime;
    public string keycode;
}

public class InputHandler : MonoBehaviour
{
    Dictionary<string, string> keyList = new Dictionary<string, string>()
    {
        {"up", "u"},
        {"left","l"},
        {"right","r"},
        {"down","d"},
        {"z","z"},
        {"x","x"}
    };
    string inputlist{
        get{return GetComponent<playerController>().inputList;}
        set{GetComponent<playerController>().inputList = value;}
    }
    public List<keyRecorder> keyTape;
    Vector3 startPos;
    Quaternion startRotation;
    bool recordmod = false;
    CurtainCanvas cc;

    void getInput()
    {
        inputlist = "";
        foreach (var dic in keyList)
        {
            if (Input.GetKey(dic.Key))
                inputlist += dic.Value;
        }
    }

    void readTape(float timer)
    {
        inputlist = "";
        foreach (var item in keyTape)
        {
            if (item.endTime == 0f)
                inputlist += item.keycode;
        }
        /*if(GetComponent<Rigidbody>().velocity.magnitude <= 0.1f)
            Time.timeScale = .05f;
        else
            Time.timeScale = 1f;*/
    }

    void Awake()
    {
        cc = FindObjectOfType<CurtainCanvas>();
    }

    void Update()
    {
        if (recordmod)
            return;
        if (Input.GetKeyDown("z"))
        {
            recordmod = true;
            StartCoroutine(keyRecord());
            return;
        }
        getInput();
    }

    IEnumerator keyRecord()
    {
        float timer = Time.time;
        startPos = transform.position;
        startRotation = transform.rotation;

        cc.openCurtain();
        yield return new WaitForSeconds(.1f);

        keyTape = new List<keyRecorder>();
        yield return null;
        while (!Input.GetKeyDown("z"))
        {
            writeTape(timer);
            readTape(timer);
            yield return null;
        }
        cc.closeCurtain();
        Time.timeScale = 1f;
        for (int i = 0; i < keyTape.Count; i++)
        {
            if (keyTape[i].endTime == 0)
            {
                keyTape[i] = editEndtime(keyTape[i], Time.time - timer);
            }
        }
        recordmod = false;
        GameObject Doll =  Instantiate(Resources.Load("Doll"), startPos, startRotation) as GameObject;
        Doll.GetComponent<dollHandler>().setTape(keyTape);
        Doll.GetComponent<dollHandler>().period = Time.time - timer;
    }

    void writeTape(float timer)
    {
        foreach (var dic in keyList)
        {
            if (Input.GetKeyDown(dic.Key))
            {
                keyRecorder tmp = new keyRecorder();
                tmp.keycode = dic.Value;
                tmp.startTime = Time.time - timer;
                keyTape.Add(tmp);
            }
            Time.timeScale = 1f;
            if (Input.GetKeyUp(dic.Key))
            {
                for (int i = 0; i < keyTape.Count; i++)
                {
                    if (keyTape[i].keycode == dic.Value && keyTape[i].endTime == 0f)
                    {
                        keyTape[i] = editEndtime(keyTape[i], Time.time - timer);
                    }
                }
            }
        }
    }

    keyRecorder editEndtime(keyRecorder tmp, float endTime)
    {
        keyRecorder tmp2 = new keyRecorder();
        tmp2 = tmp;
        tmp2.endTime = endTime;
        return tmp2;
    }
}
