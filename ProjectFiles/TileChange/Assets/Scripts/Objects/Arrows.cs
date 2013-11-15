using UnityEngine;
using System.Collections;

public class Arrows : PlayerPhysics {
	
	public GameObject arrowobject;
	public LayerMask player;
	public	bool isStarted;
	// Use this for initialization
	public override void Start () {
      base.Start();
		isStarted=false;
	}
	
	IEnumerator ThrowArrow(){
		isStarted=true;
	yield return new WaitForSeconds(0.3f);
		Instantiate(arrowobject,new Vector3(transform.position.x,transform.position.y,7),Quaternion.Euler(new Vector3(0,180,0)));
		isStarted=false;
	yield return null;
	}
	
	public void DetectPlayer(){
		for(int i=0;i<3;i++){
		  float x = transform.position.x+c.x-s.x/2;
		  float y = (transform.position.y+c.y-s.y/2)+s.y/2*i;
		  ray = new Ray(new Vector3(x,y,transform.position.z), new Vector2(1,0));
		  Debug.DrawRay(ray.origin,ray.direction,Color.red);
			if(Physics.Raycast(ray,out hit,200,player)){
			 PlayerController pc = hit.collider.gameObject.GetComponent<PlayerController>();
			 if(pc.Disguise == PlayerController.state.Detected || pc.Disguise == PlayerController.state.Fall){
					if(!isStarted)StartCoroutine("ThrowArrow");
				}else{
					if(isStarted)StopCoroutine("ThrowArrow");
					isStarted=false;
				}
			}
   	    }
	}
	
	// Update is called once per frame
	public override void Update(){
		DetectPlayer();
		}

 }
