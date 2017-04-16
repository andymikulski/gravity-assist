using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour {
	public LayerMask m_MagneticLayers;
	private Vector3 m_Position;
	public float m_Radius;
	public float m_Force;
	public float m_Mass;

	public bool useDimensionsToCalculateForce = false;
	public float strengthMultiplier = 1f;

	private Rigidbody rb;
	public GameObject explosion;
	private Object explosionInstance;
	public GameObject impactSound;
	private Object soundInstance;

	void Start ()
	{
		explosion = Resources.Load ("ShipExplosion") as GameObject;
		impactSound = Resources.Load ("ShipExplosionSound") as GameObject;
		rb = GetComponent<Rigidbody> ();
		UpdateVariables ();
	}

	public void UpdateVariables ()
	{
		if (useDimensionsToCalculateForce) {
			m_Radius = GetComponent<Collider> ().bounds.size.x * 10;
			m_Force = m_Radius / 2;
			m_Mass = m_Force;

			rb.mass = m_Mass;
			rb.drag = Mathf.Infinity;
			rb.angularDrag = Mathf.Infinity;
			m_Force = m_Force * strengthMultiplier;
		}
	}

	void FixedUpdate ()
	{
		Collider[] colliders;
		Rigidbody rigidbody;
		colliders = Physics.OverlapSphere (transform.position + m_Position, m_Radius, m_MagneticLayers);
		foreach (Collider collider in colliders)
		{
			rigidbody = collider.gameObject.GetComponent<Rigidbody> ();

			if (rigidbody != null && rigidbody != gameObject.GetComponent<Rigidbody> ()) {
				rigidbody.AddExplosionForce ((m_Force * Mathf.Sqrt(Vector3.Distance(m_Position, transform.position))) * -1, transform.position + m_Position, m_Radius);
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!gameObject.CompareTag("Dock") && collision.collider.CompareTag ("Player")) {
			PlayerShip otherShip = collision.collider.GetComponent<PlayerShip> ();

			if (collision.relativeVelocity.magnitude <= 0f) {
				return;
			}

			if (otherShip != null && otherShip.enabled && otherShip.gameObject.active) {
				otherShip.Respawn ();
			}

			if (collision.relativeVelocity.magnitude > 10f) {
				Vector3 pos = collision.collider.gameObject.transform.position;
				collision.collider.gameObject.SetActive (false);
				Destroy (collision.collider.gameObject);

				if(explosionInstance != null) {
					Destroy (explosionInstance);
				}

				if(soundInstance != null) {
					Destroy(soundInstance);
				}

				explosionInstance = Instantiate (explosion, pos, new Quaternion());
				soundInstance = Instantiate (impactSound);


				ShakeObject cameraShake = GameObject.Find("CameraManager").GetComponent<ShakeObject> ();
				cameraShake.ShakeCamera (collision.relativeVelocity.magnitude / 300f, 0.02f); 
			}
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position + m_Position, m_Radius);
	}
}
