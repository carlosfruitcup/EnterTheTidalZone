using UnityEngine;
/// <summary>This is used to load a scene from anywhere as a component, typically used in UnityEvents.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="GlobalVariables"/> 
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        Debug.Log("Loading scene "+scene+"...");
        GlobalVariables.global.Fade(scene);
    }
}
