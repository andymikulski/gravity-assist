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
	private CurrentLevel level;

//	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
		level = GameObject.Find ("GameManager").GetComponent<CurrentLevel> ();

		startRotation = transform.rotation;

		gravity = GetComponent<CelestialBody> ();

		checkpointCollider = gameObject.AddComponent<CapsuleCollider> ();
		checkpointCollider.radius = 2f;
		checkpointCollider.isTrigger = true;
	}

	void Update()
	{
		if (level.stage >= nextStage) {
			isFlipped = true;
		}
		Quaternion rot = Quaternion.Euler (new Vector3 (0f, isFlipped ? 180f : 0f, 0f)) * startRotation;
		transform.rotation = Quaternion.Lerp (transform.rotation, rot, speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (isFlipped) {
			return;
		}

		bool hasPlayerCollision = false;

		Collider other = collision.collider;

		if (other.CompareTag("Player")) {
			PlayerShip player = other.GetComponent<PlayerShip> ();

			if (player != null && player.enabled) {
				hasPlayerCollision = true;
				StartCoroutine (SetNextPoint (other, 1f));
//
//				foreach (AudioClip clip in clips) {
//					AudioSource sound = gameObject.AddComponent<AudioSource> ();
//					sound.volume = 0.5f;
//					sound.PlayOneShot (clip);
//				}
			}
		}
	}

	IEnumerator SetNextPoint(Collider other, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);

		gravity = GetComponent<CelestialBody> ();
		gravity.m_Force = 20f;
		PlayerShip player = other.GetComponent<PlayerShip> ();
		if (player != null) {
			player.SetCheckpoint (gameObject);

			level.SetStage (nextStage);
		}
	}
}
