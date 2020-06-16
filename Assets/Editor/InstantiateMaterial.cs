using UnityEngine;
using UnityEditor;
public class InstantiateMaterial : EditorWindow
{
    [MenuItem("Yummer/Instantiate Selected Material")]
    static void InstantiateMat()
    {
        Instantiate(Selection.activeGameObject.GetComponent<Renderer>().material);
    }
}
