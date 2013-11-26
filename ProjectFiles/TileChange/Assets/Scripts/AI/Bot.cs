using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bot : PlayerPhysics {
	
	
	
	private GameObject WayPointController;
	public List<Transform> waypointlist;
	public int currentwaypoint=0;
	private Vector3 OriginalPosition;
	
	public int Damage;
	
	public GameObject Seed; 
	private int xdirection,ydirection;
	private GameManager GM;
	// Use this for initialization
	public override void Start () {
	base.Start();
	Damage = 30;
	GM = Camera.main.GetComponent<GameManager>();
	OriginalPosition = transform.position;
	SetUpWaypoints();
		
	}
	void SetUpWaypoints(){
		WayPointController = GameObject.Find("Waypoints");
		Transform[] waypoints = WayPointController.GetComponentsInChildren<Transform>();
		waypointlist = new List<Transform>();
		foreach(Transform t in waypoints){
			if(t!=WayPointController.transform){
			waypointlist.Add(t);
			}
		}
	}
	void OnTriggerEnter(Collider col){
		if(col.collider.gameObject.tag=="Arrow"){
		//	Debug.Log("HitBy Arrow");
		    Damage -=5;
		//	Debug.Log("now the damage is"+Damage);
			if(Damage<0){
				GM.AddKeyAtStart=true;
				GM.KeySpawn.transform.position = new Vector3(transform.position.x,transform.position.y,8.5f);
				GM.SpawnKey();
				Destroy(gameObject);	
			}
		}
	}
	float x, y;
	void DetectChameleon(){
		ray = new Ray(new Vector3(x,y,7),new Vector3(xdirection,ydirection,0));
		Debug.DrawRay(ray.origin,ray.direction,Color.red);
		if(Physics.Raycast(ray,out hit,10)){
			if(hit.collider.tag == "Player"){
		     //  Instantiate(Seed,transform.position,Quaternion.identity);
			  Debug.Log("Player Detected");
			}
		}
		
	}
	float currentwaypointX;
	float currentwaypointY;
	float rotationz;
	void RotateEnemy(){
		Vector3 relativerotation = new Vector3(currentwaypointX-transform.position.x,currentwaypointY-transform.position.y,0);
		if(relativerotation.x<0 && relativerotation.y>0){
			rotationz=0;
			x = transform.position.x+c.x;
		    y = transform.position.y+c.y+s.x/2;
			xdirection=0;
			ydirection=1;
		}else if(relativerotation.x>0 && relativerotation.y>0){
			rotationz=270;
			x = transform.position.x+c.x+s.x/2;
		    y = transform.position.y+c.y;
		    ydirection=0;
			xdirection=1;
		}else if(relativerotation.x>0 && relativerotation.y<0){
			rotationz=180;
			x = transform.position.x+c.x;
		    y = transform.position.y+c.y-s.y/2;
			xdirection=0;
			ydirection=-1;
		}else if(relativerotation.x<0 && relativerotation.y<0){
			rotationz = 90;
			x = transform.position.x+c.x-s.x/2;
		    y = transform.position.y+c.y;
			xdirection=-1;
			ydirection=0;
		}
		transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y,rotationz));
	
	}
	
	void MoveTowardsNextWaypoint(){
	 currentwaypointX = waypointlist[currentwaypoint].transform.position.x;
	 currentwaypointY = waypointlist[currentwaypoint].transform.position.y;
		Vector3 Distance = new Vector3((currentwaypointX-OriginalPosition.x),(currentwaypointY-OriginalPosition.y) , 0);
		transform.position +=Distance*Time.deltaTime/5;
		if(Vector3.Distance(waypointlist[currentwaypoint].transform.position,transform.position)<3){
			OriginalPosition = waypointlist[currentwaypoint].transform.position;
			currentwaypoint++;
			if(currentwaypoint>=waypointlist.Count){
				currentwaypoint=0;
			}
		}
	}
	
	// Update is called once per frame
	public override void Update () {
	
	MoveTowardsNextWaypoint();
		RotateEnemy();
		DetectChameleon();
	}
}
