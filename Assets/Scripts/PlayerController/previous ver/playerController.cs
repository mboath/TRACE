using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveForce;
    public float rotateSpeed;
    public string inputList = "";

    Vector3 POS
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void movecontroller()
    {
        Vector3 dir = Vector3.zero;
        if (inputList.Contains("l"))
            dir += new Vector3(1,0,0);
        if (inputList.Contains("d"))
            dir += new Vector3(0,0,1);
        if (inputList.Contains("r"))
            dir += new Vector3(-1,0,0);
        if (inputList.Contains("u"))
            dir += new Vector3(0,0,-1);
        dir.Normalize();
        rotateControl(dir);
        GetComponent<Rigidbody>().AddForce(dir * moveForce);
    }

    void rotateControl(Vector3 dir)
    {
        if(dir.magnitude == 0)
            return;
        Vector3 rotateEular = Quaternion.LookRotation(dir).eulerAngles;
        Vector3 tmp = rotateEular-transform.eulerAngles;
        if(tmp.y > 180)
            tmp = new Vector3(tmp.x, tmp.y-360, tmp.z);
        if(tmp.y < -180)
            tmp = new Vector3(tmp.x, tmp.y+360, tmp.z);
        if(tmp.magnitude < rotateSpeed * Time.deltaTime)
        {
            transform.eulerAngles = rotateEular;
        }
        else
        {
            tmp.Normalize();
            tmp *= Time.deltaTime * rotateSpeed;
            transform.eulerAngles = transform.eulerAngles + tmp;
        }/**/
        //transform.LookAt(direct.transform);
        //transform.rotation = Quaternion.LookRotation(dir);

    }


    void Start()
    {
    }

    void FixedUpdate()
    {
        movecontroller();
    }
}
