using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRender : MonoBehaviour
{
    public SkinnedMeshRenderer smr;

    public void setMat(Material mat)
    {
        for(int i=0;i<smr.materials.Length;i++)
        {
            smr.sharedMaterials[i] = mat;
        }
    }
}
