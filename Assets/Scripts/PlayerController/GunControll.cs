using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControll : MonoBehaviour
{
    //float slootInterval = .2f;
    float timing;
    Animator gunAnimator;
    public Camera FPSCam;
    public float range = 100f;
    public LayerMask shootableLayer;
    public Transform shootPos;
    public int gunColor = 0;
    public MeshRenderer mr;

    SparkControll gunSpark;

    public static GunControll instance;

    void Awake()
    {
        if(instance!=null)
            Debug.Log("gun instance error");
        instance = this;
        gunAnimator = GetComponent<Animator>();
        resetMat();
        
    }

    void Start()
    {
        gunSpark = (Instantiate(ResourceManager.RManager.SpartVFX, shootPos.position, Quaternion.LookRotation(transform.forward))).GetComponent<SparkControll>();
        gunSpark.transform.parent = shootPos;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.T))
        {
            if(tryShoot())
            {
                shootOnce();
            }   
        }

        /*if(Input.GetKeyDown(KeyCode.R))
        {
            resetAllDoll();
        }*/

        if(Input.GetMouseButtonDown(1))
        {
            ChangeMaterial();
        }
    }
    void resetAllDoll()
    {
        DollControl[] dolls = FindObjectsOfType<DollControl>();
        foreach(var d in dolls)
        {
            Destroy(d.gameObject);
        }

    }

    bool tryShoot() 
    {
        return true;
    }

    void shootOnce()
    {
        gunAnimator.SetTrigger("shoot");
        RaycastHit hit;
        Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range, shootableLayer);
        shootBullet(hit);

    }

    void shootBullet(RaycastHit hit)
    {
        Vector3 target;
        float distance;
        if(hit.distance != 0f)
        {
            target = hit.point;
            distance = hit.distance;
        }
        else
        {
            distance = 100f;
            target = FPSCam.transform.position + distance * FPSCam.transform.forward;
        }
        Bullet temp = (Instantiate(Resources.Load("bullet"), shootPos.position, Quaternion.identity) as GameObject).GetComponent<Bullet>();
        temp.setBullet(distance, target, gunColor, hit.normal);
        gunSpark.SetColor(gunColor);
        gunSpark.spark.Play();
    }

    void ChangeMaterial()
    {
        gunColor++;
        if(gunColor > ColorManager.NUM)
            gunColor = 0;
        resetMat();
    }

    void resetMat()
    {
        Material[] mat = mr.materials;
        mat[0] = ColorManager.LoadMaterial(gunColor, false);
        mr.materials = mat;
    }
}
