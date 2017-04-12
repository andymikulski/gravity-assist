using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

//	Gravity thisGravity;

	void Start()
	{
//		thisGravity = gameObject.GetComponent<Gravity> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!gameObject.active || !gameObject.activeInHierarchy || !gameObject.activeSelf) {
			return;
		}

		GameObject other = collision.collider.gameObject;

		if (other.tag == "Player") {
			PlayerShip otherShip = other.GetComponent<PlayerShip> ();

			if (otherShip != null && otherShip.enabled) {
					otherShip.Respawn ();
			}

			Destroy (other.gameObject);
		}
	}

}
