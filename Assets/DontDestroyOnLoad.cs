using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {
	public bool EnsureUnique = false;
	void Awake () {
		if (!EnsureUnique) {
			DontDestroyOnLoad (gameObject);
			return;
		}

		Debug.Log ("asdf" + GameObject.FindGameObjectsWithTag ("GameManager").Length);
		if (GameObject.FindGameObjectsWithTag ("GameManager").Length <= 1) {
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
