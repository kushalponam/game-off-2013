using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GUIStyle guistyle;

	
	void Start(){
		
	}
	
	void OnGUI(){
		
		if (GUI.Button(new Rect(550,300,150,50),"Play")){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel1")){
				Application.LoadLevel("ChangeLevel1");
			}
		}
		if(GUI.Button(new Rect(550,400,150,50),"Levels")){
			if(Application.CanStreamedLevelBeLoaded("LevelSelect")){
				Application.LoadLevel("LevelSelect");
			}
		}
		if(GUI.Button(new Rect(550,500,150,50),"Credits")){
		}
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel1")){
				Application.LoadLevel("ChangeLevel1");
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel1")){
				Application.LoadLevel("ChangeLevel2");
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			if(Application.CanStreamedLevelBeLoaded("ChangeLevel1")){
				Application.LoadLevel("ChangeLevel3");
			}
		}
	}
}
