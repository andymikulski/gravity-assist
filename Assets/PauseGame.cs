using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public bool IsPaused = false;
	public bool HasMenu = false;
	public Rect PauseMenu = new Rect(20, 20, 120, 50);

//	private float time = 0;

	private Camera mainCamera;
	private Camera pauseCamera;

	void Start ()
	{
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		pauseCamera = transform.parent.Find ("Pause Camera").GetComponent<Camera>();
	}

	void Update ()
	{
		if( Input.GetKeyDown(KeyCode.Escape))
		{
			IsPaused = !IsPaused;

			if (IsPaused) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
			}

			Time.fixedDeltaTime = 0.02f * Time.timeScale;
//			time = 0;
		}

//		float menuFrom = IsPaused ? 90f : 44.458f;
//		float menuTo = IsPaused ? 44.458f : 90f;
//		time += Time.deltaTime;
//		pauseCamera.transform.rotation = new Quaternion(Mathf.Lerp (menuFrom, menuTo, 0.5f), 0f, 0f, 0f);
	}

	void OnGUI() {
		if (IsPaused && !HasMenu) {
			HasMenu = true;

			mainCamera.gameObject.SetActive (false);
			pauseCamera.gameObject.SetActive (true);
			RenderSettings.fog = true;
		} else if (!IsPaused && HasMenu) {
			HasMenu = false;

			mainCamera.gameObject.SetActive (true);
			pauseCamera.gameObject.SetActive (false);
			RenderSettings.fog = false;
		}

		if (IsPaused && HasMenu) {
			GUI.Window (0, PauseMenu, RenderPauseMenu, "Pause Menu");
		}
	}

	void RenderPauseMenu(int windowID) {
		if (GUI.Button (new Rect (10, 20, 100, 20), "Hello World")) {
			print ("Got a click");
		}
	}
}
