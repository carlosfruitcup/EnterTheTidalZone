using System.Collections.Generic;
using UnityEngine;
/// <summary>This is used in the root of a scene, it keeps its components and children persistant.
/// <para>This is a MonoBehaviour class. </para>
/// </summary>
public class DontDestroy : MonoBehaviour
{
    public static DontDestroy global;
    public bool doDestroy = false;
    void Awake()
    {
        if (global == null) global = this;
        else if(doDestroy)
        {
            Destroy(global.gameObject);
            global = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
