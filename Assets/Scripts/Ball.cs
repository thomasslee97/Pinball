using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	/// <summary>
	/// 	Controls the ball game object in game
	/// </summary>

	public GameObject GAMELOGIC; // The Game Logic Game Object

	private ConstantForce _force; // ConstantForce component on this Game Object
	private Gamelogic _logic; // Gamelogic component on Game Logic Game Object

	// Use this for initialization
	void Start () {
		// Get the ConstantForce component, Game Logic Game Object, and Gamelogic component
		_force = GetComponent<ConstantForce> () as ConstantForce;
		GAMELOGIC = GameObject.FindGameObjectWithTag ("GAMELOGIC");
		_logic = GAMELOGIC.GetComponent<Gamelogic> () as Gamelogic;
	}

	void OnTriggerEnter(Collider col){
		// When the ball enters a trigger, check if the ball has collided with a drain
		if (col.gameObject.tag == "DRAIN") {
			Debug.Log("BALL LOST"); // Debug purposes only

			// Alert Gamelogic that there is no longer a ball in play
			_logic.bBallInPlay = false;

			// Set the ActiveBall Game Object to null
			_logic.ActiveBall = null; 

			// Spawn a new ball
			_logic.SpawnBall();

			// Destroy this ball
			Destroy(this.gameObject);
		}
	}

	public void Flipper(GameObject flip, float flipForce){
		// Apply a force to the ball Rigidbody component to force it away from a flipper
		Vector3 direction = flip.transform.position - transform.position;
		Quaternion toRotation = Quaternion.FromToRotation (transform.up, direction);
		transform.rotation = Quaternion.Lerp (transform.rotation, toRotation, 0.01f * Time.time);
		gameObject.GetComponent<Rigidbody>().AddForce (new Vector3(0.0f, 0.0f, flipForce), ForceMode.Acceleration);
	}

	public IEnumerator RemoveForce(){
		// Remove all forces in relative space from the ball
		yield return new WaitForSeconds(0.01f);
		_force.relativeForce = new Vector3 (0.0f, 0.0f, 0.0f);
	}
}
