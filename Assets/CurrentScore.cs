using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrentScore : MonoBehaviour {
	public Text blastText;
	public Text parText;
	public int score;
	public int par;
	public int lastPar;

	// Use this for initialization
	void Start () {
		blastText = GameObject.Find ("BlastText").GetComponent<Text> ();
		parText = GameObject.Find ("ParText").GetComponent<Text> ();
	}

	public void Reset() {
		blastText = GameObject.Find ("BlastText").GetComponent<Text> ();
		parText = GameObject.Find ("ParText").GetComponent<Text> ();

		score = 0;
		par = lastPar;
		UpdateDisplay ();
	}

	public void UpdateDisplay() {
		blastText.text = "blasts: " + score;
		parText.text = "par: " + par;
	}

	public void AddBlast() {
		score += 1;
		UpdateDisplay ();
	}

	public void RemoveBlast() {
		score -= 1;
		UpdateDisplay ();
	}

	public int GetScore() {
		return score;
	}

	public void SetPar(int value){
		par = value;
		lastPar = par;
		UpdateDisplay ();
	}
}
