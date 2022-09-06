using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGroupTrigger : MonoBehaviour
{
    Transform player;
    public MovingCubeGroup a;

    void Awake()
    {
        player = FindObjectOfType<FPController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < 3f)
        {
            a.ActivateGroup();
            Destroy(gameObject);
        }

    }
}
