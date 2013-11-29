using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bot : PlayerPhysics {
	
	public Texture2D[] HealthTextures;
	public bool Haskey;
	public GameObject WayPointController;
	[HideInInspector]
	public List<Transform> waypointlist;
	public int currentwaypoint=0;
	private Vector3 OriginalPosition;
	
	public Texture2D[] animatedtextures;
	
	public int Damage;
	
	public GameObject Seed; 
	private int xdirection,ydirection;
	private bool isStarted;
	private GameObject Bar;
	// Use this for initialization
	public override void Start () {
	base.Start();
	isStarted=false;
	Damage = 30;
	OriginalPosition = transform.position;
	SetUpWaypoints();
	StartCoroutine("AnimateEnemy");
	Bar = transform.FindChild("Health").gameObject;
	}
	void SetUpWaypoints(){
		//WayPointController = GameObject.Find("Waypoints");
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
			audio.Play();
			UpdateDamageFeedBack();
		    Destroy(col.gameObject);
			Damage -=5;
			if(Damage<0){
			  if(Haskey){
					GM.AddKeyAtStart=true;
					GM.KeySpawn.transform.position = new Vector3(transform.position.x,transform.position.y,8.5f);
					GM.SpawnKey();
				}
				Destroy(gameObject);	
			}
		}
	}
	float x, y;
	GameObject g;
	void DetectChameleon(){
		ray = new Ray(new Vector3(x,y,7),new Vector3(xdirection,ydirection,0));
		Debug.DrawRay(ray.origin,ray.direction,Color.red);
		if(Physics.Raycast(ray,out hit,20)){
			if(hit.collider.tag == "Player"){
				if(hit.collider.gameObject.GetComponent<PlayerController>().Disguise==PlayerController.state.Idle) return;
				if(!isStarted) StartCoroutine("ThrowSeed");
			}
		}
	}
	
	IEnumerator ThrowSeed(){
		isStarted=true;
	    g=Instantiate(Seed,new Vector3(transform.position.x,transform.position.y,7),Quaternion.identity)as GameObject;
		Stone s = g.GetComponent<Stone>();
		s.xDirection = xdirection;
		s.yDirection = ydirection;
		yield return new WaitForSeconds(0.5f);
		isStarted=false;
		yield return null;
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
		//	y = transform.position.y+c.y;
			xdirection=0;
			ydirection=1;
		}else if(relativerotation.x>0 && relativerotation.y>0){
			rotationz=270;
		//	x=transform.position.x+c.x;
			x = transform.position.x+c.x+s.x/2;
		    y = transform.position.y+c.y;
		    ydirection=0;
			xdirection=1;
		}else if(relativerotation.x>0 && relativerotation.y<0){
			rotationz=180;
			x = transform.position.x+c.x;
		    y = transform.position.y+c.y-s.y/2;
		  //  y = transform.position.y+c.y;
			xdirection=0;
			ydirection=-1;
		}else if(relativerotation.x<0 && relativerotation.y<0){
			rotationz = 90;
			x = transform.position.x+c.x-s.x/2;
		//	x = transform.position.x+c.x;
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
	
	
	int t;
	IEnumerator AnimateEnemy(){
		
	Begin:
		renderer.material.SetTexture("_MainTex",animatedtextures[t]);
		yield return new WaitForSeconds(0.2f);
		t++;
	    if(t>=2) t=0;
	goto Begin;
	}
	
	
	void UpdateDamageFeedBack(){
		
		Bar.renderer.material.SetTexture("_MainTex",HealthTextures[5-(Damage/6)]);
		
	}
	
	// Update is called once per frame
	public override void Update () {
	
	    MoveTowardsNextWaypoint();
		RotateEnemy();
		DetectChameleon();
		
	}
}
