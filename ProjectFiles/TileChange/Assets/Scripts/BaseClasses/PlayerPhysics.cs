using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	private BoxCollider bcollider;
	public Vector3 s;
	public Vector3 c;
	private bool Gotkey;
	public bool levelcomplete;
	private Vector3 colliderscale;
	
	
	protected Ray ray;
	protected RaycastHit hit;
	// Use this for initialization
	public virtual void Start () {
	    Gotkey=false;
		levelcomplete=false;
		bcollider = GetComponent<BoxCollider>();
		colliderscale = transform.localScale;
		s = new Vector3(bcollider.size.x*colliderscale.x,bcollider.size.y*colliderscale.y,bcollider.size.z*colliderscale.z);
		c = new Vector3(bcollider.center.x*colliderscale.x,bcollider.center.y*colliderscale.y,bcollider.center.z*colliderscale.z);
	}
	public void DetectWalls(ref float deltaX,ref float deltaY,Vector3 p,float Distance,LayerMask WallMask){
		     float dirx = deltaX;
		     float diry = deltaY;
			 float x = p.x+c.x+s.x/2*dirx;
		     float y = p.y+c.y+s.y/2*diry;
			 ray = new Ray(new Vector3(x,y,transform.position.z),new Vector2(dirx,diry)); 
		     Debug.DrawRay(ray.origin,ray.direction,Color.red);
		    if(Physics.Raycast(ray,out hit,Distance)){
				if(hit.collider.tag == "Gate" && Gotkey!=true){
					deltaX=0;
					deltaY=0;
				}
			}
		    if(Physics.Raycast(ray,out hit,Distance,WallMask)){
			   deltaX=0;
			   deltaY=0;
			}
	}
	public GameObject DetectTile(Vector3 p,float Distance,LayerMask Blocks){
		
	      float z = p.z+c.z+s.z/2;
		  ray = new Ray(new Vector3(transform.position.x,transform.position.y,z),new Vector3(0,0,1));
		  Debug.DrawRay(ray.origin,ray.direction,Color.red);  
		  if(Physics.Raycast(ray,out hit,Distance,Blocks)){
				return hit.collider.gameObject;
		  }
		return null;
	}
	
	public GameObject GetTheDoor(RaycastHit h){
		if(h.collider.gameObject.name == "Green" || h.collider.gameObject.name =="Brown"){
			return hit.collider.gameObject;
		}
		return null;
	}
	
	void OnTriggerEnter(Collider col){
		if(col.collider.gameObject.tag=="Key"){
			Gotkey=true;
			Destroy(col.collider.gameObject);
		}
		if(col.collider.gameObject.tag == "DuplicateKey"){
			Destroy(col.collider.gameObject);
		}
		if(col.collider.gameObject.tag == "Gate"){
			levelcomplete=true;
			Debug.Log("Level Cleared");
		}
	}
	public virtual void Update(){
	}
	
}
