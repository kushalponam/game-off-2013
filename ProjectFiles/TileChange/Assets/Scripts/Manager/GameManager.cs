using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject[] Blocks;
	public int ChangeStep;
	private GameObject playerspawn;
	private GameObject KeySpawn;
	public int currentTime;
	public int targetTime;
	public int changetime;
	
	private float rot;
	private GUIManager gui;
	// Use this for initialization
	void Start () {
	    rot =90;
		playerspawn = GameObject.Find("PlayerSpawn");
		KeySpawn = GameObject.Find("KeySpawn");
		gui = GetComponent<GUIManager>();
		Blocks = GameObject.FindGameObjectsWithTag("Blocks");
	    targetTime = (int)(Time.timeSinceLevelLoad)+ChangeStep;
		SpawnPlayer(0);
	}
	
	public void SpawnPlayer(float waittime){
		Invoke("spawningplayer",waittime);
		if(GameObject.FindGameObjectWithTag("Key")==null) Invoke("SpawnKey",waittime);
	}
	
	void SpawnKey(){
		Instantiate(Resources.Load("Prefab/Key"),KeySpawn.transform.position,Quaternion.identity);
	}
	void spawningplayer(){
		Instantiate(Resources.Load("Prefab/Player"),playerspawn.transform.position,Quaternion.Euler(new Vector3(0,0,180)));
		
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
	
	// Update is called once per frame
	void Update () {
	RotateBlocks();
	ChangeFeedBack();	
	
	}
}