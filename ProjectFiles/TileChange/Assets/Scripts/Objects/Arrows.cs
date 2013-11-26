using UnityEngine;
using System.Collections;

public class Arrows : PlayerPhysics {
	
	public float ArrowDetectTime;
	public GameObject arrowobject;
	public LayerMask player;
	public	bool isStarted;
	public bool UpSideArrow;
	private float x,y,xpos,ypos;
	
	public GameObject g;
	// Use this for initialization
	public override void Start () {
        base.Start();
		isStarted=false;
	}
	
	IEnumerator ThrowArrow(){
		isStarted=true;
	yield return new WaitForSeconds(ArrowDetectTime);
		 if(UpSideArrow){
	     g = Instantiate(arrowobject,new Vector3(xpos,ypos,7),Quaternion.Euler(new Vector3(0,180,90))) as GameObject;
		 KillArrow k = g.GetComponent<KillArrow>();
		 k.YDirection=true;
		 //isStarted=false;
		}else
		{
		 Instantiate(arrowobject,new Vector3(xpos,ypos,7),Quaternion.Euler(new Vector3(0,180,0)));
		}
	isStarted=false;
	yield return null;
	}
	
	
	public void DetectPlayer(){
		for(int i=0;i<3;i++){
		if(UpSideArrow){
		  x = (transform.position.x+c.x-s.x)+s.x*i;
		  y = (transform.position.y+c.y-s.y/4);
		ray = new Ray(new Vector3(x,y,transform.position.z), new Vector2(0,-1));
		}else{
		  x = transform.position.x+c.x+s.x/3;
		  y = (transform.position.y+c.y-s.y/3)+s.y/3*i;
		ray = new Ray(new Vector3(x,y,transform.position.z), new Vector2(1,0));
		}
		  Debug.DrawRay(ray.origin,ray.direction,Color.red);
			if(Physics.Raycast(ray,out hit,200,player)){
			 PlayerController pc = hit.collider.gameObject.GetComponent<PlayerController>();
			 if(pc.Disguise == PlayerController.state.Detected){
					if(UpSideArrow){
						 xpos = hit.collider.gameObject.transform.localPosition.x;
						 ypos = transform.position.y;
					}else{
					 xpos = transform.position.x;
					 ypos = hit.collider.gameObject.transform.localPosition.y;
					}
				
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
