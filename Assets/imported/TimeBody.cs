using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {
    public bool isSpongebob = false;
	bool isRewinding = false;

	public float recordTime = 5f;

	List<PointInTime> pointsInTime;

	CharacterController rb;
    Rigidbody rb2;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();
        if(isSpongebob)
		    rb = GetComponent<CharacterController>();
        else
            rb2 = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalVariables.global.rewindTime)
			StartRewind();
		else
			StopRewind();
	}

	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind ()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}
		
	}

	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind ()
	{
        if(isSpongebob)
        {
            GlobalVariables.global.digitalGlitch.intensity = 0.6f;
            GlobalVariables.global.analogGlitch.scanLineJitter = 0.15f;
            GlobalVariables.global.analogGlitch.verticalJump = 0.15f;
            GlobalVariables.global.analogGlitch.horizontalShake = 0.15f;
            GlobalVariables.global.analogGlitch.colorDrift = 0.15f;
            GlobalVariables.global.busy = true;
        }
		isRewinding = true;
        if(isSpongebob)
		    rb.enabled = false;
        else
            rb2.isKinematic = true;
	}

	public void StopRewind ()
	{
        if(isSpongebob)
        {
            GlobalVariables.global.digitalGlitch.intensity = 0.05f;
            GlobalVariables.global.analogGlitch.scanLineJitter = 0.01f;
            GlobalVariables.global.analogGlitch.verticalJump = 0f;
            GlobalVariables.global.analogGlitch.horizontalShake = 0f;
            GlobalVariables.global.analogGlitch.colorDrift = 0.03f;
            GlobalVariables.global.rewindTime = false;
            GlobalVariables.global.busy = false;
        }
		isRewinding = false;
        if(isSpongebob)
            rb.enabled = true;
        else
            rb2.isKinematic = false;
	}
}