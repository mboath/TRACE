using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    float speed = 50f;
    float distance;
    Vector3 target;
    List<GameObject> dolls;
    Vector3 t;
    public MeshRenderer mr;
    public int color_code;
    bool selfDestorying = false;
    //public TrailRenderer trail;

    Vector3 hitnormal;

    float startTime;
    Vector3 originScale;

    public void setBullet(float distance, Vector3 target, int colorCode, Vector3 normal)
    {
        hitnormal = normal;
        color_code = colorCode;
        resetMat();
        t=target;
        this.distance = distance;
        this.target = target;
        Vector3 heading = target - transform.position;
        heading = heading/(heading.magnitude);
        transform.rotation = Quaternion.LookRotation(heading);
        startTime = Time.time;
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        if(distance < 100){
            ResourceManager.RManager.setPublicSpark(target, normal, distance/speed, colorCode);
        }
        
    }

    float highestTimer = 2f;

    void Update()
    {
        if(Time.time - startTime > highestTimer && !selfDestorying)
            StartCoroutine(selfDestory());
        if(Time.time - startTime > .05f && !mr.enabled)
            mr.enabled = true;
    }


    IEnumerator selfDestory()
    {
        
        selfDestorying = true;
        transform.GetChild(0).gameObject.SetActive(false);
        
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }

    public void destoryBullet()
    {
        StartCoroutine(selfDestory());
    }


    void OnCollisionEnter(Collision other)
    {
        if(selfDestorying)
            return;
        //Debug.Log(other.gameObject.name + " hit bullet");
        if(other.gameObject.GetComponent<DyableObject>()!=null)
        {
            other.gameObject.GetComponent<DyableObject>().hitNormal = hitnormal;
            other.gameObject.GetComponent<DyableObject>().BulletDye(other, color_code);
        }
        else if(other.gameObject.GetComponent<dye>()!=null)
        {
            other.gameObject.GetComponent<dye>().EvokeDye(color_code);
        }
        StartCoroutine(selfDestory());
    }


    void resetMat()
    {
        Material[] mat = mr.materials;
        mat[0] = ColorManager.LoadMaterial(color_code, true);
        mr.materials = mat;
        //trail.material = mat[0];
    }


}
