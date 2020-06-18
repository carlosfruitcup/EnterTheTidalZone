using UnityEngine;
/// <summary>This stores position and rotation information for time travel.
/// <para>This is a normal class. </para>
/// <seealso cref="TimeBody"/> 
/// </summary>
public class PointInTime {

	public Vector3 position;
	public Quaternion rotation;

	public PointInTime (Vector3 _position, Quaternion _rotation)
	{
		position = _position;
		rotation = _rotation;
	}

}