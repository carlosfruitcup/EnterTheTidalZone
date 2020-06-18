using UnityEngine;
using UnityEditor;
/// <summary>This is used to instantiate a material so it can be modified without impacting other objects using the same material.
/// <para>This is an EditorWindow class. </para>
/// </summary>
public class InstantiateMaterial : EditorWindow
{
    [MenuItem("Tidal/Instantiate Selected Material")]
    static void InstantiateMat()
    {
        Instantiate(Selection.activeGameObject.GetComponent<Renderer>().material);
    }
}
