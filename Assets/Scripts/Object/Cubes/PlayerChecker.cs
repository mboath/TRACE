using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public Vector3 BoxPoint;
    public Vector3 BoxSize;

    public void checkTap()
    {
        Collider[] tapCheck = Physics.OverlapBox(transform.position + BoxPoint, BoxSize, Quaternion.identity,  LayerMask.GetMask("player"));
        if(tapCheck.Length > 0)
            isPlayer = true;
        else
            isPlayer = false;
    }

    void Start()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        BoxPoint = box.center + (box.size.y/2 + 0.3f) * Vector3.up;
        BoxSize = new Vector3(box.size.x/2, .3f, box.size.z/2);
    }

    void Update()
    {
        checkTap();
    }

    public bool isPlayer = false;

}
