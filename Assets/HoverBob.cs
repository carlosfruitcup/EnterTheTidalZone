using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>This is used in Global, it bounces up and down the transform's scale upon hovering.
/// <para>This is a class based on MonoBehaviour, IPointerEnterHandler and IPointerExitHandler. </para>
/// <seealso cref="GlobalVariables"/>
/// </summary>
public class HoverBob: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform nextBg;
    private bool started = false;
    private bool started2 = false;
    public float duration = 1f;
    public float scale = 1.5f;
    private float startTime;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
        startTime = Time.time;
        started2 = false;
        started = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited");
        startTime = Time.time;
        started2 = true;
        started = false;
    }
    void Update()
    {
        if(started)
		{
			float t = (Time.time - startTime) / duration;
			nextBg.localScale = new Vector3(Mathf.SmoothStep(1f,scale,t),Mathf.SmoothStep(1f,scale,t),1f);
			if(nextBg.localScale.x == scale)
			{
				started = false;
			}
		}
		else if(started2)
		{
			float t = (Time.time - startTime) / duration;
			nextBg.localScale = new Vector3(Mathf.SmoothStep(scale,1f,t),Mathf.SmoothStep(scale,1f,t),1f);
			if(nextBg.localScale.x == 1f)
			{
				started2 = false;
			}
		}
    }
}