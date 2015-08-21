using UnityEngine;
using System.Collections;

public class CamSystem : MonoBehaviour {

	// GAME OBJECTS
	public GameObject BallTrack; // Empty Game Object that tracks to the ball position
	public GameObject BallFollow; // Empty Game Object that follows BallTrack at a set distance
	public GameObject Ball; // The active Ball Game Object
	public GameObject Cam; // The camera Game Object
	public GameObject GAMELOGIC; // The Game Logic Game Object
	public GameObject Marker_BottomRight_Top; // Empty Game Object marking the highest point of the bottom right of the board
	public GameObject Marker_TopRight_Left; // Empty Game Object marking the leftmost position of the top right of the board
	public GameObject Marker_TopRight_Bottom; // Empty Game Object marking the lowest point of the top right of the board
	public GameObject Marker_TopLeft_Bottom; // Empty Game Object marking the lowest point of the top left of the board
	public GameObject Marker_Launch; // Empty Game Object marking the leftmost position of the ball launcher
	public GameObject Cam_Top_Right; // Empty Game Object marking camera position and rotation for the top right of the board
	public GameObject Cam_Top_Left; // Empty Game Object marking camera position and rotation for the top left of the board
	public GameObject Cam_Lower_Board; // Empty Game Object marking camera position and rotation for the lower board

	// TRANSFORMS
	public Transform Trans_BallTrack; // Transform attached to BallTrack Game Object
	public Transform Trans_BallFollow; // Transform attached to the BallFollow Game Object
	public Transform Trans_Ball; // Transform attached to the Ball Game Object
	public Transform Trans_Cam; // Transform attached to the Camera Game Object
	public Transform Trans_Marker_BottomRight_Top; // Transform attached to the Marker_BottomRight_Top Game Object
	public Transform Trans_Marker_TopRight_Left; // Transform attached to the Marker_TopRight_Left Game Object
	public Transform Trans_Marker_TopRight_Bottom; // Transform attached to the Marker_TopRight_Bottom Game Object
	public Transform Trans_Marker_TopLeft_Bottom; // Transform attached to the Marker_TopLeft_Bottom Game Object
	public Transform Trans_Marker_Launch; // Transform attached to the Marker_Launch Game Object
	public Transform Trans_Cam_Top_Right; // Transform attached to the Cam_Top_Right Game Object
	public Transform Trans_Cam_Top_Left; // Transform attached to the Cam_Top_Left Game Object
	public Transform Trans_Cam_Lower_Board; // Transform attached to the Cam_Lower_Board Game Object

	private Gamelogic _logic; // Gamelogic component attached to the Game Logic Game Object

	// Use this for initialization
	void Start () {
		// Get tagged Game Objects
		BallTrack = GameObject.FindGameObjectWithTag ("CAM_BALL_TRACK");
		BallFollow = BallTrack.transform.GetChild (0).gameObject;
		Cam = GameObject.FindGameObjectWithTag ("MainCamera");
		Cam_Top_Right = GameObject.FindGameObjectWithTag ("CAM_TOPRIGHT");
		Cam_Top_Left = GameObject.FindGameObjectWithTag ("CAM_TOPLEFT");
		Cam_Lower_Board = GameObject.FindGameObjectWithTag ("CAM_LOWERBOARD");
		GAMELOGIC = GameObject.FindGameObjectWithTag ("GAMELOGIC");

		// Get the Gmaelogic component on the Game Logic Game Object
		_logic = GAMELOGIC.GetComponent<Gamelogic> () as Gamelogic;

		// Set the Ball Game Object to the Active Ball from Game Logic
		Ball = _logic.ActiveBall;

		// Get the tranforms from Game Objects
		Trans_BallTrack = BallTrack.transform;
		Trans_BallFollow = BallTrack.transform;
		Trans_Ball = Ball.transform;
		Trans_Cam = Cam.transform;
		Trans_Marker_BottomRight_Top = Marker_BottomRight_Top.transform;
		Trans_Marker_TopRight_Left = Marker_TopRight_Left.transform;
		Trans_Marker_TopRight_Bottom = Marker_TopRight_Bottom.transform;
		Trans_Marker_TopLeft_Bottom = Marker_TopLeft_Bottom.transform;
		Trans_Marker_Launch = Marker_Launch.transform;
		Trans_Cam_Top_Right = Cam_Top_Right.transform;
		Trans_Cam_Top_Left = Cam_Top_Left.transform;
		Trans_Cam_Lower_Board = Cam_Lower_Board.transform;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject followObject = BallFollow; // Used to track which Game Object the camera should follow
		if (_logic.bBallInPlay) {
			// Set the Ball Game Object to the Active Ball of Game Object
			Ball = _logic.ActiveBall;

			// Get the transform of the Ball Game Object
			Trans_Ball = Ball.transform;

			// Determine the position of the ball
			if (Trans_Ball.position.z >= Trans_Marker_TopRight_Bottom.position.z) {
				// At Top of board
				if (Trans_Ball.position.x >= Trans_Marker_TopRight_Left.position.x) {
					// In Top Right
					followObject = Cam_Top_Right;
				} else if(Trans_Ball.position.z >= Trans_Marker_TopLeft_Bottom.position.z){
					// In Top Left
					followObject = Cam_Top_Left;
				}
			} else if(Trans_Ball.position.z <= Trans_Marker_BottomRight_Top.position.z){
				// At Bottom Of board
				followObject = Cam_Lower_Board;
			} else {
				// Follow Ball
				followObject = BallFollow;
			}

			// Determine if the ball is in the launcher
			if(Trans_Ball.position.x >= Trans_Marker_Launch.position.x){
				// Follow Ball
				followObject = BallFollow;
			}

			// Set the location of the BallTrack Game Object to that of the Ball Game Object
			BallTrack.transform.position = Ball.transform.position;

			// Set the position and rotation of the camera to that of the followObject
			Cam.transform.position = Vector3.Lerp(Cam.transform.position, followObject.transform.position, Time.timeScale * 1.0f);
			Cam.transform.rotation = Quaternion.Lerp(Cam.transform.rotation, followObject.transform.rotation, 1.0f);
		}
	}
}
