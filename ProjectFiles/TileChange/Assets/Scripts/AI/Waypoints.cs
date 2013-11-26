using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour {
	
	
	public static Waypoints start;
	public Waypoints next;
	public bool isStart=false;
	// Use this for initialization
	void Awake () {
	
		if(!next){
			Debug.LogWarning("There is no next waypoint");
		}
		if(isStart){
			start = this;
		}
	}
	
	
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position,1);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position,next.transform.position);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
