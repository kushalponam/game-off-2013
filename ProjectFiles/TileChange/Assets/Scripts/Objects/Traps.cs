using UnityEngine;
using System.Collections;

public class Traps : MonoBehaviour {
	
	private GameObject doors;
	void Start(){
		doors = transform.FindChild("Doors").gameObject;
	}
	public void CloseDoor(){
		if(!doors.animation.IsPlaying("Close")){
			doors.animation.Play("Close");
		}
	}
	public void OpenDoor(){
		if(!doors.animation.IsPlaying("Open")){
			doors.animation.Play("Open");
		}
		Invoke("CloseDoor",0.5f);
	}

}
