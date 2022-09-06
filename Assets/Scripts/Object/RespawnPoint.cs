using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public static Transform RESPAWN;


    public float energySecond = 10f;

    Vector3 boxPoint = new Vector3(0f, 0.04f, 0f);
    Vector3 boxSize = new Vector3(0.4f, 0.2f, 0.4f);

    GameObject player;

    bool activated = false;


    IEnumerator activateIE()
    {
        activated = true;
        player.GetComponent<FPController>().enabled = false;
        RESPAWN = this.transform;
        yield return new WaitForSeconds(1f);
        player.GetComponent<FPController>().enabled = true;
        player.GetComponent<FPController>().recorderModeActivate(true);

    }

    void Awake()
    {
        player = FindObjectOfType<FPController>().gameObject;
    }

    public void checkSpawn()
    {
        Collider[] spawnCheck = Physics.OverlapBox(transform.position + boxPoint, boxSize, Quaternion.identity, LayerMask.GetMask("player"));
        //  Debug.Log(spawnCheck.Length);
        if(spawnCheck.Length > 0)
        {
            StartCoroutine(activateIE());
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!activated)
            checkSpawn();
    }
}
