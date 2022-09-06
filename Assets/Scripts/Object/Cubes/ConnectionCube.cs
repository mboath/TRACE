using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCube : MonoBehaviour
{
    public Transform twin;
    static string laserName = "LaserPoint";

    LineRenderer Line;

    public tap RootTap;

    public MovingPlat plat;

    public int color_code;

    void SetConnectionActive()
    {
        Line.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code) * 16f);
        plat.SetActiveColor(true);
    }

    void SetConnectionInactive()
    {
        Line.material.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code) * 8f);
        plat.SetActiveColor(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Line = CubeLineManager.NewLine(transform.Find(laserName), twin.Find(laserName), color_code);
        RootTap.onTapEvoke += SetConnectionActive;
        RootTap.onTapDisable += SetConnectionInactive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
