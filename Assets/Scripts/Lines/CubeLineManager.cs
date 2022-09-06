using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLineManager : MonoBehaviour
{

    public static LineRenderer NewLine(Transform a, Transform b, int color_code)
    {
        GameObject tmp = new GameObject();
        tmp.transform.parent = a;
        tmp.AddComponent<LineRenderer>();
        Material lineMaterial = new Material(Shader.Find("HDRP/Lit"));
        lineMaterial.SetColor("_BaseColor", Color.black);
        lineMaterial.SetColor("_EmissiveColor", ColorManager.PalatteToColor(color_code) * 8f);
        LineRenderer lineRenderer = tmp.GetComponent<LineRenderer>();
        lineRenderer.sharedMaterial = lineMaterial;
        lineRenderer.startWidth = .04f;;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, a.position);
        lineRenderer.SetPosition(1, b.position);
        lineRenderer.generateLightingData = true;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return lineRenderer;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
