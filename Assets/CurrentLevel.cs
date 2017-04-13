using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevel : MonoBehaviour {
	public int stage = 0;
	public float speed = 1f;
	public bool enabled = false;
	public string levelName;

	private CanvasGroup flavorText;
	private Camera levelCam;
	private LevelData levelData;

	void Start () {
		flavorText = GameObject.Find ("FlavorText").GetComponent<CanvasGroup> ();
		Invoke ("Enable", 2f);
	}

	public void Enable()
	{
		enabled = true;
		SetStage (1);
	}

	public void Disable()
	{
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (!enabled) {
			return;
		}
		if (flavorText) {
			flavorText.alpha = Mathf.Lerp (flavorText.alpha, enabled ? 0f : 1f, 15f * Time.deltaTime);
		}
			
		if (levelCam != null && Camera.main != null) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, levelCam.transform.position, speed * Time.deltaTime);
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, levelCam.orthographicSize, speed * Time.deltaTime);
		}
	}

	public void SetStage(int newStage) {
		if (newStage == stage) {
			return;
		}

		stage = newStage;

		string levelName = "Level " + stage + " Cam";
		Debug.Log ("loading level " + levelName);
		GameObject level = GameObject.Find (levelName);

		levelCam = level.GetComponent<Camera> ();
		levelData = levelCam.GetComponent<LevelData> ();
		levelName = "Level" + stage;

		GameObject manager = GameObject.Find ("GameManager");

		LevelScore totalScore = manager.GetComponent<LevelScore> ();
		CurrentScore score = manager.GetComponent<CurrentScore> ();

		// save the total score from this stage
		totalScore.AddScore (score.GetScore());

		// update the displayed score
		score.Reset ();
		score.SetPar (levelData.par);

		// add the new par
		totalScore.AddPar(levelData.par);
	}

}
