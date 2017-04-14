using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	GameObject gameManager;
	LevelScore score;
	CurrentLevel level;
	GameObject[] letters;

	// Use this for initialization
	void Start () {
		letters = GameObject.FindGameObjectsWithTag ("GradeLetter");

		gameManager = GameObject.Find ("GameManager");
		score = gameManager.GetComponent<LevelScore> ();
		level = gameManager.GetComponent<CurrentLevel> ();

		GameObject nextButton = GameObject.Find ("NextLevelButton");
		nextButton.GetComponent<NextSceneButton> ().SceneToLoad = level.levelName;

		UpdateText ("ParScore", score.totalPar.ToString());
		UpdateText ("BlastScore", score.totalScore.ToString());
		UpdateText ("TimeScore", "todo");

		float gradeScore = score.totalScore / score.totalPar;

		if (gradeScore <= 0.5) {
			ShowGrade ("A");
		} else if (0.5 < gradeScore && gradeScore <= 0.75) {
			ShowGrade ("B");
		} else if (0.75 < gradeScore && gradeScore <= 1) {
			ShowGrade ("C");
		} else if (1 < gradeScore) {
			ShowGrade ("F");
		}
	}

	void ShowGrade(string grade){
		foreach (GameObject letter in letters) {
			letter.SetActive (letter.name == grade);	
		}

		Debug.Log ("show grade " + grade);
	}

	void UpdateText(string name, string value){
		GameObject textParent = GameObject.Find (name);
		if (textParent != null) {
			Text mainText = textParent.GetComponent<Text> ();
			Text subText = textParent.transform.FindChild("Shadow").GetComponent<Text> ();

			mainText.text = value;
			subText.text = value;
		}
	}
}
