using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControll : MonoBehaviour
{
    public float rotatespeed = 0.5f;
    public Quaternion ROTATION{get{return transform.rotation;}}
    public void setDirection(Vector3 dir)
    {
        Quaternion direction = Quaternion.LookRotation(dir);
        float deltaY = MathTool.angelDistance(direction, transform.rotation).y;
        //Debug.Log(deltaY);
        if(Mathf.Abs(deltaY) < rotatespeed * Time.deltaTime)
        {
            transform.Rotate(deltaY * Vector3.up);
        }
        else
        {
            transform.Rotate(Mathf.Sign(deltaY) * Vector3.up * Time.deltaTime * rotatespeed);
        }
    }

    public LayerMask groundLayer;

    /* public void groundNormal()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, - Vector3.up, out hit, 1f, groundLayer);
        if(hit.distance == 0)
            transform.localr
        fpCamera.localPosition = new Vector3(0, fpCamera.localPosition.y, distance);
    } */
    // Start is called before the first frame update
}
