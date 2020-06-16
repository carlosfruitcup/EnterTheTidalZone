using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour
{
    public DialogHandler dialog;
    public float fadeTime = 1f;
    public Image fadeImage;
    public Animator handAnim;
    public Animator mapAnim;
    public IEnumerator FadeScreen()
    {
        while (fadeImage.color.a < 0.75f)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + (Time.deltaTime / fadeTime));
            yield return null;
        }
    }
    public IEnumerator WaitAndTalk()
    {
        yield return new WaitForSeconds(3.2f);
        dialog.TriggerDialogue();
    }
    public void ShowMapFirstTime()
    {
        transform.Find("Close").gameObject.SetActive(false);
        StartCoroutine(WaitAndTalk());
        ShowMap();
    }
    public void ShowMap()
    {
        transform.Find("Close").gameObject.SetActive(true);
        StartCoroutine(FadeScreen());
        handAnim.Play("loop");
        mapAnim.Play("loop");
    }
}
