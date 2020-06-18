using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>Unused? Supposed to trigger a UnityEvent upon clicking it.
/// <para>This is a class based on MonoBehaviour and IPointerDownHandler. </para>
/// </summary>
public class ClickObject: MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onClick;
    public void OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke();
    }
}