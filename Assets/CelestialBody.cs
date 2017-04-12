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

	void Start ()
	{
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

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position + m_Position, m_Radius);
	}
}
