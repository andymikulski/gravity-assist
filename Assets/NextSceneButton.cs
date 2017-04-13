using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour {

	public AudioClip buttonSound;
	private AudioSource audioPlayer;

	public string SceneToLoad;

	private void Start()
	{
		audioPlayer = GetComponent<AudioSource> ();
	}

	public void GoToNext ()
	{
		PlaySoundWithCallback(buttonSound, LoadLevelSelect);
	}

	private void LoadLevelSelect()
	{
		SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
	}



	public delegate void AudioCallback();
	public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
	{
		audioPlayer.PlayOneShot(clip);
		StartCoroutine(DelayedCallback(clip.length, callback));
	}
	private IEnumerator DelayedCallback(float time, AudioCallback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
}
