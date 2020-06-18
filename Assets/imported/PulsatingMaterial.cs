using UnityEngine;
/// <summary>Imported from an earlier project. This pulsates a material's color to make it less boring.
/// <para>This is a MonoBehaviour class. </para>
/// </summary>
public class PulsatingMaterial : MonoBehaviour
{
    public float FadeDuration = 2.5f;
    public Color ColorChange = Color.white;
    private Color startColor;
    private Color endColor;
    private float lastColorChangeTime;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        startColor = material.color;
        endColor = ColorChange;
    }

    void Update()
    {
        var ratio = (Time.time - lastColorChangeTime) / FadeDuration;
        ratio = Mathf.Clamp01(ratio);
        material.color = Color.Lerp(startColor, endColor, ratio);
        //material.color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
        //material.color = Color.Lerp(startColor, endColor, ratio * ratio); // Another cool effect

        if (ratio == 1f)
        {
            lastColorChangeTime = Time.time;

            // Switch colors
            var temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }
}