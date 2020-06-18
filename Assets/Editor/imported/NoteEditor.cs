using UnityEditor;
using UnityEngine;
/// <summary>This is a robust note text UI for the Note class.
/// <para>This is an Editor class. </para>
/// <seealso cref="Note"/> 
/// </summary>
[CustomEditor(typeof(Note))]
public class NoteEditor : Editor
{
    private Note note;
 
    private Vector2 scrollPos;
 
    private void Init()
    {
        note = base.target as Note;        
    }
 
    public override void OnInspectorGUI()
    {
        Init();
        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(100.0F));
        GUIStyle myStyle = new GUIStyle();
        myStyle.wordWrap = true;
        myStyle.stretchWidth = false;
        myStyle.normal.textColor = Color.gray;
        note.text = GUILayout.TextArea(note.text, myStyle, GUILayout.Width(200.0F), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.EndScrollView();
        Repaint();       
    }          
}