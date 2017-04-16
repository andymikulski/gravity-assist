using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCrawl : MonoBehaviour {
	
	public float finalDestination;
	public float speed = 1f;
	private float offset;
	private bool enabled = false;
	public float startTime = 2.8f;

	void Start(){
		Invoke ("Enable", startTime);
	}

	void Enable(){
		enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (!enabled) {
			return;
		}

		Vector3 pos = transform.localPosition;
		if (pos.y < finalDestination) {
			pos.y += (speed * Time.deltaTime);
			transform.localPosition = pos;
		} else {
			pos.y = finalDestination;
			transform.localPosition = pos;
		}
	}
}
