using UnityEngine;
/// <summary>This is used in Global for the credit roll, not to be used elsewhere.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="GlobalVariables"/> 
/// </summary>
public class Credits : MonoBehaviour
{
    public float originalY = -2391f;
    public float speed;
    private RectTransform credits;
    private Vector2 destination;

    public void Scroll()
    {
        credits = GetComponent<RectTransform>();
        credits.anchoredPosition = new Vector2(credits.anchoredPosition.x,originalY);
        destination = new Vector2(credits.anchoredPosition.x,0f);
    }

    // Update is called once per frame
    void Update()
    {
        credits.anchoredPosition = Vector2.Lerp(credits.anchoredPosition,destination,speed);
    }
}
