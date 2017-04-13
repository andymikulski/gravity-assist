using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	private CapsuleCollider checkpointCollider;
	private CelestialBody gravity;
	public int nextStage = 1;

	public bool isFlipped = false;
	public float speed = 2f;
	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;

		gravity = GetComponent<CelestialBody> ();

		checkpointCollider = gameObject.AddComponent<CapsuleCollider> ();
		checkpointCollider.radius = 2f;
		checkpointCollider.isTrigger = true;
	}

	void Update()
	{
		Quaternion rot = Quaternion.Euler (new Vector3 (0f, isFlipped ? 180f : 0f, 0f)) * startRotation;
		transform.rotation = Quaternion.Lerp (transform.rotation, rot, speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		bool hasPlayerCollision = false;

		Collider other = collision.collider;

		if (other.tag == "Player") {
			PlayerShip player = other.GetComponent<PlayerShip> ();

			if (player != null && player.enabled) {
				hasPlayerCollision = true;
			}
		}

		isFlipped = hasPlayerCollision;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			return;
		}
			
		StartCoroutine (SetNextPoint (other, 1f));
	}


	IEnumerator SetNextPoint(Collider other, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);

		gravity.m_Force = 20f;
		PlayerShip player = other.GetComponent<PlayerShip> ();
		if (player != null) {
			player.SetCheckpoint (gameObject);

			GameObject.Find ("GameManager").GetComponent<CurrentLevel> ().SetStage (nextStage);
		}
	}
}
