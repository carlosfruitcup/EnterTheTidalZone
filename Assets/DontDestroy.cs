using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy global;
    public bool doDestroy = false;
    void Awake()
    {
        if (global == null) global = this;
        else if(doDestroy)
        {
            Destroy(global);
            global = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
