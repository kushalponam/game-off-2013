using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public AudioClip DieClip;
	public AudioClip keyClip;
	
	
	public GameObject[] Blocks;
	public int ChangeStep;
	private GameObject playerspawn;
	[HideInInspector]
	public GameObject KeySpawn;
	private GameObject DuplicateKey;
	protected int currentTime;
	protected int targetTime;
	protected int changetime;
    
	protected bool AddDuplicateKey;
	[HideInInspector]
	public bool AddKeyAtStart;
	
	public Arrows[] arrows;
	protected float PlayerMoveStep;
	protected bool RotationRequired;
	protected bool TimerFeedback;
	protected int ColorsToChange;
	protected float ArrowTime;
	public float TileTime;
	
	protected float rot;
	private GUIManager gui;
	protected PlayerController pc;
	// Use this for initialization
	public virtual void Start () {
	    rot=90;
		ChangeStep = 3;
		PlayerMoveStep = 15;
		ColorsToChange=4;
		playerspawn = GameObject.Find("PlayerSpawn");
		KeySpawn = GameObject.Find("KeySpawn");
		if(AddDuplicateKey) DuplicateKey = GameObject.Find("DuplicateKey");
		if(TimerFeedback)gui = GetComponent<GUIManager>();
		Blocks = GameObject.FindGameObjectsWithTag("Blocks");
	    targetTime = (int)(Time.timeSinceLevelLoad)+ChangeStep;
		arrows = GameObject.Find("Arrow").GetComponentsInChildren<Arrows>();
		ChangeArrowTime();
		SpawnPlayer(0);
	}
	public virtual void ChangePlayerSettings(){
		pc.MoveStep = PlayerMoveStep;
		pc.NumberofColors = ColorsToChange;
	    pc.TileTime = TileTime;
	}
	public virtual void ChangeArrowTime(){
		foreach(Arrows a in arrows){
			a.ArrowDetectTime = ArrowTime;
		}
	}
	
	public virtual void LevelComplete(){
	}
	public void SpawnPlayer(float waittime){
		Invoke("spawningplayer",waittime);
		if(GameObject.FindGameObjectWithTag("Key")==null)Invoke("SpawnKey",waittime);
	}
	
	public void SpawnKey(){
		if(AddKeyAtStart)Instantiate(Resources.Load("Prefab/Key"),KeySpawn.transform.position,Quaternion.identity);
		if(AddDuplicateKey)Instantiate(Resources.Load("Prefab/DuplicateKey"),DuplicateKey.transform.position,Quaternion.identity);
	}
	public void PlayDieClip(){
		audio.clip= DieClip;
		audio.Play();
	}
	
	public void PlayKeyClip(){
		audio.clip = keyClip;
		audio.Play();
	}
	
	void spawningplayer(){
		Instantiate(Resources.Load("Prefab/Player"),playerspawn.transform.position,Quaternion.Euler(new Vector3(0,0,180)));
		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		ChangePlayerSettings();
	}
	void RotateBlocks(){
		currentTime = (int)Time.timeSinceLevelLoad;
		if(currentTime>=targetTime){
	    changetime=targetTime+1; 
		foreach(GameObject g in Blocks){
				g.transform.rotation = Quaternion.RotateTowards(g.transform.rotation,Quaternion.Euler(new Vector3(0,0,rot)),300*Time.deltaTime);
			}
		if(currentTime>changetime){
				targetTime = changetime+ChangeStep;
				rot +=90;
			}
		}	
	}
	void ChangeFeedBack(){
	if(gui!=null){
		if(targetTime-currentTime<=2 && targetTime-currentTime>=0){
			if(targetTime-currentTime == 0){
				gui.UpdateText("Change");
					return;
			}else{
				gui.UpdateText((targetTime-currentTime).ToString());
			}
		}else
			{
				gui.UpdateText(" ");
			}
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
	if(RotationRequired)RotateBlocks();
	if(TimerFeedback)ChangeFeedBack();	
	
	}
}