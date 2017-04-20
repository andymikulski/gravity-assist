using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

	public Object[] asteroids;
	public Queue<Object> asteroidQueue;

	public float width;
	public float height;

	// Use this for initialization
	void Start () {
		BoxCollider collider = gameObject.AddComponent<BoxCollider> ();
		collider.center = new Vector3 (0f, 0f, -0.525f * height);
		collider.size = new Vector3 (width, 10f, 10f);
		collider.isTrigger = true;

		asteroidQueue = new Queue<Object> (asteroids.Length);

		foreach (Object asteroid in asteroids) {
			asteroidQueue.Enqueue (asteroid);
		}

		SpawnAsteroid ();
		SpawnAsteroid ();
		SpawnAsteroid ();
		SpawnAsteroid ();
		SpawnAsteroid ();
		SpawnAsteroid ();
	}

	void OnTriggerEnter(Collider collide){
		if (collide.gameObject.name.Contains ("Asteroid ")) {
			Destroy (collide.gameObject);
			SpawnAsteroid ();
		}
	}

	private GameObject SpawnAsteroid () {
		Object asteroidPrefab = asteroidQueue.Dequeue ();
		asteroidQueue.Enqueue (asteroidPrefab);

		float startX = Random.Range (0, width);
		startX -= width / 2;

		float startZ = height / 2;

		Vector3 startPos = new Vector3 (startX, 0f, startZ);

		GameObject newAsteroid = Instantiate (asteroidPrefab) as GameObject;
		newAsteroid.transform.SetParent (gameObject.transform);

		newAsteroid.transform.localPosition = startPos;

		newAsteroid.GetComponent<Rigidbody> ().AddForce (new Vector3 (0f, 0f, -1 * Random.Range(500, 1000)));
		return newAsteroid;
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(width, 0f, height));
	}
}
