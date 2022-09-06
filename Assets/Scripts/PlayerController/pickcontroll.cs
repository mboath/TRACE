using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pickcontroll : MonoBehaviour
{
    public GameObject[] list;
    GameObject currentobject = null;
    void Start()
    {
        
    }

    void Update()
    {
        if(currentobject != null)
        {
            currentobject.transform.position = transform.position;
            currentobject.transform.LookAt(transform.parent);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentobject!= null)
                currentobject = null;
            else
            {
                foreach(var a in list)
                {
                    if((transform.position - a.transform.position).magnitude < 3)
                    {
                        currentobject = a;
                        return;
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
