using UnityEngine;
using System.Collections;

public class LoopMusic : MonoBehaviour
{
	AudioSource sound;
	public int IntroEndSample = 0;
	public bool LoopMusicOnComplete = true;
	private bool isEnabled = false;

	void Start()
	{
		sound = GetComponent<AudioSource>();
		Invoke ("PlaySound", 2f);
	}

	void PlaySound()
	{
		isEnabled = true;
		sound.Play ();
	}

	void Update()
	{
		if (isEnabled && !sound.isPlaying) {
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