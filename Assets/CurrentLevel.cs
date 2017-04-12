using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevel : MonoBehaviour {
	public int stage = 1;
	public float speed = 1f;
	public bool enabled = false;

	private CanvasGroup flavorText;

	void Start () {
		flavorText = GameObject.Find ("FlavorText").GetComponent<CanvasGroup> ();
		Invoke ("Enable", 2f);
	}

	public void Enable()
	{
		enabled = true;
	}

	public void Disable()
	{
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
		flavorText.alpha = Mathf.Lerp (flavorText.alpha, enabled ? 0f : 1f, 15f * Time.deltaTime);

		if (!enabled) {
			return;
		}

		string levelName = "Level " + stage + " Cam";
		GameObject levelCam = GameObject.Find (levelName);
		Camera mainCam = Camera.main;

		if (levelCam != null && mainCam != null) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, levelCam.transform.position, speed * Time.deltaTime);
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, levelCam.GetComponent<Camera> ().orthographicSize, speed * Time.deltaTime);
		}
	}

	public void SetStage(int newStage) {
		stage = newStage;
	}
}
