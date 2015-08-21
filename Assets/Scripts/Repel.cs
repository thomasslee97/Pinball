using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Repel : MonoBehaviour {

	public float fExplosionStrength = 10.0f;

	public int iScoreForHit = 0;

	private float fOldStrength = 0.0f;

	private GameObject ScoreObj;
	private Scoresystem sys;

	private List<GameObject> ColObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
		fOldStrength = fExplosionStrength;

		ScoreObj = GameObject.FindGameObjectWithTag ("SCORESYSTEM");
		sys = ScoreObj.GetComponent<Scoresystem> () as Scoresystem;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider target){
		if (target.gameObject.tag == "BALL") {
			RepelObject(target.gameObject);
			ColObjects.Add (target.gameObject);
			sys.AddScore(iScoreForHit);
		}
	}

	void OnTriggerExit(Collider leave){
		ColObjects.Remove (leave.gameObject);
	}

	void RepelObject(GameObject target){
		Vector3 forceVec = -target.gameObject.GetComponent<Rigidbody>().velocity.normalized * fExplosionStrength;
		target.gameObject.GetComponent<Rigidbody>().AddForce (forceVec, ForceMode.Acceleration);
	}

	public void ChangeRepel(float val){
		fExplosionStrength = val;
		foreach (GameObject go in ColObjects) {
			RepelObject(go);
		}
	}

	public void RevertRepel(){
		fExplosionStrength = fOldStrength;
	}
}
