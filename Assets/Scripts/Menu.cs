using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject Btn_Reset;
	public GameObject Btn_Exit;
	public GameObject InstructionText;
	public GameObject ControlsPanel;
	public GameObject ControlsText;

	private bool bMenuShown = false;

	// Use this for initialization
	void Start () {
		Btn_Exit.SetActive (false);
		Btn_Reset.SetActive (false);
		ControlsPanel.SetActive (false);
		ControlsText.SetActive (false);
		Screen.lockCursor = true;
		Cursor.visible = false;
		Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SetMenuShown(!bMenuShown);
		}
	}

	public void SetMenuShown(bool open){
		bMenuShown = open;
		Btn_Exit.SetActive (bMenuShown);
		Btn_Reset.SetActive (bMenuShown);
		InstructionText.SetActive(!bMenuShown);
		ControlsPanel.SetActive(bMenuShown);
		ControlsText.SetActive(bMenuShown);
		Screen.lockCursor = !bMenuShown;
		Cursor.visible = bMenuShown;
		Time.timeScale = ((bMenuShown) ? 0.0f : 1.0f);
	}

	public void ResetGame(){
		Application.LoadLevel (0);
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
