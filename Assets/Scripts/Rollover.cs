using UnityEngine;
using System.Collections;

public class Rollover : MonoBehaviour {

	public int iScoreForHit = 0;
	public GameObject LightObject;

	private GameObject ScoreObj;
	private Scoresystem sys;
	private LightController control;

	// Use this for initialization
	void Start () {
		ScoreObj = GameObject.FindGameObjectWithTag ("SCORESYSTEM");
		sys = ScoreObj.GetComponent<Scoresystem> () as Scoresystem;
		control = LightObject.GetComponent<LightController> () as LightController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		StartCoroutine(control.Flash (0.5f));
		sys.AddScore (iScoreForHit);
	}
}
