using UnityEngine;
using System.Collections;

public class Eventmanager : MonoBehaviour {
	
	
	public delegate void press();
	public static event press OnMovementButtonPress;
	
	void CheckForButtonPress(){
		
		if(Input.GetButton("Horizontal")&&Input.GetButtonDown("Vertical")){
			OnMovementButtonPress=null;
			return;
		}
		
		if(Input.GetButton("Horizontal")||Input.GetButtonDown("Vertical")){
			OnMovementButtonPress();
		}
	}

	// Update is called once per frame
	void Update () {
	//CheckForButtonPress();
	}
}
