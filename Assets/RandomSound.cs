using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour {

	private AudioSource sound;
	public AudioClip[] clips;

	void Awake() {
		sound = gameObject.AddComponent<AudioSource>();
		if (clips.Length > 0) {
			sound.clip = clips [Random.Range (0, clips.Length)];
			sound.Play ();
		}
	}
}
