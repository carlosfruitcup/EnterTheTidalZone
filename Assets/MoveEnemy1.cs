using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveEnemy1 : MonoBehaviour // change to MonoBehaviour' to 'BaseEnemy' whenever possible
{
	public int distance;
	private int tracker;
	private int negative;
	private bool lorr;

    // Start is called before the first frame update
    void Start()
    {
		transform.position = new Vector3(1.0f, 1.0f, 1.0f);
		tracker = distance;
		negative = distance - distance * 2;
		lorr = true;
		transform.position = transform.position + new Vector3(-16, -15, -5);
    }

    // Update is called once per frame
    void Update()
    {
        if(tracker == 0){
			lorr = !lorr;
			tracker = distance;
		} else {
			if(lorr){
				transform.Translate(Vector3.right * distance);
				tracker--;
			} else {
				transform.Translate(Vector3.right * negative);
				tracker--;
			}
		}
    }
}
