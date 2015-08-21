using UnityEngine;
using System.Collections;

public class DropTarget : MonoBehaviour {

	public bool bDrop = true; // True: Drop target, False: Standup target

	private GameObject _Holder; // The drop target holder Game Object
	private DropTargetHolder _HolderScript; // The DropTargetHolder script attached to the _Holder Game Object
	private float _fOriginalPosition; // The original vertical position of the drop target

	// Use this for initialization
	void Start () {
		_Holder = transform.parent.gameObject;
		_HolderScript = _Holder.GetComponent<DropTargetHolder> () as DropTargetHolder;
		_fOriginalPosition = transform.localPosition.z;
	}

	void OnTriggerEnter(Collider col){
		// When an object enters the trigger

		// If the colliding object is a ball
		if (col.gameObject.tag == "BALL") {

			// If this is a drop target
			if (bDrop) {
				// Set the position of the target to the dropped position
				transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, -10.0f);
			}

			// Add the score and handle target logic
			_HolderScript.AlertHit ();
		}
	}

	public void ResetTarget(){
		// Reset the target position to its original position
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, _fOriginalPosition);
	}
}
