using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTiming : MonoBehaviour {
	private CanvasGroup byline;
	private CanvasGroup creditline;

	public AudioSource sound;
	public LoopMusic musicScript;
	public float speed = 0.125f;

	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Theme Music").GetComponent<AudioSource> ();
		byline = GameObject.Find("Byline").GetComponent<CanvasGroup>();
		creditline = GameObject.Find("Creditline").GetComponent<CanvasGroup>();

		byline.alpha = 0f;
		creditline.alpha = 0f;
	}

	// Update is called once per frame
	void Update () {
		if (!sound.isPlaying) {
			return;
		}
		float introLength = musicScript.IntroEndSample * 0.90f;
		byline.alpha = Mathf.Lerp (byline.alpha, sound.timeSamples < introLength / 2 ? 1f : 0f, speed);
		creditline.alpha = Mathf.Lerp (creditline.alpha, (sound.timeSamples > introLength / 2) && sound.timeSamples <= introLength ? 1f : 0f, speed);
	}
}
