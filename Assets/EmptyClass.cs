using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupPlayerShip : MonoBehaviour {
	private LineRenderer _lineRenderer;
	private TrackPlayer trackPlayer;
	private MouseOrbitZoom mainCameraControls;
	private MouseOrbitZoom bgCameraControls;
	public float magnitude = 10f;

	private Vector3 _currentPosition;
	private Vector3 bgFgOffset;
	private ParticleSystem engineBlast;
	public AudioClip[] blastSounds;
	private AudioSource engineAudio;

	private ShakeObject cameraShake;

	public void Start()
	{
		_lineRenderer = gameObject.AddComponent<LineRenderer>() as LineRenderer;
		engineAudio = gameObject.AddComponent<AudioSource>() as AudioSource;

		_lineRenderer.startWidth = 1f;
		_lineRenderer.endWidth = 1f;
		_lineRenderer.enabled = false;

		//		engineBlast = GameObject.Find ("Dope Fire Trail").GetComponent<ParticleSystem>();

		cameraShake = Camera.main.GetComponent<ShakeObject> ();
		if (cameraShake == null) {
			cameraShake = Camera.main.gameObject.AddComponent<ShakeObject>() as ShakeObject;
			cameraShake.smooth = true;
			cameraShake.smoothAmount = 5f;
		}
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_lineRenderer.SetPosition(0, transform.position);
			_lineRenderer.numPositions = 1;
			_lineRenderer.enabled = true;

			Time.timeScale = 0.2f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
		else if (Input.GetMouseButton(0))
		{
			_currentPosition = GetCurrentMousePosition();
			_lineRenderer.numPositions = 2;
			_lineRenderer.SetPosition (0, transform.position);
			_lineRenderer.SetPosition (1, _currentPosition);
			//			gameObject.transform.rotation = Quaternion.LookRotation(_currentPosition);
			gameObject.transform.rotation = Quaternion.Inverse (Quaternion.LookRotation (_currentPosition));

			Time.timeScale = 0.2f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
	}

	public void FixedUpdate()
	{
		if (Input.GetMouseButtonUp(0))
		{

			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;

			_lineRenderer.enabled = false;
			Vector3 releasePosition = GetCurrentMousePosition();
			Vector3 direction = releasePosition - transform.position;
			Vector3 impulse = direction * magnitude;

			gameObject.transform.rotation = Quaternion.Inverse (Quaternion.LookRotation (releasePosition));
			gameObject.GetComponent<Rigidbody> ().AddForce (impulse);

			BlastEngines ();
			ShakeCamera (impulse.magnitude / 2000);
		}
	}

	private Vector3 GetCurrentMousePosition()
	{
		if (Camera.main != null) {
			Vector3 foundPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			return foundPoint;
		} else {
			return new Vector3 ();
		}
	}

	public void ShakeCamera(float amount)
	{
		float clampedAmount = Mathf.Clamp (amount, 0.2f, 0.8f);
		//		cameraShake.ShakeCamera (clampedAmount, clampedAmount * 0.05f);
		cameraShake.ShakeCamera (amount, amount * 0.05f);
	}

	public void BlastEngines()
	{
		// engineBlast.Stop ();
		// float engineBlastDuration = engineBlast.main.duration;
		// engineBlastDuration = direction.magnitude;
		//		engineBlast.Play ();

		if (blastSounds.Length > 0) {
			engineAudio.clip = blastSounds [Random.Range (0, blastSounds.Length)];
			engineAudio.Play ();
		}
	}
}