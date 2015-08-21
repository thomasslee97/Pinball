using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	/// <summary>
	/// 	Controls the board Game Object
	/// </summary>

	public float fRotateAmount = 10.0f; // The amount to rotate the board when nudging the board
	public float fRotateTime = 2.0f; // Time taken to rotate the board to nudging position
	public float fRotationX = -15.0f; // Angle of the board 
	public float fResetTime = 2.0f; // Time taken to reset the board after nudging
	public bool bRotateBoard = true; // Whether the board should be rotated on startup (DEBUG PURPOSES - SET TO TRUE BEFORE BUILD)

	private bool _bNudging = false; // Whether the player is currently nudging
	private float _fTime = 0.0f; // Stores time since last nudge

	// Use this for initialization
	void Start () {
		if (bRotateBoard) {
			// If the board is to be rotated, call SetupBoard()
			SetupBoard ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButton ("TiltRight") || Input.GetAxisRaw("TiltRightTrigger") != 0)&& !_bNudging) {
			// If any of the buttons mapped to tilting right have been pressed and the board is not being nudged,
			// set _bNudging to true and rotate the board by the rotation amount (stored in fRotateAmount),
			// over the time period stored in fRotateTime
			_bNudging = true;
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(((bRotateBoard) ? fRotationX : 0.0f), transform.localRotation.y, -fRotateAmount)), fRotateTime);
		} else if ((Input.GetButton ("TiltLeft") || Input.GetAxisRaw("TiltLeftTrigger") != 0)&& !_bNudging) {
			// If any of the buttons mapped to tilting left have been pressed and the board is not being nudged,
			// set _bNudging to true and rotate the board by the rotation amount (stored in fRotateAmount),
			// over the time period stored in fRotateTime
			_bNudging = true;
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(((bRotateBoard) ? fRotationX : 0.0f), transform.localRotation.y, fRotateAmount)), fRotateTime);
		} else {
			// Add the time singe the last frame to the time since last nudge
			_fTime += Time.deltaTime;

			// Reset _bNudging and _fTime
			ResetNudge();

			// Reset the board rotation
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(((bRotateBoard) ? fRotationX : 0.0f), transform.localRotation.y, 0.0f)), fRotateTime);
		}
	}

	void ResetNudge(){
		// If enough time has passed since the last nudge, reset _bNudging and _fTime to defaults
		if (_fTime >= fResetTime) {
			_bNudging = false;
			_fTime = 0.0f;
		}
	}

	void SetupBoard(){
		// Rotate the board on the X axis by the amount required for proper board angle
		transform.Rotate (new Vector3 (fRotationX, 0.0f, 0.0f));
	}
}
