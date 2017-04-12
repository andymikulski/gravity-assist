using UnityEngine;
using System.Collections;

public class LoopMusic : MonoBehaviour
{
	AudioSource sound;
	public int IntroEndSample = 0;
	public bool LoopMusicOnComplete = true;

	void Start()
	{
		sound = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (!sound.isPlaying) {
			Loop ();
		}
	}

	void Loop()
	{
		if (!LoopMusicOnComplete)
		{
			return;
		}

		sound.timeSamples = IntroEndSample;
		sound.Play ();
	}
}