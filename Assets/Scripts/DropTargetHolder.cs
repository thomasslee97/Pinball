using UnityEngine;
using System.Collections;

public class DropTargetHolder : MonoBehaviour {

	public GameObject ScoreSystem; // Game Object controlling the score system
	public bool bSimpleReset = true; // Stores whether the targets reset as soon as the final drop target is hit
	public bool bDrop = true; // Set to false if being used as standup target

	public int iTargetsDropped = 0; // Stores number of targets dropped
	public int iTargets = 3; // Stores total number of target attached to this holder
	public int iScoreForCompletion = 1000; // Score given to the player for hitting all drop targets
	public Scoresystem sys; // The Scoresystem attached to the ScoreSystem Game Object

	// Use this for initialization
	void Start () {

		// Get the Scoresystem attached to the ScoreSystem Game Object
		sys = ScoreSystem.GetComponent<Scoresystem> () as Scoresystem;
	}

	// Add score and reset targets if needed
	public void AlertHit(){

		// If this is a drop target holder
		if (bDrop) {

			// Increase the number of target dropped
			iTargetsDropped++;

			// If all the targets have been dropped
			if (iTargetsDropped == iTargets) {

				// If the targets are to be reset as soon as all target are dropped
				if (bSimpleReset) {

					// Iterate through all drop targets and reset them to their original positions
					for (int i = 0; i < transform.childCount; i++) {
						transform.GetChild (i).GetComponent<DropTarget> ().ResetTarget ();
					}

					// Reset the number of targets dropped
					iTargetsDropped = 0;

					// Add score to the score system for hitting all the targets
					sys.AddScore (iScoreForCompletion);
				}
			}
		}
	}
}
