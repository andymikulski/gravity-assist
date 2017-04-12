using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotate : MonoBehaviour {
	public Transform systemCenter;
	public float speed = 50f;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.RotateAround(systemCenter.position, new Vector3(0f, 1f, 0f), Time.deltaTime * speed);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.RotateAround(systemCenter.position, new Vector3(0f, -1f, 0f), Time.deltaTime * speed);
		}
	}
}
