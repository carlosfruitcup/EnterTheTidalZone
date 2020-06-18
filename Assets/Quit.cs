using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>Used to quit the game as a component for UnityEvents.
/// <para>This is a MonoBehaviour class. </para>
/// </summary>
public class Quit : MonoBehaviour
{
    public void ExitApp(){
		#if UNITY_EDITOR
         // Application.Quit() is for built app
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
	}
}
