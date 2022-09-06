using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    static int colorNum=4;
    public static int NUM{get{return colorNum;}}

    public static Dictionary<int,string> ColorPath
        = new Dictionary<int,string>{
            {0,"emission/white"},
            {1,"emission/blue"},
            {2,"emission/red"},
            {3,"emission/yellow"},
            {4,"emission/green"}
        };
    
    public static Dictionary<int,string> UnlitColorPath
        = new Dictionary<int,string>{
            {0,"emission/grey"},
            {1,"emission/unlit/blue"},
            {2,"emission/unlit/red"},
            {4,"emission/unlit/green"},
            {3,"emission/unlit/yellow"}
        };
    
    static float[,] colorPalatte = new float[,]{
        {191f/255f, 191f/255f, 191f/255f, 1.5f},
        {0f, 47/255f, 227f/255f, 3.5f},
        {203f/255f, 0f/255f, 0f/255f, 4f},
        {238f/255f, 58f/255f, 14f/255f, 3.5f},
        {33f/255f, 230f/255f, 5f/255f, 3.2f}
    };

    public static Color PalatteToColor(int code)
    {
        Color tmp = Color.white;
        tmp.r = colorPalatte[code, 0];
        tmp.g = colorPalatte[code, 1];
        tmp.b = colorPalatte[code, 2];
        tmp = tmp * Mathf.Pow(2, colorPalatte[code, 3]);
        tmp.a = 1f;
        return tmp;
    }
    
    public Dictionary<int, bool> colorState;

    

    public static Material LoadMaterial(int code, bool lit)
    {
        return Resources.Load<Material>(lit? ColorPath[code] : UnlitColorPath[code]);
    }

    void initiateColor()
    {
        colorState = new Dictionary<int, bool>();
        for(int i=0;i<=4;i++)
        {
            Color tmp = PalatteToColor(i);
            //Debug.Log(i + " " + tmp);
            LoadMaterial(i, true).SetColor("_EmissiveColor", tmp);
            LoadMaterial(i, false).SetColor("_EmissiveColor", tmp);
            colorState.Add(i, false);
        }
    }


    public event Action<int> onColorEvoke;
    public event Action<int> onColorDisable;

    public void colorEvoke(int id)
    {
        if(!colorState[id])
        {
            LoadMaterial(id, true).SetColor("_EmissiveColor", (PalatteToColor(id))*3f);
            LoadMaterial(id, false).SetColor("_EmissiveColor", (PalatteToColor(id))*1.1f);
            colorState[id] = true;

        }
        if(onColorEvoke != null)
        {
            onColorEvoke(id);
        }
    }

    public void colorDisable(int id)
    {
        if(!colorState[id])
            return;
        colorState[id] = false;
        LoadMaterial(id, true).SetColor("_EmissiveColor", PalatteToColor(id));
        LoadMaterial(id, false).SetColor("_EmissiveColor", PalatteToColor(id));
        if(onColorDisable != null)
        {
            onColorDisable(id);
        }
    }

    void Awake()
    {
        if(instance!= null)
            Destroy(gameObject);
        instance = this;
        initiateColor();
        DontDestroyOnLoad(gameObject);
    }


}
