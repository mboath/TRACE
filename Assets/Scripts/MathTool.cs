using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTool
{

    public static Vector3 V2To3(Vector2 input)
    {
        return new Vector3(input.x, input.y, 0);
    }

    public static bool interval(float a, float l, float r)
    {
        return (l <= a && a <= r);
    }

    public static Vector3 angelDistance(Quaternion a, Quaternion b)
    {  
        Vector3 tmp = a.eulerAngles - b.eulerAngles;
        for (int i = 0; i < 3; i++)
        {
            tmp[i] %= 360;
            if(tmp[i]<0)
                tmp[i] = (-tmp[i])<(tmp[i]+360)?tmp[i]:(tmp[i]+360);
            if(tmp[i]>0)
                tmp[i] = (tmp[i])<(360-tmp[i])?tmp[i]:(tmp[i]-360);
        }
        return tmp;
    }

    public static float BoolToOne(bool a)
    {
        return a?1f:-1f;
    }

    public static float planeDistance(Vector3 a, Vector3 b)
    {
        a.y=0;
        b.y=0;
        return (a-b).magnitude;
    }

    public static void setLitColor(Material a, Color c)
    {
        a.SetColor("_BaseColor", c);
    }

    public static void setLitEmission(Material a, Color c)
    {
        a.SetColor("_EmissiveColor", c);
    }

    public static bool LargerThanBox(Vector3 a, Vector3 b)
    {
        Debug.Log(a + " " + b);
        return (a.x > b.x || a.y > b.y || a.z > b.z);
    }

    public static Vector3 Clamp3(Vector3 a, Vector3 b)
    {
        a.x = Mathf.Clamp(a.x, 0, b.x);
        a.y = Mathf.Clamp(a.y, 0, b.y);
        a.z = Mathf.Clamp(a.z, 0, b.z);
        return a;
    }

    public static Vector3 BoxSize(Vector3 scale)
    {
        /*float v = scale.x * scale.y * scale.z;
        if(v < 8f)
            return scale * 2f;
        else
            return scale * (40f)/(v-(8-20));   */
        return scale * 2f;
    }

    public static Vector3 AbsVector(Vector3 a)
    {
        a.x = Mathf.Abs(a.x);
        a.y = Mathf.Abs(a.y);
        a.z = Mathf.Abs(a.z);
        return a;
    }
}
