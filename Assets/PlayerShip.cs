using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {
	private LineRenderer _lineRenderer;
	private TrackPlayer trackPlayer;
	private MouseOrbitZoom mainCameraControls;
	private MouseOrbitZoom bgCameraControls;
	public float basePower = 10f;

	private Vector3 _currentPosition;
	private Vector3 bgFgOffset;
	private bool hasFollowButton = false;
	private ParticleSystem engineBlast;
	public AudioClip[] blastSounds;
	private AudioSource engineAudio;

	private CurrentScore currentScore;

	public GameObject respawnPlatform;
	public bool enabled = true;

	public float spawnOffsetX = 0f;
	public float spawnOffsetY = 0f;
	public float spawnOffsetZ = 0f;

	public void RefreshReferences()
	{
		if (respawnPlatform == null) {
			respawnPlatform = GameObject.Find ("Starting Point");
		}

		_lineRenderer = GetComponent<LineRenderer> ();
		_lineRenderer.enabled = false;

		trackPlayer = Camera.main.GetComponent<TrackPlayer> ();

		mainCameraControls = Camera.main.GetComponent<MouseOrbitZoom> ();

		engineBlast = gameObject.transform.Find ("Dope Fire Trail").GetComponent<ParticleSystem>();
		engineAudio = GetComponent<AudioSource>();


		currentScore = GameObject.Find ("GameManager").GetComponent<CurrentScore> ();
	}

	public void Start()
	{
		RefreshReferences ();
	}

	public void Respawn()
	{
		Debug.Log ("respawning...");

		Quaternion startRotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

		Vector3 startPos = respawnPlatform.transform.position + new Vector3 (spawnOffsetX, spawnOffsetY, spawnOffsetZ);

		PlayerShip newPlayer = Instantiate (this, startPos, startRotation);
		Rigidbody rb = newPlayer.GetComponent<Rigidbody> ();
		rb.velocity = new Vector3(0f, 0f, 0f); 
		rb.angularVelocity = new Vector3(0f, 0f, 0f);

		newPlayer.name = "Player";
		newPlayer.RefreshReferences ();

		currentScore.Reset ();

		// disable this script
		enabled = false;
		this.enabled = false;
//		this.gameObject.SetActive (false);
		Destroy (this);
	}

	public void SetCheckpoint(GameObject checkpoint)
	{
		respawnPlatform = checkpoint;
	}


	public void Update()
	{
		if (Camera.main != null)
		{
			hasFollowButton = Input.GetButton ("Jump"); 
			trackPlayer.enabled = hasFollowButton;
			if (hasFollowButton) {
				Vector3 newCamPosition = Camera.main.transform.position;
				newCamPosition.y = mainCameraControls.target.position.y;

				mainCameraControls.target.transform.position = newCamPosition;
//				bgCameraControls.target.transform.position = new Vector3 (newCamPosition.x - bgFgOffset.x, bgCameraControls.target.transform.position.y - bgFgOffset.y, newCamPosition.z - bgFgOffset.z);
			}

			Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
			bool onScreen = screenPoint.z > -0.1 && screenPoint.x > -0.1 && screenPoint.x < 1.1 && screenPoint.y > 0 && screenPoint.y < 1.1;
			if (!onScreen) {
				Respawn ();
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Respawn ();
		}

		if (Input.GetMouseButtonDown (0) || Input.GetMouseButton (0)) {
			Vector3 allowedDestination = Vector3.ClampMagnitude (_currentPosition - transform.position, 15f);

			_currentPosition = GetCurrentMousePosition ();
			_lineRenderer.positionCount = 2;
			_lineRenderer.SetPosition (0, transform.position);
			_lineRenderer.SetPosition (1, transform.position + allowedDestination);
			_lineRenderer.enabled = true;
			gameObject.transform.rotation = Quaternion.LookRotation (_currentPosition);

			Time.timeScale = 0.2f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		} else if (Input.GetMouseButtonUp (0)) {
			// respawnPlatform.SetActive (false);

			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;

			_lineRenderer.enabled = false;

			CelestialBody dock = respawnPlatform.GetComponent<CelestialBody> ();
			dock.m_Force = 2f;

			Vector3 releasePosition = _lineRenderer.GetPosition (1);
			Vector3 direction = releasePosition - _lineRenderer.GetPosition (0);
			Vector3 impulse = direction * basePower;

			Debug.Log ("kicking with " + direction + " - " + basePower + " - " + direction.magnitude + " - " + impulse.magnitude);

			gameObject.transform.rotation = Quaternion.LookRotation (releasePosition);
			gameObject.GetComponent<Rigidbody> ().AddForce (impulse);

			currentScore.AddBlast ();
			BlastEngines ();
			ShakeCamera (impulse.magnitude / 2000);
		} else {
			_lineRenderer.enabled = false;
		}
	}

	private Vector3 GetCurrentMousePosition()
	{
		if (Camera.main != null) {
			Vector3 foundPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			foundPoint.y = 0;

			return foundPoint;
		} else {
			return new Vector3 ();
		}
	}

	public void ShakeCamera(float amount)
	{
		ShakeObject cameraShake = GameObject.Find ("CameraManager").GetComponent<ShakeObject> ();
		cameraShake.smooth = false;
		cameraShake.smoothAmount = 5f;

		float clampedAmount = Mathf.Clamp (amount, 0.1f, 0.4f);
		cameraShake.ShakeCamera (clampedAmount, clampedAmount * 0.05f);
	}

	public void BlastEngines()
	{
		// engineBlast.Stop ();
		// float engineBlastDuration = engineBlast.main.duration;
		// engineBlastDuration = direction.magnitude;
		engineBlast.Play ();

		if (blastSounds.Length > 0) {
			engineAudio.clip = blastSounds [Random.Range (0, blastSounds.Length)];
			engineAudio.Play ();
		}
	}
}