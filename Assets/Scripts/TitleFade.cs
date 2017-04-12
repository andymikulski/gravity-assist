using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFade : MonoBehaviour {
	private CanvasGroup ui;
	public LoopMusic musicScript;

	public AudioSource sound;
	public float speed = 0.125f;

	// Use this for initialization
	void Start ()
	{
		ui = GetComponent<CanvasGroup> ();
		ui.alpha = 0f;


		if (sound == null)
			sound = GetComponent<AudioSource> ();

		if (musicScript == null)
			musicScript = GetComponent<LoopMusic> ();
	}

	void Update ()
	{
		ui.alpha = Mathf.Lerp (ui.alpha, sound.timeSamples >= musicScript.IntroEndSample ? 1f : 0f, speed);
	}
}
