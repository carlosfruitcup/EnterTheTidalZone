using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>Imported from an earlier project. Do not use this, it's also unused.
/// <para>This is a MonoBehaviour class. </para>
/// </summary>
public class LinearInterpolation : MonoBehaviour
{
    public bool rectTransform = false;
    public float delay = 0f;
    public Vector3 startPosition;
    public Vector4[] keyframes;
    public float[] keyframeSnapTimes;
    public UnityEvent keyframeDone;
    // Start is called before the first frame update
    public void Start()
    {
        if(!rectTransform)
            transform.position = startPosition;
        else
            GetComponent<RectTransform>().localPosition = new Vector2(startPosition.x,startPosition.y);
        StartCoroutine("Go");
    }
    public IEnumerator Go() {
        bool finished = false;
        yield return new WaitForSeconds(delay);
        for(int i=0;i<keyframes.Length;i++) {
            Vector4 keyframe = keyframes[i];
            Debug.Log(keyframe);
            Vector3 keyframePos = new Vector3(keyframe.x,keyframe.y,keyframe.z);
            Vector2 keyframePosRect = new Vector2(keyframe.x,keyframe.y);
            bool done = false;
            float keyframeSnapTime = keyframeSnapTimes[i];
            float keyframeLerpTime = keyframe.w;
            if(!rectTransform) {
                while(!done) {
                    if(Vector3.Distance(transform.position,keyframePos) > keyframeSnapTime) {
                        transform.position = Vector3.Lerp(transform.position,keyframePos,keyframeLerpTime);
                    } else {
                        transform.position = keyframePos;
                        done = true;
                    }
                    yield return null;
                }
            } else {
                while(!done) {
                    if(Vector2.Distance(GetComponent<RectTransform>().localPosition,keyframePosRect) > keyframeSnapTime) {
                        GetComponent<RectTransform>().localPosition = Vector2.Lerp(GetComponent<RectTransform>().localPosition,keyframePosRect,keyframeLerpTime);
                    } else {
                        GetComponent<RectTransform>().localPosition = keyframePosRect;
                        done = true;
                    }
                    yield return null;
                }
            }
            if(keyframes[keyframes.Length-1] == keyframe)
                finished = true;
        }
        if(finished)
            keyframeDone.Invoke();
    }
}
