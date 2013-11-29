using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
[AddComponentMenu("ChangeGame/Contoller")]
public class PlayerController : MonoBehaviour {
	
	//private Color yellow = new Color(0.81f,0.82f,0.32f);
	//private Color purple = new Color(0.71f,0.40f,0.75f);
	//private Color blue   = new Color(0.24f,0.48f,0.62f);
	//private Color Red    = new Color(0.69f,0.46f,0.21f);
	

	public Texture[] colors;

	[HideInInspector]
	// used by walls to push
	public bool CanBePushed;
	[HideInInspector]
	public float Horizontal;
	[HideInInspector]
	public float Vertical;
	public float MoveStep;
	public LayerMask WallMask;
	public LayerMask Blocks;
	
	[HideInInspector]
	public PlayerPhysics playerphysics;
	private GameObject tilecolor;
	
	[HideInInspector]
	public bool cantmove=false;
	
	[HideInInspector]
	public int NumberofColors;
	
	[HideInInspector]
	public float TileTime;

	
	private GameObject LastHitHole;
	public enum state{
		Idle,
		Blended,
		Detected,
		Fall
	}
	public state Disguise;
	// Use this for initialization
	void Start () {
		CanBePushed=true;
		cantmove=false;
		Disguise = state.Idle;
		playerphysics=GetComponent<PlayerPhysics>();
		
	}
		
	void ChangeColor(int i){
	renderer.material.SetTexture("_MainTex",colors[i]);
	}
	
	void ChangePlayerState(GameObject s){
	   switch(s.name){
		case "Safe":
			Disguise = state.Idle;
			break;
		case "Hole":
			Disguise = state.Fall;
			break;
		case "Yellow":
			if(renderer.material.mainTexture!= colors[0]){
				Disguise = state.Detected;
			}else if(renderer.material.mainTexture==colors[0]){
				Disguise = state.Blended;
			}
			break;
		case "Blue":
			if(renderer.material.mainTexture!= colors[2]){
				Disguise = state.Detected;
			}else if(renderer.material.mainTexture==colors[2]){
				Disguise = state.Blended;
			}
			break;
		case "Purple":
			if(renderer.material.mainTexture!= colors[1]){
				Disguise = state.Detected;
			}else if(renderer.material.mainTexture==colors[1]){
				Disguise = state.Blended;
			}
	        break;
		case "Red":
			if(renderer.material.mainTexture!= colors[3]){
				Disguise = state.Detected;
			}else if(renderer.material.mainTexture==colors[3]){
				Disguise = state.Blended;
			}
			break;
		default:
			break;
		}
	}
	public void ReduceSize(){
		Vector3 s = new Vector3(3,3,3);
		transform.localScale -= s*3*Time.deltaTime;
		if(transform.localScale.x<0.5f){
			GameManager GM = Camera.main.GetComponent<GameManager>();
			GM.SpawnPlayer(1.0f);
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
				
		if(Input.GetKeyDown(KeyCode.Q)){
			ChangeColor(0);
		}
		if(Input.GetKeyDown(KeyCode.W)){
			ChangeColor(1);
		}
		if(Input.GetKeyDown(KeyCode.E)){
			ChangeColor(2);
		}
		
		if(tilecolor!=null)ChangePlayerState(tilecolor);
		if(Input.GetButtonDown("Horizontal")||Input.GetButtonDown("Vertical")){
			playerphysics.DetectWalls(ref Horizontal,ref Vertical,p,MoveStep,WallMask);
			Vector3 final = new Vector3(MoveStep*Horizontal,MoveStep*Vertical,0);
			transform.position += final;
		}
		if(playerphysics.levelcomplete){
			GameManager gm = Camera.main.GetComponent<GameManager>();
			gm.LevelComplete();
		}
		
	   if(Input.GetKeyDown(KeyCode.Escape)){
		 	if(Application.CanStreamedLevelBeLoaded("MainMenu")){
				Application.LoadLevel("MainMenu");
			}
	   }

		
	}
	
}
