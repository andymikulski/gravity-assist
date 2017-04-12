using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelenTester : MonoBehaviour {
	private PlayerShip player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerShip> ();
	}

	Rigidbody SpawnPlayer()
	{
		GameObject newPlayer = Instantiate (gameObject, transform.position + new Vector3 (1f, 0f, 0f), new Quaternion (0f, 0f, 0f, 0f));
		Rigidbody rb = newPlayer.GetComponent<Rigidbody> ();
		if (rb == null) {
			rb = newPlayer.AddComponent<Rigidbody> ();
		}
		rb.velocity = new Vector3(0f, 0f, 0f); 
		rb.angularVelocity = new Vector3(0f, 0f, 0f);

		return rb;
	}
	
	// Update is called once per frame
	void Update () {
		player = GetComponent<PlayerShip> ();


		if (player != null && Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log (player.basePower + " - " + player.basePower * Vector3.forward + " - " + (player.basePower * Vector3.forward).magnitude);
			SpawnPlayer ().AddForce (player.basePower * Vector3.forward);
			SpawnPlayer ().AddForce (player.basePower * (Vector3.forward + Vector3.right));
			SpawnPlayer ().AddForce (player.basePower * ((Vector3.forward + (Vector3.right / 2f))));
			SpawnPlayer ().AddForce (player.basePower * ((Vector3.forward + (Vector3.right * 1.5f))));
			SpawnPlayer ().AddForce (player.basePower * (Vector3.right));
			SpawnPlayer ().AddForce (player.basePower * ((Vector3.back + Vector3.right)));
			SpawnPlayer ().AddForce (player.basePower * ((Vector3.back + (Vector3.right / 2f))));
			SpawnPlayer ().AddForce (player.basePower * ((Vector3.back + (Vector3.right * 1.5f))));
			SpawnPlayer ().AddForce (player.basePower * (Vector3.back));
		}
	}
}
