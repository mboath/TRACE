using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyableObject : MonoBehaviour
{
    MeshRenderer mr;
    public int color_code = 0;

    public LayerMask effectLayer;

    Vector3 _OriginScale;

    public Vector3 hitNormal;

    int scaleCount = 0;

    //public List<CubeLine> lines = new List<CubeLine>();

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        _OriginScale = transform.localScale;
        mr.material = new Material(Shader.Find("HDRP/Lit"));
        mr.material.SetColor("_BaseColor", Color.black);
        setcolor(null, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<dye>().onDyeEvoke += setcolor;
        //ColorManager.instance.onColorEvoke += effect;
    }

    void Update()
    {
        if(scaling = false && keepscalingRed)
        {
            update_red(CollisionMemory);
        }
    }

    public void getColor(int code)
    {
        setcolor(null, code);
    }

    void setcolor(Collision other, int code)
    {
        //if(code == color_code)
        //    return;
        //mr.material = ColorManager.LoadMaterial(code, false);
        mr.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(code));
        color_code = code;
        switch (code)
        {
            case 0: break;
            case 1: break;
            case 2: update_red(other);break;
            case 3: update_yellow();break;
            default: break;
        }
        
        /*if(code == 2)
        {
            //transform.position = transform.position + Vector3.up * transform.localScale.y /2;
            
        }

        if(code == 3)
        {
            transform.position = transform.position + Vector3.up * transform.localScale.y *0.1f;
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1,1.2f,1));
        }*/
            
    }


    /*void checkConnectionToBlue()
    {
        foreach(var tmp in lines)
        {
            DyableObject other = tmp.FindAnother(this);
            if(other.color_code == 0)
            {
                tmp.setColor(1);
                other.UpdateColorByTime(1, 0.3f);
            }
            else
            {
                tmp.setColor(1);
            }
        }
    }*/

    //======================================== different update methods ========================================

    bool keepscalingRed = false;
    Collision CollisionMemory = null;


    void update_red(Collision other)
    {
        if(hitNormal.y != 0)
            return;
        if(scaling)
        {
            keepscalingRed = true;
            CollisionMemory = other;
            return;
        }
        StartCoroutine(setScaleTowardsIE(2f, hitNormal, 0.6f));
    }

    void update_yellow()
    {
        if(scaling)
            return;
        if(MathTool.LargerThanBox(transform.localScale, MathTool.BoxSize(_OriginScale)))
            return;
        StartCoroutine(setScaleIE(new Vector3(0f,0.5f,0f), 0.3f));
    }


    void effect(int id)
    {
        if(id != color_code)
            return;
        if(color_code == 0)
            return;
        Collider[] tmps = Physics.OverlapSphere(transform.position, maxdistance, effectLayer);
        foreach(var a in tmps)
        {
            if(a.GetComponent<DyableObject>()!=null && a.gameObject!=gameObject)
            {
                if(a.GetComponent<DyableObject>().color_code != color_code)
                    a.GetComponent<DyableObject>().UpdateColorByPos(color_code, transform.position);
            }
        }
    }

    //======================================== effect other cubes ========================================

    float maxdistance = 3.5f;
    float waitTime = .5f;

    public void UpdateColorByPos(int code, Vector3 pos)
    {
        float distance = (transform.position - pos).magnitude;
        StartCoroutine(setColorIE(code, Mathf.Max(distance/maxdistance, 0.8f) * waitTime));
    }

    public void UpdateColorByTime(int code, float time)
    {
        StartCoroutine(setColorIE(code, time));
    }

    IEnumerator setColorIE(int code, float delay)
    {
        yield return new WaitForSeconds(delay);
        //setcolor(code);
        GetComponent<dye>().EvokeDye(code);
    }

    //======================================== scale ========================================

    bool scaling = false;

    IEnumerator setScaleIE(Vector3 multiply, float time)
    {
        Vector3 originScale = transform.localScale;
        multiply = Vector3.Scale(originScale, multiply);
        multiply = MathTool.Clamp3(multiply, new Vector3(2f,2f,2f));
        Vector3 targetScale = originScale + multiply;
        scaling = true;
        float startTime = Time.time;
        float tmp;
        Vector3 originPos = transform.position;
        while((tmp = Time.time - startTime) < time)
        {
            if(tmp < time * 0.8f)
            {
                transform.localScale = Vector3.Lerp(originScale, targetScale * 1.3f, tmp/time);
            }
            else
            {
                transform.localScale = Vector3.Lerp(targetScale * 1.3f, targetScale, (tmp - time * 0.8f)/(0.2f * time));
            }
            
            if(targetScale.y != originScale.y)
            {
                transform.position = new Vector3(originPos.x, (transform.localScale.y - originScale.y)/2 + originPos.y,originPos.z);
            }
            checkSpace();
            yield return new WaitForFixedUpdate();
        }
        scaling = false;
    }

    IEnumerator setScaleTowardsIE(float distance, Vector3 direction,float time)
    {
        scaleCount ++;
        //if(scaleCount > ResourceManager.RManager.MaxRedScale)
        //    yield break;
        Vector3 originScale = transform.localScale;
        scaling = true;
        float startTime = Time.time;
        float tmp;
        Vector3 originPos = transform.position;
        Vector3 targetScale = originScale + distance * MathTool.AbsVector(direction);
        while((tmp = Time.time - startTime) < time)
        {
            transform.localScale = Vector3.Lerp(originScale, targetScale, tmp/time);
            transform.position = originPos + Vector3.Scale((transform.localScale - originScale), direction)/2;
            checkSpace();
            yield return new WaitForFixedUpdate();
        }
        scaling = false;

    }

    /*void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.GetComponent<DyableObject>() == null && other.gameObject.tag != "bullet")
        {
            Debug.Log(other.gameObject.name + "hit! stop scaleing");
            StopAllCoroutines();
        }
            
    }*/
    void checkSpace()
    {
        Collider[] tmps = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity);
        foreach(var a in tmps)
        {
            if(a.gameObject.GetComponent<DyableObject>() == null && a.gameObject.tag != "bullet" && a.gameObject.tag != "Base")
            {
                Debug.Log(a.gameObject.name + "hit! stop scaleing");
                StopAllCoroutines();
            }
        }
    }

    /*public void setConnection(DyableObject other)
    {
        if(CheckConnected(other))
            return;
        GameObject tmp = new GameObject();
        tmp.transform.parent = FindObjectOfType<CubeLineManager>().transform;
        CubeLine tmpLine = tmp.AddComponent<CubeLine>();
        tmpLine.setConnection(this, other);
        lines.Add(tmpLine);
        other.lines.Add(tmpLine);

        if(color_code == 1)
            checkConnectionToBlue();
        else if(other.color_code == 1)
            checkConnectionToBlue();


    }

    bool CheckConnected(DyableObject other)
    {
        if(lines.Count == 0)
            return false;
        foreach(var tmp in lines)
        {
            if(tmp.FindAnother(this)==other)
            {
                return true;
            }
        }
        return false;
    }*/

    public bool blueEvoked = false;

    public void EvokeBlue()
    {
        if(blueEvoked)
            return;
        StartCoroutine(EvokeBlueIE());
        Collider[] tmps = Physics.OverlapBox(transform.position, transform.localScale/2 + new Vector3(maxdistance, maxdistance, maxdistance), Quaternion.identity, effectLayer);
        /* DyableObject k = null;
        float nearest = 100f; */
        foreach(var a in tmps)
        {
            if(a.GetComponent<DyableObject>()!=null && a.gameObject!=gameObject)
            {
                
                if(a.GetComponent<DyableObject>().color_code == 1 && !a.GetComponent<DyableObject>().blueEvoked)
                {
                    /*float distance = (transform.position - a.transform.position).magnitude;
                    if(distance<nearest)
                    {
                        nearest = distance;
                        k = a.GetComponent<DyableObject>();
                    }*/

                    float distance = (transform.position - a.transform.position).magnitude;
                    distance = Mathf.Max(distance/maxdistance, 0.8f) * waitTime;
                    float delay = transform.localScale.y * 0.1f;
                    StartCoroutine(DelayEvoke(delay,  a.GetComponent<DyableObject>()));
                }
            }
            if(a.GetComponent<Door>()!=null)
            {
                //Debug.Log("detect door");
                a.GetComponent<Door>().setopen();
            }
        }
        /*if( k != null )
        {
            float delay = transform.localScale.y * 0.1f;
            StartCoroutine(DelayEvoke(delay,  k.GetComponent<DyableObject>()));
        }*/

    }

    IEnumerator EvokeBlueIE()
    {
        Color c = ColorManager.PalatteToColor(1);
        mr.material.SetColor("_EmissiveColor", c * 4f);
        blueEvoked = true;
        yield return new WaitForSeconds(1f);
        mr.material.SetColor("_EmissiveColor", c);
        yield return new WaitForSeconds(5f);
        blueEvoked = false;
    }

    IEnumerator DelayEvoke(float time, DyableObject other)
    {
        yield return new WaitForSeconds(time);
        other.EvokeBlue();
    }

    public void BulletDye(Collision other, int code)
    {
        setcolor(other, code);
    }



}
