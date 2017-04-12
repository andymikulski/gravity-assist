using UnityEngine;
using System.Collections;

public class FakeOrbit : MonoBehaviour  {
	public Transform center;
	public float degreesPerSecond = -65.0f;

	public bool offKilter = true;

	private Vector3 v;

	void Update () {
		Vector3 direction = offKilter ? Vector3.up + Vector3.left : Vector3.up;

		transform.RotateAround (center.position, direction, degreesPerSecond * Time.deltaTime);

		Vector3 relativePos = center.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
	}
}