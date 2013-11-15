using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
[AddComponentMenu("ChangeGame/Contoller")]
public class PlayerController : MonoBehaviour {
	
	private  Color yellow = new Color(0.92f,0.87f,0.38f);
	private  Color purple = new Color(0.71f,0.40f,0.75f);
	
	public float Horizontal;
	public float Vertical;
	public float MoveStep;
	public LayerMask WallMask;
	public LayerMask Blocks;
	public LayerMask KeyMask;
	
	private PlayerPhysics playerphysics;
	private GameObject tilecolor;
	private bool isStarted=false;
	public bool cantmove=false;
	public bool GotKey=false;
	private int i;
	public enum state{
		Idle,
		Blended,
		Detected,
		Fall
	}
	public state Disguise;
	// Use this for initialization
	void Start () {
		i=1;
		renderer.material.color = yellow;
		GotKey=false;
		isStarted=false;
		cantmove=false;
		Disguise = state.Idle;
		playerphysics=GetComponent<PlayerPhysics>();
	}
	
	
	void ChangeColor(){
	 i++;
	 renderer.material.color = i>1 ? purple:yellow;
	 if(i>=2) i=0;
	}
	
	void ChangePlayerState(GameObject s){
	   switch(s.name){
		case "Safe":
			Disguise = state.Idle;
			break;
		case "Green":
			Disguise = state.Fall;
			if(!isStarted) StartCoroutine("FallintoHole",s);
			break;
		case "Brown":
			Disguise = state.Fall;
			if(!isStarted) StartCoroutine("FallintoHole",s);
			break;
		case "Blue":
			if(renderer.material.color!= purple){
				Disguise = state.Detected;
			}else if(renderer.material.color == purple){
				Disguise = state.Blended;
			}
	        break;
		case "Red":
			if(renderer.material.color!= yellow){
				Disguise = state.Detected;
			}else if(renderer.material.color == yellow){
				Disguise = state.Blended;
			}
			break;
		default:
			break;
		}
	}
	
	
	IEnumerator FallintoHole(GameObject g){
		isStarted = true;
	    yield return new WaitForSeconds(0.2f);
		if(Disguise!=state.Fall){
			isStarted=false;
			StopCoroutine("FallintoHole");
		}else{
			 stopplayer(g);
			 cantmove=true;
			 InvokeRepeating("ReduceSize",0,Time.deltaTime);
			 g.GetComponent<Traps>().OpenDoor();
			}
	 yield return null;
	}
	
	void stopplayer(GameObject g){
		transform.position = new Vector3(g.transform.position.x,g.transform.position.y,transform.position.z);
	}
	void ReduceSize(){
		Vector3 s = new Vector3(3,3,3);
		transform.localScale -= s*3*Time.deltaTime;
		if(transform.localScale.x<0.5f){
			GameManager GM = Camera.main.GetComponent<GameManager>();
			GM.SpawnPlayer(0.5f);
			Destroy(gameObject);	
			CancelInvoke("ReduceSize");
			}
	}
	
	void RotatePlayer(){
		float Rotate=0;
		if(Input.GetButtonDown("Horizontal")){
		Rotate = Horizontal>0?90:270;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,Rotate));
		return;
		}else if(Input.GetButtonDown("Vertical")){
		Rotate = Vertical>0?180:0;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,Rotate));
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		 Horizontal=cantmove?0:Input.GetAxisRaw("Horizontal");
	     Vertical=cantmove?0:Input.GetAxisRaw("Vertical");
		
		if(Horizontal!=0&&Vertical!=0) return;
		
		RotatePlayer();
		
		Vector3 p = transform.position;
	    tilecolor = playerphysics.DetectTile(p,5,Blocks);
		
		if(Input.GetButtonDown("Change")){
			ChangeColor();
		}
		
		ChangePlayerState(tilecolor);

		if(Input.GetButtonDown("Horizontal")||Input.GetButtonDown("Vertical")){
			
			playerphysics.DetectWalls(ref Horizontal,ref Vertical,p,MoveStep,WallMask,KeyMask,ref GotKey);
			Vector3 final = new Vector3(MoveStep*Horizontal,MoveStep*Vertical,0);
			transform.position += final;
		}		
	}

}
