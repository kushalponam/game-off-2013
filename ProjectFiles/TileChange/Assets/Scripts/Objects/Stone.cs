using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
	
	
	public int xDirection;
	public int yDirection;
	
	private int speed;
	private Vector3 final;
	// Use this for initialization
	void Start () {
	
		speed=30;
		Invoke("Destroy",0.3f);
	}
	
	void OnTriggerEnter(Collider other){
		if(other.collider.tag=="Player"){
		  Destroy(other.collider.gameObject);
		  GameManager GM = Camera.main.GetComponent<GameManager>();
		  GM.PlayDieClip();
		  GM.SpawnPlayer(1.0f);
		  Destroy(this.gameObject);
		}
	}
	void Destroy(){
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	final = new Vector3((xDirection*speed)*Time.deltaTime,(yDirection*speed)*Time.deltaTime,0);
	transform.position += final;
		
	}
}
