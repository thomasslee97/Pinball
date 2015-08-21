using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	public GameObject Launcher;
	public GameObject Start_Pos;
	public GameObject End_Pos;
	public GameObject Launcher_Child;
	public float fTimeToFull = 5.0f;
	public float fMaxForce = 60.0f;
	public float fPlungerForceMax = 50000.0f;

	// Privates

	public float fTimeHeld = 0.0f;
	public float fDistanceDiff = 0.0f;
	public ConstantForce LauncherForce;
	public Rigidbody rig;
	public Repel rep;
	public bool bHasMoved = false;

	// Use this for initialization
	void Start () {
		Launcher.transform.position = Start_Pos.transform.position;
		fDistanceDiff = Start_Pos.transform.localPosition.z - End_Pos.transform.localPosition.z;
		if (fDistanceDiff < 0) {
			fDistanceDiff *= -1.0f;
		}
		LauncherForce = Launcher.GetComponent<ConstantForce> () as ConstantForce;
		rig = Launcher.GetComponent<Rigidbody> () as Rigidbody;
		rep = Launcher.transform.GetChild (0).GetComponent<Repel> () as Repel;
		rep.ChangeRepel (0.0f);
		Launcher_Child = Launcher.transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Launcher_Child.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		if (Input.GetButton("PullPlunger")) {
			bHasMoved = true;
			fTimeHeld += Time.deltaTime;

			if(fTimeHeld > fTimeToFull){
				fTimeHeld = fTimeToFull;
			}

			MoveLauncher();

		} else {
			if(bHasMoved){
				float fTimePower = 1.0f - ((fTimeToFull - fTimeHeld)/fTimeToFull);

				fTimeHeld = 0.0f;
				
				float fRatio = 0.0f;
				
				fRatio = Start_Pos.transform.localPosition.z - Launcher.transform.localPosition.z;

				rep.ChangeRepel(fPlungerForceMax * fTimePower);
								
				if(Launcher.transform.localPosition.z <= Start_Pos.transform.localPosition.z){
					rig.constraints = RigidbodyConstraints.None;
					LauncherForce.relativeForce = new Vector3(0.0f, 0.0f, fMaxForce * fRatio);
				}else{
					rig.constraints = RigidbodyConstraints.FreezePosition;
					rep.ChangeRepel(0.0f);
					Launcher.transform.localPosition = Start_Pos.transform.localPosition;
					LauncherForce.relativeForce = new Vector3(0.0f, 0.0f, 0.0f);
				}

				bHasMoved = false;
				fTimeHeld = 0.0f;

				StartCoroutine(ResetPlunger());
			}
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			StartCoroutine(ResetPlunger());
		}
	}

	IEnumerator ResetPlunger(){
		yield return new WaitForSeconds (0.1f);
		rig.constraints = RigidbodyConstraints.FreezePosition;
		Launcher.transform.localPosition = Start_Pos.transform.localPosition;
		LauncherForce.relativeForce = new Vector3(0.0f, 0.0f, 0.0f);
		bHasMoved = false;
		fTimeHeld = 0.0f;
		rep.RevertRepel ();
	}

	void MoveLauncher(){
		float fRatio = 0.0f;
		fRatio = fTimeHeld / fTimeToFull;
		
		if (fRatio > 1.0f) {
			fRatio = 1.0f;
		}

		if (fRatio < 0.0f) {
			fRatio = 0.0f;
		}

		rig.constraints = RigidbodyConstraints.None;
		rep.ChangeRepel (0.0f);
		
		Launcher.transform.localPosition = new Vector3 (Launcher.transform.localPosition.x, Launcher.transform.localPosition.y, Start_Pos.transform.localPosition.z - (fRatio * fDistanceDiff));
	}
}
