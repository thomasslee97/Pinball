using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gamelogic : MonoBehaviour {

	public int iBallsRemaining = 3; // Number of balls remaining to launch
	public bool bBallInPlay = false; // Whether there is a ball currently on the board
	public bool bMagicPoleUpAtStart = true; // Whether the magic pole should be raised at the start

	// Game Objects required for logic
	public GameObject BallSpawn; // Empty Game Object marking ball spawn position
	public GameObject BallPrefab; // Prefab used for instantiation of new Balls
	public GameObject Canvas; // UI Canvas
	public GameObject MagicPole; // Magic Pole parent game object
	public GameObject Pnl_GameOver; // Gameover background panel
	public GameObject Pnl_Score; // Score background panel
	public GameObject Pnl_Score_Pause; // Paused score background panel
	public GameObject Txt_Score; // Score text
	public GameObject Txt_Score_Pause; // Paused score text
	public GameObject Txt_Score_Score_Pause; // Paused score output
	public GameObject Txt_GameOver; // Gameover text
	public GameObject ActiveBall; // Active Ball Game Object

	// Use this for initialization
	void Start () {
		// Find Ball on board at start
		ActiveBall = GameObject.FindGameObjectWithTag ("BALL");
		// Find ball spawn position
		BallSpawn = GameObject.FindGameObjectWithTag ("BALLSPAWN");

		// If the magic pole should be raised at the start, set it to the raised position
		if (bMagicPoleUpAtStart) {
			MagicPole.GetComponent<MagicPole> ().ResetPole ();
		}

		// Set up starting values
		bBallInPlay = true;
		iBallsRemaining--;
		Txt_GameOver.SetActive (false);
		Pnl_GameOver.SetActive (false);
		Txt_Score_Pause.SetActive (false);
		Txt_Score_Score_Pause.SetActive (false);
		Pnl_Score_Pause.SetActive (false);
	}

	public void SetActiveBall(GameObject ob){
		// Change the active Ball Game Object
		ActiveBall = ob;
	}

	public void SpawnBall(){
		// If the player has balls remaining, create a new ball, otherwise display the gameover menu
		if (iBallsRemaining > 0) {
			ActiveBall = Instantiate (BallPrefab, BallSpawn.transform.position, Quaternion.identity) as GameObject;
			bBallInPlay = true;
			iBallsRemaining--;
		} else {
			DisplayMenu();
		}
	}

	public void DisplayMenu(){
		// Hide in-game UI and display game over menu
		Txt_GameOver.SetActive (true);
		Txt_Score_Pause.SetActive (true);
		Txt_Score_Score_Pause.SetActive (true);
		Txt_Score.SetActive (false);
		Pnl_GameOver.SetActive (true);
		Pnl_Score.SetActive (false);
		Pnl_Score_Pause.SetActive (true);
		Txt_Score_Pause.GetComponent<Text> ().text = Txt_Score.GetComponent<Text> ().text;
		Canvas.GetComponent<Menu> ().SetMenuShown (true);
	}
}
