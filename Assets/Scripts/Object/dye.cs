using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dye : MonoBehaviour
{
    public event Action<int> onDyeEvoke;
    public event Action disableEvoke;

    public void EvokeDye(int code)
    {
        onDyeEvoke(code);
    }

    public void returnColor()
    {
        disableEvoke();
    }
}
