using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		
		if (GUI.Button(new Rect(550,220,150,50),"Level 1")){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel1")){
			Application.LoadLevel("ChangeLevel1");
		}
		}
		if(GUI.Button(new Rect(550,280,150,50),"Level 2")){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel2")){
			Application.LoadLevel("ChangeLevel2");
		}
		}
		if(GUI.Button(new Rect(550,340,150,50),"Level 3")){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel3")){
			Application.LoadLevel("ChangeLevel3");
		}
		}
		if(GUI.Button(new Rect(550,400,150,50),"Level 4")){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel4")){
			Application.LoadLevel("ChangeLevel4");
		}
		}
		if(GUI.Button(new Rect(550,460,150,50),"Main Menu")){
			if(Application.CanStreamedLevelBeLoaded("MainMenu")){
			Application.LoadLevel("MainMenu");
		}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
