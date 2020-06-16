using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFlasher : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float t = 1f;
    void Awake()
    {
        if(text == null) text = transform.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        StartCoroutine("FadeInOut");
    }
    public IEnumerator FadeInOut()
    {
        while(true) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0.5f);
            while (text.color.a < 1.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
                yield return null;
            }
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            while (text.color.a > 0.5f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
                yield return null;
            }
            yield return null;
        }
    }
}
