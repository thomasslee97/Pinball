using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {

	private LightController _control; // The LightController object for this bumper

	// Use this for initialization
	void Start () {
		// Get the LightController component on the second child of this Game Object
		_control = transform.GetChild (1).GetComponent<LightController> () as LightController;
	}

	void OnTriggerEnter(Collider col){
		// Call SimpleFlash() in the LightController object
		_control.SimpleFlash ();
	}
}
