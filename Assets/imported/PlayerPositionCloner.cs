using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionCloner : MonoBehaviour
{
    public GameObject player;
    public bool disabled = false;
    public bool cloneY = false;
    public bool cloneZ = false;
    public float yOffset = 0f;
    public float zOffset = 0f;
    public float yThresholdUp = 0f;
    public float yThresholdDown = 0f;
    public bool lerpToPosition = false;
    public float lerpDuration = 0.75f;
    public bool useMinimap = false;
    public bool isAnimator = false;
    // Update is called once per frame
    void Update()
    {
        if(!disabled) {
            if(!useMinimap) {
                if(isAnimator) {
                    gameObject.transform.localEulerAngles = new Vector3(player.transform.eulerAngles.x,player.transform.eulerAngles.y,gameObject.transform.eulerAngles.z);
                } else if(!lerpToPosition)
                    gameObject.transform.position = new Vector3(player.transform.position.x,((cloneY) ? player.transform.position.y+yOffset : gameObject.transform.position.y),(cloneZ ? player.transform.position.z+zOffset : gameObject.transform.position.z));
                else
                    gameObject.transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, lerpDuration),(cloneY ? Mathf.Lerp(transform.position.y, player.transform.position.y, lerpDuration)+yOffset : transform.position.y),(cloneZ ? Mathf.Lerp(transform.position.z, player.transform.position.z, lerpDuration)+zOffset : gameObject.transform.position.z));
            } else if(useMinimap) {
                gameObject.transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, lerpDuration),(cloneY ? Mathf.Lerp(transform.position.y, player.transform.position.y, lerpDuration)+yOffset : transform.position.y),(cloneZ ? player.transform.position.z+zOffset : gameObject.transform.position.z));
            }
        }
    }
}
