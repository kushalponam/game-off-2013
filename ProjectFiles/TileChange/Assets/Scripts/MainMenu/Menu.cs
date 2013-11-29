using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GUIStyle PlayStyle;
	public GUIStyle LevelStyle;
	public GUIStyle Credits;
	public GameObject sound;
	void Start(){
		Instantiate(sound,Vector3.zero,Quaternion.identity);
	}
	
	void OnGUI(){
		
		if(GUI.Button(new Rect(600,300,150,50)," ",PlayStyle)){
			if(Application.CanStreamedLevelBeLoaded("Story")){
				Application.LoadLevel("Story");
			}
		}
		if(GUI.Button(new Rect(600,400,150,50)," ",LevelStyle)){
			if(Application.CanStreamedLevelBeLoaded("LevelSelect")){
				Application.LoadLevel("LevelSelect");
			}
		}
		if(GUI.Button(new Rect(600,500,150,50)," ",Credits)){
			if(Application.CanStreamedLevelBeLoaded("Credits")){
				Application.LoadLevel("Credits");
			}
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
