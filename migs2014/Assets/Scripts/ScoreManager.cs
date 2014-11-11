using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	
	public int numscores; 
	public int score;
	public string playerName;
	public string posKey;
	public TextMesh posName;
	
	public TextMesh hs1name;
	public TextMesh hs2name;
	public TextMesh hs3name;
	public TextMesh hs4name;
	public TextMesh hs5name;
	
	public TextMesh hs1;
	public TextMesh hs2;
	public TextMesh hs3;
	public TextMesh hs4;
	public TextMesh hs5;
	
	public bool getInput;
	public bool prepScores;
	public bool skipInput;
	
	
	// Use this for initialization
	void Start () {
		score = GameManager.ins.score; 
		Destroy (GameManager.ins.gameObject);
		getCurrentScores ();
		getInput = false;
		prepScores = true;
		skipInput = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (prepScores){
			PlaceScore();
			getCurrentScores ();
			prepScores = false;
			getInput = true;
		}
		if (getInput){
			GetName (posKey, posName);
		}
		else {
			getCurrentScores ();
			PlayerPrefs.Save ();
			if (Input.GetKeyDown (KeyCode.Escape)){
				PlayerPrefs.DeleteAll();
				getCurrentScores();
			}
			else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
				Application.LoadLevel ("Title"); 
				this.enabled = false;
			}
		}
	}
	
	private void getCurrentScores(){
		if (!PlayerPrefs.HasKey ("HS1")){
			numscores = 0;
		}
		else if (!PlayerPrefs.HasKey ("HS2")){
			numscores = 1;
			
			hs1.text = PlayerPrefs.GetInt ("HS1").ToString();
			hs1name.text = PlayerPrefs.GetString ("HS1name");
		}
		else if (!PlayerPrefs.HasKey ("HS3")){
			numscores = 2;
			
			hs1.text = PlayerPrefs.GetInt ("HS1").ToString();
			hs1name.text = PlayerPrefs.GetString ("HS1name");
			
			hs2.text = PlayerPrefs.GetInt ("HS2").ToString();
			hs2name.text = PlayerPrefs.GetString ("HS2name");
		}
		else if (!PlayerPrefs.HasKey ("HS4")){
			numscores = 3;
			
			hs1.text = PlayerPrefs.GetInt ("HS1").ToString();
			hs1name.text = PlayerPrefs.GetString ("HS1name");
			
			hs2.text = PlayerPrefs.GetInt ("HS2").ToString();
			hs2name.text = PlayerPrefs.GetString ("HS2name");
			
			hs3.text = PlayerPrefs.GetInt ("HS3").ToString();
			hs3name.text = PlayerPrefs.GetString ("HS3name");
		}
		else if (!PlayerPrefs.HasKey ("HS5")){
			numscores = 4;
			
			hs1.text = PlayerPrefs.GetInt ("HS1").ToString();
			hs1name.text = PlayerPrefs.GetString ("HS1name");
			
			hs2.text = PlayerPrefs.GetInt ("HS2").ToString();
			hs2name.text = PlayerPrefs.GetString ("HS2name");
			
			hs3.text = PlayerPrefs.GetInt ("HS3").ToString();
			hs3name.text = PlayerPrefs.GetString ("HS3name");
			
			hs4.text = PlayerPrefs.GetInt ("HS4").ToString();
			hs4name.text = PlayerPrefs.GetString ("HS4name");
		}
		else {
			numscores = 5;
			
			hs1.text = PlayerPrefs.GetInt ("HS1").ToString();
			hs1name.text = PlayerPrefs.GetString ("HS1name");
			
			hs2.text = PlayerPrefs.GetInt ("HS2").ToString();
			hs2name.text = PlayerPrefs.GetString ("HS2name");
			
			hs3.text = PlayerPrefs.GetInt ("HS3").ToString();
			hs3name.text = PlayerPrefs.GetString ("HS3name");
			
			hs4.text = PlayerPrefs.GetInt ("HS4").ToString();
			hs4name.text = PlayerPrefs.GetString ("HS4name");
			
			hs5.text = PlayerPrefs.GetInt ("HS5").ToString();
			hs5name.text = PlayerPrefs.GetString ("HS5name");
		}
	}
	
	private void PlaceScore(){
		switch (numscores){
		case 0:
			PlayerPrefs.SetInt ("HS1", score);
			posKey = "HS1name";
			PlayerPrefs.DeleteKey (posKey);
			posName = hs1name;
			break;
		case 1:
			if (score > PlayerPrefs.GetInt ("HS1")){
				PlayerPrefs.SetInt ("HS2", PlayerPrefs.GetInt ("HS1"));
				PlayerPrefs.SetInt ("HS1", score);
				
				PlayerPrefs.SetString ("HS2name", PlayerPrefs.GetString ("HS1name"));
				
				posKey =  "HS1name";
				PlayerPrefs.DeleteKey (posKey);
				
				posName = hs1name;
			}
			else {
				PlayerPrefs.SetInt ("HS2", score);
				posKey =  "HS2name";
				PlayerPrefs.DeleteKey (posKey);
				
				posName = hs2name;
			}
			break;
		case 2:
			if (score <= PlayerPrefs.GetInt ("HS2")){
				PlayerPrefs.SetInt ("HS3", score);
				posKey =  "HS3name";
				PlayerPrefs.DeleteKey (posKey);
				
				posName = hs3name;
			}
			else if (score > PlayerPrefs.GetInt ("HS1")){
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", PlayerPrefs.GetInt ("HS1"));
				PlayerPrefs.SetInt ("HS1", score);
				
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				PlayerPrefs.SetString ("HS2name", PlayerPrefs.GetString ("HS1name"));
				
				posKey =  "HS1name";
				PlayerPrefs.DeleteKey (posKey);
				
				posName = hs1name;
			}
			else {
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", score);
				
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				PlayerPrefs.DeleteKey ("HS2name");
				
				posKey =  "HS2name";
				PlayerPrefs.DeleteKey (posKey);
				
				posName = hs2name;
			}
			break;
		case 3:
			if (score <= PlayerPrefs.GetInt ("HS3")){
				PlayerPrefs.SetInt ("HS4", score);
				posKey =  "HS4name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs4name;
			}
			else if (score > PlayerPrefs.GetInt ("HS1")){
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", PlayerPrefs.GetInt ("HS1"));
				PlayerPrefs.SetInt ("HS1", score);
				
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				PlayerPrefs.SetString ("HS2name", PlayerPrefs.GetString ("HS1name"));
				posKey =  "HS1name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs1name;
			}
			else if (score > PlayerPrefs.GetInt ("HS2")){
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", score);
				
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				posKey =  "HS2name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs2name;
			}
			else {
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", score);
				
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				posKey =  "HS3name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs3name;
			}
			break;
		case 4:
			if (score <= PlayerPrefs.GetInt ("HS4")){
				PlayerPrefs.SetInt ("HS5", score);
				posKey =  "HS5name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs5name;
			}
			else if (score > PlayerPrefs.GetInt ("HS1")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", PlayerPrefs.GetInt ("HS1"));
				PlayerPrefs.SetInt ("HS1", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				PlayerPrefs.SetString ("HS2name", PlayerPrefs.GetString ("HS1name"));
				posKey =  "HS1name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs1name;
			}
			else if (score > PlayerPrefs.GetInt ("HS2")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				posKey =  "HS2name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs2name;
			}
			else if (score > PlayerPrefs.GetInt ("HS3")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				posKey =  "HS3name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs3name;
			}
			else {
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				posKey =  "HS4name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs4name;
			}
			break;
		case 5:
			if (score > PlayerPrefs.GetInt ("HS1")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", PlayerPrefs.GetInt ("HS1"));
				PlayerPrefs.SetInt ("HS1", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				PlayerPrefs.SetString ("HS2name", PlayerPrefs.GetString ("HS1name"));
				posKey =  "HS1name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs1name;
			}
			else if (score > PlayerPrefs.GetInt ("HS2")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", PlayerPrefs.GetInt ("HS2"));
				PlayerPrefs.SetInt ("HS2", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				PlayerPrefs.SetString ("HS3name", PlayerPrefs.GetString ("HS2name"));
				posKey =  "HS2name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs2name;
			}
			else if (score > PlayerPrefs.GetInt ("HS3")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", PlayerPrefs.GetInt ("HS3"));
				PlayerPrefs.SetInt ("HS3", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				PlayerPrefs.SetString ("HS4name", PlayerPrefs.GetString ("HS3name"));
				posKey =  "HS3name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs3name;
			}
			else if (score > PlayerPrefs.GetInt ("HS4")){
				PlayerPrefs.SetInt ("HS5", PlayerPrefs.GetInt ("HS4"));
				PlayerPrefs.SetInt ("HS4", score);
				
				PlayerPrefs.SetString ("HS5name", PlayerPrefs.GetString ("HS4name"));
				posKey =  "HS4name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs4name;
			}
			else if (score > PlayerPrefs.GetInt ("HS5")){
				PlayerPrefs.SetInt ("HS5", score);
				posKey =  "HS5name";
				PlayerPrefs.DeleteKey (posKey);
				posName = hs5name;
			}
			else {
				skipInput = true;
			}
			break;
		}
	}
	
	private void GetName(string key, TextMesh namegui){
		if (skipInput){
			getInput = false;
			return;
		}
		foreach (char c in Input.inputString) {
			if (c == "\b"[0]){
				if (namegui.text.Length != 0){
					namegui.text = namegui.text.Substring(0, namegui.text.Length - 1);
					playerName = playerName.Substring(0, playerName.Length-1);
				}
			}
			else if (c == "\n"[0] || c == "\r"[0]){
				PlayerPrefs.SetString (key, playerName);
				getInput = false;
			}
			else if (playerName.Length < 10) {
				namegui.text += char.ToUpper(c);
				playerName += char.ToUpper (c);
			}
			else if (playerName.Length == 10){
				namegui.text = namegui.text.Substring(0, namegui.text.Length - 1) + char.ToUpper(c);
				playerName = playerName.Substring(0, playerName.Length-1) + char.ToUpper(c);
			}
		}
	}
}
