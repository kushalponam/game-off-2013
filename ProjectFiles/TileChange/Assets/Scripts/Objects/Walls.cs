using UnityEngine;
using System.Collections;

public class Walls : MonoBehaviour {
	
	public GameObject Blocks;
	public Vector3 SignVector;
	public float rotation;
	public bool MoveXback;
	public bool MoveXFront;
	public bool MoveYBack;
	public bool MoveYFront;
	// Use this for initialization
	void Start () {
		Blocks = transform.parent.gameObject;
		SignVector = new Vector3(Mathf.Sign(transform.localPosition.x),Mathf.Sign(transform.localPosition.y),0);
	}
	
	void OnTriggerEnter(Collider col){
		if(col.collider.gameObject.tag=="Player"){
			PlayerController pc = col.collider.GetComponent<PlayerController>();
			if(pc.CanBePushed){
				if(MoveXback){
					col.gameObject.transform.position += new Vector3(pc.MoveStep,0,0);
					pc.CanBePushed=false;
				return;
				}else if(MoveXFront){
					col.gameObject.transform.position += new Vector3(pc.MoveStep*-1,0,0);
					pc.CanBePushed=false;
				return;
				}else if(MoveYBack){
					col.gameObject.transform.position += new Vector3(0,pc.MoveStep,0);
					pc.CanBePushed=false;
				return;
				}else if(MoveYFront){
					col.gameObject.transform.position += new Vector3(0,pc.MoveStep*-1,0);
					pc.CanBePushed=false;
				return;
				}
			}
		}
	}
	void OnTriggerExit(Collider col){
		if(col.collider.gameObject.tag=="Player"){
			PlayerController pc = col.collider.GetComponent<PlayerController>();
			pc.CanBePushed=true;
		}
	}
	void PushChameleon(Vector3 Sign){
       if(Sign.x==-1 && Sign.y==1){
			 MoveXback  =(rotation>0)&&(rotation<90)?true:false;
			 MoveYBack  =(rotation>90)&&(rotation<180)?true:false;
			 MoveXFront =(rotation>180)&&(rotation<270)?true:false;
			 MoveYFront =(rotation>270)?true:false;
			return;
		}else if(Sign.x== 1 && Sign.y== 1){
			MoveYFront  =(rotation>0)&&(rotation<90)?true:false;
			MoveXback   =(rotation>90)&&(rotation<180)?true:false;
			MoveYBack   =(rotation>180)&&(rotation<270)?true:false;
			MoveXFront  =(rotation>270)?true:false;
			return;
		}else if(Sign.x==-1 && Sign.y==-1){
			MoveYBack   =(rotation>0)&&(rotation<90)?true:false;
			MoveXFront  =(rotation>90)&&(rotation<180)?true:false;
			MoveYFront  =(rotation>180)&&(rotation<270)?true:false;
			MoveXback   =(rotation>270)?true:false;
			return;
		}else if(Sign.x==1 && Sign.y==-1){
			MoveXFront  =(rotation>0)&&(rotation<90)?true:false;
			MoveYFront  =(rotation>90)&&(rotation<180)?true:false;
			MoveXback   =(rotation>180)&&(rotation<270)?true:false;
			MoveYBack   =(rotation>270)?true:false;
			return;
		}
	}
	// Update is called once per frame
	void Update () {
		rotation = Blocks.transform.rotation.eulerAngles.z;
		PushChameleon(SignVector);
	}
}
