using UnityEngine;
/// <summary>This is used in Global for the credit roll, not to be used elsewhere.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="GlobalVariables"/> 
/// </summary>
public class Credits : MonoBehaviour
{
    public float speed;
    public float anchorTop = 10f;
    private RectTransform credits;

    public void Scroll()
    {
        credits = GetComponent<RectTransform>();
        credits.anchoredPosition = Vector2.zero;
        credits.anchorMax = new Vector2(0.5f,0f);
        credits.anchorMin = credits.anchorMax;
    }

    // Update is called once per frame
    void Update()
    {
        credits.anchorMax = new Vector2(0.5f,Mathf.Lerp(credits.anchorMax.y,anchorTop,speed));
        credits.anchorMin = credits.anchorMax;
        if(credits.anchorMax.y >= (anchorTop/2))
            credits.anchorMax = new Vector2(0.5f,0f);
    }
}
