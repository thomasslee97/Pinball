using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flipper : MonoBehaviour {

	public bool bIsLeft = false; // Whether this is a left or right flipper
	public float fFlipperForce = 40.0f; // Force used to rotate flipper (Higher = faster rotation)
	public float fFlipForce = 5000.0f; // Force applied to ball when the flipper flips

	private int _iFlipButton = 1; // Mouse button assigned to this flipper
	private string _sFlipper = "FlipRight"; // Input profile assigned to this flipper

	private Rigidbody _rig; // Rigidbody attached to this flipper
	private ConstantForce _force; // ConstantForce component attached to this flipper

	public List<GameObject> ColObjects = new List<GameObject>(); // List of Game Objects currently colliding with flipper

	// Use this for initialization
	void Start () {
		// If this flipper is on the left, change the input mapping accordingly
		if (bIsLeft) {
			_iFlipButton = 0;
			_sFlipper = "FlipLeft";
		}

		// Get the Rigidbody and ConstantForce components attached to flipper Game Object
		_rig = gameObject.GetComponent<Rigidbody> () as Rigidbody;
		_force = gameObject.GetComponent<ConstantForce> () as ConstantForce;
	}
	
	// Update is called once per frame
	void Update () {
		// If any of the flip buttons are pressed, flip the flipper, otherwise reset the flipper position
		if (Input.GetMouseButton (_iFlipButton) || Input.GetButton(_sFlipper)) {
			Flip (true);
		} else {
			Flip (false);
		}
	}

	void Flip(bool up){

		// If the flipper is being flipped
		if (up) {
			// If the flipper is not at the maximum position
			if (((bIsLeft) ? transform.localRotation.y >= -0.4f : transform.localRotation.y <= 0.4f)){

				// Free all constraints
				_rig.constraints = RigidbodyConstraints.None;

				// Apply relative torque to the flipper to rotate the flipper
				_force.relativeTorque = new Vector3 (0.0f, ((bIsLeft) ? -fFlipperForce : fFlipperForce), 0.0f);
				foreach(GameObject go in ColObjects){
					// Apply a force to each Ball Game Object currently colliding with the flipper
					go.GetComponent<Ball>().Flipper(gameObject, fFlipForce);
				}
			}else{
				// Freeze rotation on the Y axis
				_rig.constraints = RigidbodyConstraints.FreezeRotationY;
			}
			
		} else {
			// Freeze rotation on the Y axis (To kill momentum)
			_rig.constraints = RigidbodyConstraints.FreezeRotationY;

			// Free all constraints
			_rig.constraints = RigidbodyConstraints.None;

			// If the flipper is below the minimum position
			if(((bIsLeft) ? transform.localRotation.y >= 0.0f : transform.localRotation.y <= 0.0f)){
				// Freeze rotation on the Y axis
				_rig.constraints = RigidbodyConstraints.FreezeRotationY;
				// Reset flipper position
				transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
			}else{
				// Apply relative torque to rotate the flipper
				_force.relativeTorque = new Vector3 (0.0f, ((bIsLeft) ? fFlipperForce : -fFlipperForce), 0.0f);
			}
		}
	}
}
