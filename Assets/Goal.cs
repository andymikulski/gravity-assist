using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
	public string NextScene;

	void OnCollisionEnter(Collision collision)
	{
		Collider other = collision.collider;

		if (other.tag == "Player") {
			PlayerShip player = other.GetComponent<PlayerShip> ();
			if (player != null && player.enabled) {

				// update the game manager scores before going to the score screen
				GameObject manager = GameObject.Find ("GameManager");
				LevelScore totalScore = manager.GetComponent<LevelScore> ();
				CurrentScore score = manager.GetComponent<CurrentScore> ();
				CurrentLevel currentLevel = manager.GetComponent<CurrentLevel> ();


				// save the total score from this stage
				totalScore.AddScore (score.GetScore());
				SceneManager.LoadScene("ScoreScreen", LoadSceneMode.Single);

				// set the 'current level name' to whatever the props says is next
				// this is read from the score screen that we're about to load, so it
				// knows where to go after the player reviews their grade etc
				currentLevel.levelName = NextScene;
			}
		}
	}
}
