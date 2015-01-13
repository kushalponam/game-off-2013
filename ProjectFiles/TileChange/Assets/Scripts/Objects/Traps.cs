using UnityEngine;
using System.Collections;

public class Traps : MonoBehaviour {
	
	private GameManager GM;
	public float TileTime;
	private GameObject doors;
	private bool IsStarted=false;
	void Start(){
		IsStarted=false;
		doors = transform.FindChild("Doors").gameObject;
	    Invoke("GetTiming",0.6f);
	}
	void GetTiming(){
		GM=Camera.main.GetComponent<GameManager>();
		TileTime = GM.TileTime;
	
	}
	
	public void Resize(){
		if(doors.animation.isPlaying){
		doors.animation.Stop();
		}
		doors.animation.Play("Resize");	
	}
	public void Shake(){
		if(doors.animation.isPlaying){
		doors.animation.Stop();
		}
		doors.animation.Play("Shake");
	}
	public void Fall(){
		if(doors.animation.isPlaying){
		doors.animation.Stop();
		}
		doors.animation.Play("Fall");	
		Invoke("Resize",doors.animation.clip.length);
	}
    
	IEnumerator FallIntoHole(GameObject g){
		IsStarted=true;
		Shake();
		yield return new WaitForSeconds(TileTime);
	    if(g!=null){
		GM.PlayDieClip();
		PlayerController PC = g.GetComponent<PlayerController>();
		if(PC!=null)PC.cantmove=true;
		Fall();
		PC.InvokeRepeating("ReduceSize",0,Time.deltaTime);
        IsStarted=false;
		}else{
			StopCoroutine("FallIntoHole");
			IsStarted=false;
		}
		yield return null;
	}
	
	
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag=="Player"){
		if(!IsStarted)StartCoroutine("FallIntoHole",col.gameObject);
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.collider.gameObject.tag=="Player"){
			doors.animation.Stop();
			PlayerController PC = col.gameObject.GetComponent<PlayerController>();
			PC.cantmove=false;
			IsStarted=false;
			StopCoroutine("FallIntoHole");
		}
	}
	
	
}
