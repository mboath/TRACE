using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainCanvas : MonoBehaviour
{
    GameObject current_curtain;
    GameObject player;
    public void openCurtain()
    {
        //current_curtain.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        current_curtain.GetComponent<curtain>().setCurtain(true);

    }

    public void closeCurtain()
    {
        current_curtain.GetComponent<curtain>().setCurtain(false);
    }
    void Awake()
    {
        player = FindObjectOfType<InputHandler>().gameObject;
        current_curtain = transform.Find("curtain").gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            openCurtain();
        if(Input.GetKeyDown(KeyCode.Y))
            closeCurtain();
    }
}
