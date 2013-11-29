using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GUIStyle nextstyle;
	
	private string levelname;
	private string leveltoload;
	void Start(){
		
		switch(Application.loadedLevelName){
		case "Story":
			leveltoload = "HLevel1";
			break;
		case "HLevel1":
			leveltoload = "ChangeLevel1";
			break;
		case "HLevel2":
			leveltoload = "ChangeLevel2";
			break;
		case "HLevel3":
			leveltoload = "ChangeLevel3";
			break;
		case "HLevel4":
			leveltoload = "ChangeLevel4";
			break;
		case"Won":
			leveltoload = "Credits";
			break;
		case "Credits":
			leveltoload = "Thanks";
			break;
		case "Thanks":
			leveltoload = "MainMenu";
			break;
		
		default:
			break;
		}
		
		
	}
	
	
	void OnGUI(){
		
		if(GUI.Button(new Rect(700,450,150,50)," ",nextstyle)){
			if(Application.CanStreamedLevelBeLoaded(leveltoload)){
				Application.LoadLevel(leveltoload);
			}
		}
	}
	
}
