using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public float fSpinTime = 1.0f;
	public int iScoreForHit = 500;

	private bool bSpinning = false;
	private GameObject Spinner_ob;
	public GameObject ScoreObj;
	private Scoresystem sys;
	private Rigidbody rig;
	private ConstantForce force;

	// Use this for initialization
	void Start () {
		Spinner_ob = transform.GetChild (0).gameObject;
		force = Spinner_ob.GetComponent<ConstantForce> () as ConstantForce;
		rig = Spinner_ob.GetComponent<Rigidbody> () as Rigidbody;
		ScoreObj = GameObject.FindGameObjectWithTag ("SCORESYSTEM");
		sys = ScoreObj.GetComponent<Scoresystem> () as Scoresystem;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "BALL") {
			float diff = col.transform.localPosition.z - transform.localPosition.z;
			StartCoroutine(Spin (((diff) < 0.0f) ? true : false));
		}
	}

	public IEnumerator Spin(bool diff){
		bSpinning = true;
		force.relativeTorque = new Vector3(((diff) ? 5.0f : -5.0f), 0.0f, 0.0f);
		sys.AddScore(iScoreForHit);
		yield return new WaitForSeconds(fSpinTime);
		force.relativeTorque = new Vector3(0.0f, 0.0f, 0.0f);
		bSpinning = false;
		StartCoroutine (Stop ());
	}

	public IEnumerator Stop(){
		while ((Spinner_ob.transform.localRotation.x < 350.0f && Spinner_ob.transform.localRotation.x > 190.0f) || (Spinner_ob.transform.localRotation.x > 10.0f && Spinner_ob.transform.localRotation.x < 170.0f)) {
			yield return new WaitForSeconds(0.01f);
		}

		if (!bSpinning) {
			rig.constraints = RigidbodyConstraints.FreezeAll;
			Spinner_ob.transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			rig.constraints = RigidbodyConstraints.None;
		}
	}
}
