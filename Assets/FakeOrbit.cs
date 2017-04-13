using UnityEngine;
using System.Collections;

public class FakeOrbit : MonoBehaviour  {
	public Transform center;
	public float degreesPerSecond = -65.0f;

	public bool offKilter = true;

	public bool useRandoms = false;
	public float minSpeed = 5f;
	public float maxSpeed = 65f;

	private Vector3 v;

	void Start(){
		if (useRandoms) {
			degreesPerSecond = Random.Range (minSpeed, maxSpeed);
			bool negative = Random.value < 0.5;

			if (negative) {
				degreesPerSecond *= -1;
			}

			offKilter = Random.value < 0.5;
			Vector3 direction = offKilter ? Vector3.up + Vector3.left : Vector3.up;

			transform.RotateAround (center.position, direction, Random.Range(0f, 360f));
		}
	}

	void Update () {
		Vector3 direction = offKilter ? Vector3.up + Vector3.left : Vector3.up;
		transform.RotateAround (center.position, direction, degreesPerSecond * Time.deltaTime);

		Vector3 relativePos = center.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
	}
}