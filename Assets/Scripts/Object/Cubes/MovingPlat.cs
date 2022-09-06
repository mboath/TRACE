using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour
{
    public int color_code;

    public Vector3 targetVetor;
    public float moveSpeed;
    public float returnSpeed;
    Vector3 originPos;

    Vector3 moveUnit;

    public bool moving;

    void Awake()
    {
        originPos = transform.position;
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("HDRP/Lit"));
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black);
        GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code));
    }
    // Start is called before the first frame update
    void Start()
    {
        moveUnit = targetVetor.normalized * moveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //moving = GetComponent<PlayerChecker>().isPlayer;
        float distance = (transform.position - originPos).magnitude;

        if(moving)
        {
            //Debug.Log(distance + " " + targetVetor.magnitude);
            if(distance < targetVetor.magnitude)
            {
                //FindObjectOfType<FPController>().transform.parent = transform;
                transform.position += moveUnit;
                if(GetComponent<PlayerChecker>().isPlayer)
                    FPController.instance.SetMove(moveUnit);
            } 
        }
        else
        {
            if(distance > returnSpeed * Time.deltaTime)
            {
                //GetComponent<FPController>().transform.parent = null;
                transform.position -= moveUnit;
                if(GetComponent<PlayerChecker>().isPlayer)
                    FPController.instance.SetMove(-moveUnit);
            }
        }
    }

    public void SetActiveColor(bool a)
    {
        GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code) * (a?2f:1f));
        moving = a;
    }
}
