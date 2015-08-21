using UnityEngine;
using System.Collections;

public class Kickout : MonoBehaviour {

	public float fKickX = 250.0f; // Kick power along x-axis
	public float fKickZ = 250.0f; // Kick power along y-axis
	public float fWaitTime = 2.5f; // Time to hold the ball before kicking it out

	private Rigidbody _rig; // Rigidbody attached to colliding ball
	private bool _bAllowBall = true; // Whether the kickout should accept balls

	void OnTriggerEnter(Collider col){
		// If a Ball collides with the kickout and the kickout is accepting balls
		if (col.gameObject.tag == "BALL" && _bAllowBall) {
			// Kickout no longer accepting balls
			_bAllowBall = false;

			// Set ball position to above the kickout
			col.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (col.transform.localScale.y / 2) + (transform.localScale.y / 2), transform.localPosition.z);

			// Get the Rigidbody component attached to the ball
			_rig = col.gameObject.GetComponent<Rigidbody>() as Rigidbody;

			// Kill all momentum on the ball
			_rig.constraints = RigidbodyConstraints.FreezeAll;

			// Kick out ball after period of time
			StartCoroutine(Kick ());
		}
	}

	public IEnumerator Kick(){
		yield return new WaitForSeconds(fWaitTime);
		// Free all constraints
		_rig.constraints = RigidbodyConstraints.None;

		// Kick the ball out
		_rig.AddForce (new Vector3(fKickX, 25.0f, fKickZ), ForceMode.Acceleration);

		// Give ball time to free
		yield return new WaitForSeconds(10.0f);

		// Allow ball to enter again
		_bAllowBall = true;
	}
}
