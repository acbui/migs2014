using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	private TextMesh mesh;
	// Use this for initialization
	void Start () {
		mesh = gameObject.GetComponent<TextMesh> ();
		this.renderer.sortingLayerName = "UI";
	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = "" + GameManager.ins.score;
	}
}
