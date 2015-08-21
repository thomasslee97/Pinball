using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public Color LitColour;
	public Color UnlitColour;
	public float fWaited = 0.0f;

	private Renderer rendererer;
	
	private Material material;

	private bool bFlashing = false;

	// Use this for initialization
	void Start () {
		rendererer = GetComponent<Renderer>();
		material = rendererer.sharedMaterial;

		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		UpdateMaterial (UnlitColour);
	}

	public void UpdateMaterial(Color colour){
		material.SetColor("_Color", colour);
		//DynamicGI.SetEmissive (GetComponent<Renderer>(), new Color (1f, 0.1f, 0.5f, 1.0f) * 10.0f);
		DynamicGI.UpdateMaterials(rendererer);
		DynamicGI.UpdateEnvironment();
	}

	public IEnumerator Flash(float fWaitTime){
		while (bFlashing) {
			yield return new WaitForSeconds(0.1f);
		}
		UpdateMaterial (LitColour);
		bFlashing = true;

		Debug.Log ("FLASHING");

		yield return new WaitForSeconds (fWaitTime);

		UpdateMaterial (UnlitColour);
		bFlashing = false;
	}

	public void SimpleFlash(){
		StartCoroutine(Flash(0.5f));
	}
}
