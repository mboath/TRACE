using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DyeRecorder : MonoBehaviour
{
    //List<cubeTape> tap = new List<cubeTape>();
    public List<DyableObject> tap= new List<DyableObject>();
    //List<LineRenderer> lines = new List<LineRenderer>();
    Material lineMat;
    LineRenderer line;
    int color_code = 0;

    void Awake()
    {
        createLine();
    }

    public void addCube(DyableObject next)
    {
        if(line.positionCount>0)
        {
            if(tap[tap.Count-1] == next)
                return;
        }
        Debug.Log("add cube " + next.name);
        tap.Add(next);
        line.positionCount ++;
        line.SetPosition(line.positionCount-1, next.transform.position);
        next.gameObject.GetComponent<dye>().onDyeEvoke+=setColor;
    }

    void setColor(int code)
    {
        if(code == color_code)
            return;
        if(code == 0)
            Destroy(gameObject);
        MathTool.setLitEmission(line.material, ColorManager.PalatteToColor(code));

    }

    void createLine()
    {
        if(GetComponent<LineRenderer>()!=null)
            return;
        gameObject.AddComponent<LineRenderer>();
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.material = new Material(Shader.Find("Shader Graphs/line"));
        MathTool.setLitEmission(line.material, ColorManager.PalatteToColor(0));
        line.startWidth = .01f;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

}
