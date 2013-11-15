using UnityEngine;
using System.Collections;

public class KillArrow : MonoBehaviour {
	
	public float speed;
	// Use this for initialization
	void Start () {
	speed = 200;
	Invoke("DestroyItself",2.0f);
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
	
	// Update is called once per frame
	void Update () {
		Vector3 final = new Vector3(speed*Time.deltaTime,0);
		transform.position += final;
	}
}
