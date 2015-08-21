using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Specialized;

public class Scoresystem : MonoBehaviour {

	public int iScore = 0;
	public float fScoreMultiplier = 1.0f;
	public GameObject ScoreText;
	public Text Txt_ScoreText;

	private int iCurrentMultID = 0;

	private OrderedDictionary Modifiers = new OrderedDictionary();

	// Use this for initialization
	void Start () {
		Txt_ScoreText = ScoreText.GetComponent<Text> () as Text;
	}
	
	// Update is called once per frame
	void Update () {
		string output = iScore.ToString();
		output = output.PadLeft(8, '0');
		Txt_ScoreText.text = output;
	}

	public void AddScore(int val){
		iScore += (int)(val * fScoreMultiplier);
	}

	public string RequestMultId(){
		iCurrentMultID++;
		return iCurrentMultID.ToString();
	}

	public void AddMultiplier(string modId, float mod){
		if(!Modifiers.Contains(modId)){
			fScoreMultiplier *= mod;
			Modifiers.Add (modId, mod);
		}
	}

	public void RemoveModifier(string modId){
		if (Modifiers.Contains (modId)) {
			float mod = (float)Modifiers [modId];
			fScoreMultiplier /= mod;
			Modifiers.Remove (modId);
		}
	}

	public int GetScore(){
		return iScore;
	}
}
