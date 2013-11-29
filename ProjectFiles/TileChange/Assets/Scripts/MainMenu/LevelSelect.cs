using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	
	public GUIStyle level1;
	public GUIStyle level2;
	public GUIStyle level3;
	public GUIStyle level4;
	public GUIStyle menu;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		
		if (GUI.Button(new Rect(200,200,150,70)," ",level1)){
			if(Application.CanStreamedLevelBeLoaded("Story")){
			Application.LoadLevel("Story");
		}
		}
		if(GUI.Button(new Rect(550,200,150,70)," ",level2)){
			if(Application.CanStreamedLevelBeLoaded("HLevel2")){
			Application.LoadLevel("HLevel2");
		}
		}
		if(GUI.Button(new Rect(200,340,150,70)," ",level3)){
			if(Application.CanStreamedLevelBeLoaded("HLevel3")){
			Application.LoadLevel("HLevel3");
		}
		}
		if(GUI.Button(new Rect(550,340,150,70)," ",level4)){
			if(Application.CanStreamedLevelBeLoaded("HLevel4")){
			Application.LoadLevel("HLevel4");
		}
		}
		if(GUI.Button(new Rect(650,500,150,50)," ",menu)){
			if(Application.CanStreamedLevelBeLoaded("MainMenu")){
			Application.LoadLevel("MainMenu");
		}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
