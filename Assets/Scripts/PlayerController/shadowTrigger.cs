using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowTrigger : MonoBehaviour
{
    public bool startPoint;
    public DollControl parentDoll;

    /* private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Bullet>()!=null)
        {
            ResourceManager.RManager.setPublicSpark(other.transform.position, Camera.main.transform.forward * -1, 0f, other.gameObject.GetComponent<Bullet>().color_code);
            if(other.gameObject.GetComponent<Bullet>().color_code == 0)
            {
                Destroy(parentDoll.gameObject);
            }
            else
            {
                parentDoll.setColor(other.gameObject.GetComponent<Bullet>().color_code);
                parentDoll.reset(startPoint);
            }
            other.gameObject.GetComponent<Bullet>().destoryBullet();

        }
    } */

    public void setDoll()
    {
        parentDoll.reset(startPoint);
    }

    void OnDestroy()
    {
        //Vector3 gunPos = FindObjectOfType<GunControll>().shootPos.position;
        //Bullet temp = (Instantiate(Resources.Load("bullet"), transform.position, Quaternion.identity) as GameObject).GetComponent<Bullet>();
        //temp.speed = 70f;
        //float distance = (gunPos - transform.position).magnitude;
        //temp.setBullet(distance, gunPos, 1);
    }
}
