using UnityEngine;
using System.Collections;

public class FlipperChild : MonoBehaviour {

	Flipper flip; // Flipper component attached to parent of this object

	// Use this for initialization
	void Start () {
		// Get the Flipper component attached to parent
		flip = transform.parent.GetComponent<Flipper> ();
	}

	void OnTriggerEnter(Collider target){
		// When a Ball collides with this object, add the ball to ColObjects List
		if (target.gameObject.tag == "BALL") {
			flip.ColObjects.Add (target.gameObject);
		}
	}
	
	void OnTriggerExit(Collider leave){
		// Remove any Game Object no longer colliding with this object from the ColObjects List
		flip.ColObjects.Remove (leave.gameObject);
	}
}
