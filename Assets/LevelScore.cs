using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour {
	public int totalPar = 0;
	public int totalScore = 0;

	public void AddPar(int value){
		totalPar += value;
	}

	public void SetPar(int value){
		totalPar = value;
	}

	public void AddScore(int value){
		totalScore += value;
	}

	public float GetTotalScore() {
		return totalScore / totalPar;
	}

	public void Reset() {
		totalPar = 0;
		totalScore = 0;
	}
}
