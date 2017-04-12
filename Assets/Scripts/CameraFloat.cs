using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFloat : MonoBehaviour {
	public float amount = 0.02f;
	public float xOffset = 0.01f;
	public float yOffset = 0.01f;

	// Update is called once per frame
	void Update () {
		transform.position = transform.position + new Vector3 (Mathf.Sin (Time.time) * (amount + xOffset), Mathf.Sin (Time.time) * (amount + yOffset), 0f);
	}
}
