using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        Debug.Log("Loading scene "+scene+"...");
        GlobalVariables.global.Fade(scene);
    }
}
