using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLine : MonoBehaviour
{
    DyableObject cube1;
    DyableObject cube2;
    LineRenderer line;
    public int color_code;

    void createLine()
    {
        if(GetComponent<LineRenderer>()!=null)
            return;
        gameObject.AddComponent<LineRenderer>();
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.material = new Material(Shader.Find("Shader Graphs/line"));
        MathTool.setLitEmission(line.material, ColorManager.PalatteToColor(0));
        line.material.SetFloat("_Trnas", .4f);
        line.startWidth = .04f;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void setConnection(DyableObject a, DyableObject b)
    {
        cube1 = a;
        cube2 = b;
        createLine();
        line.positionCount = 2;
        line.SetPosition(0, a.transform.position);
        line.SetPosition(1, b.transform.position);
    }

    public void updateConnection(DyableObject tmp)
    {
        if(cube1 == tmp)
            line.SetPosition(0, tmp.transform.position);
        else if(cube2 == tmp)
            line.SetPosition(1, tmp.transform.position);
        else
            Debug.Log("cube line update error");
    }

    public void setColor(int code)
    {
        if(code == color_code)
            return;
        MathTool.setLitEmission(line.material, ColorManager.PalatteToColor(code));
        color_code = code;
    }

    public DyableObject FindAnother(DyableObject tmp)
    {
        if(cube1 == tmp)
            return cube2;
        else if(cube2 == tmp)
            return cube1;
        else{
            Debug.Log("cube line update error");
            return null;
        }
    }
}
