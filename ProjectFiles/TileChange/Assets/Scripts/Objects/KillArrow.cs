using UnityEngine;
using System.Collections;

public class KillArrow : MonoBehaviour {
	
	public float speed;
	public bool YDirection;
	// Use this for initialization
	void Start () {
	speed = 100;
	Invoke("DestroyItself",1.0f);
	}
	void DestroyItself(){
		Destroy(this.gameObject);
	}
	void OnTriggerEnter(Collider other){
		if(other.collider.tag=="Player"){
		if(other.collider.gameObject.GetComponent<PlayerController>().Disguise==PlayerController.state.Idle || other.collider.gameObject.GetComponent<PlayerController>().Disguise==PlayerController.state.Blended ) return;
		  Destroy(other.collider.gameObject);
		  GameManager GM = Camera.main.GetComponent<GameManager>();
		  GM.SpawnPlayer(0.5f);
		  Destroy(this.gameObject);
		}
	}
	
	Vector3 final;
	// Update is called once per frame
	void Update () {
		if(YDirection){
		final = new Vector3(0,-speed*Time.deltaTime);
		}else{
		final = new Vector3(speed*Time.deltaTime,0);	
		}
			
		transform.position += final;
	}
}
