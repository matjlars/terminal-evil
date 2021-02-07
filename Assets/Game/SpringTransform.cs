using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTransform : MonoBehaviour
{
	[Range(0f, 1f)]
	public float smoothTime = 0.5f;
	Vector3 velocity;

	/// <summary>
	/// Where this GameObject was last frame, before parents may have moved it.
	/// Essentially, the current "desired" world position of this GameObject.
	/// </summary>
	Vector3 worldPosition;

	Vector3 targetPosition{
		get{return transform.parent.position;}
	}

	public void Snap(){
		transform.position = targetPosition;
		worldPosition = transform.position;
		velocity = Vector3.zero;
	}

	void Start(){
		worldPosition = transform.position;
	}

	void Update(){
		worldPosition = Vector3.SmoothDamp(worldPosition, targetPosition, ref velocity, smoothTime);
		transform.position = worldPosition;
	}

	public static float Spring(float from, float to, float time)
	{
		time = Mathf.Clamp01(time);
		time = (Mathf.Sin(time * Mathf.PI * (.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + (1.2f * (1f - time)));
		return from + (to - from) * time;
	}

	public static Vector3 Spring(Vector3 from, Vector3 to, float time)
	{
		return new Vector3(Spring(from.x, to.x, time), Spring(from.y, to.y, time), Spring(from.z, to.z, time));
	}
}