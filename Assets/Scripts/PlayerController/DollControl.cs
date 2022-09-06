using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class DollControl : MonoBehaviour
{
    //public List<GameObject> shadows;
    public List<posTape> tape1;
    public GameObject model;
    bool replaying;
    bool replayFromStart;

    int index = 0;
    float timer = 0;
    public MeshRenderer smr;
    public GameObject line;

    int color_code = 0;

    public void reset(bool startPoint)
    {
        //line.GetComponent<LineRenderer>().sharedMaterials[0].DOFloat(0.6f, "_TRANS", .7f);
        setTrans(.4f);
        //resetAllMat();
        
        replayFromStart = startPoint;
        if(replayFromStart)
            index = 0;
        else
            index = tape1.Count-1;
        timer = 0;
        transform.position = tape1[index].pos;
        transform.rotation = tape1[index].rot;
        model.SetActive(true);
        //GetComponent<TapCheck>().enabled = true;
        replaying = true;
    }

    void endReplay()
    {
        //line.GetComponent<LineRenderer>().sharedMaterials[0].DOFloat(0.1f, "_TRANS", .7f);
        setTrans(.1f);
        //resetAllMat();
        //model.SetActive(false);
        //GetComponent<TapCheck>().enabled = false;
        replaying = false;
    }

    void OnDestroy()
    {
        //ColorManager.instance.onColorEvoke -= Replay;             //evoke on tap
        Destroy(line);
        //foreach(var a in shadows)
        //{
        //    Destroy(a);
        //}
    }

    string path;

    void Awake()
    {      
        //ColorManager.instance.onColorEvoke += Replay;             //evoke on tap
    }


    void Start()
    {
        
        /*bool startPoint = true;
         foreach (var a in shadows)
        {
            a.AddComponent<shadowTrigger>();
            a.GetComponent<shadowTrigger>().parentDoll = this;
            a.GetComponent<shadowTrigger>().startPoint = startPoint;
            startPoint = false;
        } */
        resetAllMat();
        setColor(1);
        endReplay();
    }

    void FixedUpdate()
    {
        if(!replaying)
            return;
        if(timer > tape1[index].deltaTime)
        {
            timer -= tape1[index].deltaTime;
            index  += (int)MathTool.BoolToOne(replayFromStart);
            if(index == tape1.Count -1 || index == 0)
            {
                endReplay();
                return;
            }
        }
        transform.position += Time.deltaTime * (tape1[index+(int)MathTool.BoolToOne(replayFromStart)].pos - transform.position)/(tape1[index+(int)MathTool.BoolToOne(replayFromStart)].deltaTime-timer);
        Vector3 r = Time.deltaTime * (MathTool.angelDistance(tape1[index+(int)MathTool.BoolToOne(replayFromStart)].rot, transform.rotation))/(tape1[index+(int)MathTool.BoolToOne(replayFromStart)].deltaTime-timer);
        r += transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(r);
        timer += Time.deltaTime;
    }

    public void Replay(int code)
    {
        //Debug.Log(code + " " + color_code);
        if(code == color_code)
            reset(true);
    }

    public void resetAllMat()
    {
        for(int i=0;i<smr.materials.Length;i++)
        {
            smr.materials[i] = line.GetComponent<LineRenderer>().sharedMaterials[0];
        }
        
        /* foreach(var a in shadows)
        {
            for(int i=0;i<a.GetComponent<ShadowRender>().smr.materials.Length;i++)
            {
                a.GetComponent<ShadowRender>().smr.materials[i] = line.GetComponent<LineRenderer>().materials[0];
            }
        } */

    }

    public void setColor(int code)
    {
        Color tmp = ColorManager.PalatteToColor(code)*4f;
        line.GetComponent<LineRenderer>().materials[0].SetColor("_EmissiveColor", tmp*2f);
        line.GetComponent<LineRenderer>().enabled = true;
        for(int i=0;i<smr.materials.Length;i++)
        {
            smr.materials[i].SetColor("_Emission_Color", tmp);
        }
        
        /* foreach(var a in shadows)
        {
            for(int i=0;i<a.GetComponent<ShadowRender>().smr.materials.Length;i++)
            {
                a.GetComponent<ShadowRender>().smr.materials[i].SetColor("_Emission_Color", tmp);
            }
        } */
        color_code = code;
    }

    void setTrans(float t)
    {
        line.GetComponent<LineRenderer>().materials[0].SetFloat("_TRANS", t);
        for(int i=0;i<smr.sharedMaterials.Length;i++)
        {
            smr.materials[i].SetFloat("_TRANS", t);
        }
        
        /* foreach(var a in shadows)
        {
            for(int i=0;i<a.GetComponent<ShadowRender>().smr.sharedMaterials.Length;i++)
            {
                a.GetComponent<ShadowRender>().smr.materials[i].SetFloat("_TRANS", t);
            }
        } */
    }

    
}
