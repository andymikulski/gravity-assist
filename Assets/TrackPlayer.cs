using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour {
	public Transform target;
	public float xOffset = 0f;
	public float yOffset = -10f;
	public float zOffset = 0f;
	public float CameraSpeed = 20f;
	public AnimationCurve CameraMotion = AnimationCurve.Linear(0,0,1,1);

	private Vector3 lastPosition;
	public bool BeSmooth;

	void LateUpdate () {
		if (target != null) {
			lastPosition = target.position - new Vector3 (xOffset, yOffset, zOffset);
			if (BeSmooth) {
				transform.position = Vector3.Lerp (transform.position, lastPosition, CameraMotion.Evaluate (Time.deltaTime) * CameraSpeed);
			} else {
				transform.position = lastPosition;
			}
		}
	}

	public Vector3 GetLastPosition()
	{
		return lastPosition;
	}
}
