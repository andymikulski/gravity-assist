using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpin : MonoBehaviour {
	public float spinSpeedX;
	public float spinSpeedY;
	public float spinSpeedZ;

	void Start ()
	{
		// Random initial start rotation (provide variation across prefabs)
		transform.Rotate (new Vector3 (0f, Random.Range(0f, 360f) , 0f));

		if (spinSpeedX == 0 && spinSpeedY == 0 && spinSpeedZ == 0) {
			spinSpeedX = Random.Range (0.05f, 0.25f);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (spinSpeedX, spinSpeedY, spinSpeedZ));
	}
}
