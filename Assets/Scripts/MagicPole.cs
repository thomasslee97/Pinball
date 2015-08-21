using UnityEngine;
using System.Collections;

public class MagicPole : MonoBehaviour {

	//private GameObject Holder;

	// Use this for initialization
	void Start () {
		//Holder = transform.parent.gameObject;
		transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		LowerPole ();
	}
	
	void OnTriggerEnter(){
		LowerPole ();
	}

	public void ResetPole(){
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, 0.0f);
	}

	public void LowerPole(){
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, -2.6f);
	}
}
