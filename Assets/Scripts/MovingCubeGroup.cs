using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingCubeGroup : MonoBehaviour
{
    public float distance;
    // Start is called before the first frame update

    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);
    }

    public void ActivateGroup()
    {
        transform.DOMoveY(transform.position.y + distance, 1f);
        GetComponent<MovingCubeGroup>().enabled = false;
    }

    
}
