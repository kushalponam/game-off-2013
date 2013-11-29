using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	
	
	public Texture[] GreenTextures;
	public Texture[] BlueTextures;
	public Texture[] PurpleTexture;
	public Texture[] yellowTexture;
	// Use this for initialization
	void Start () {
//	StartCoroutine("Animate");
	
	}
	
	int t=0;
	IEnumerator Animate(){
	//renderer.material.SetTexture("_MainTex",GreenTextures[t]);
	Begin:
		renderer.material.SetTexture("_MainTex",GreenTextures[t]);
	   /* if(Input.GetKeyDown(KeyCode.Q)){
		renderer.material.SetTexture("_MainTex",yellowTexture[t]);
		}else if(Input.GetKeyDown(KeyCode.W)){
			renderer.material.SetTexture("_MainTex",PurpleTexture[t]);
		}else if(Input.GetKeyDown(KeyCode.E)){
			renderer.material.SetTexture("_MainTex",BlueTextures[t]);
		}*/
		yield return new WaitForSeconds(0.2f);
		t++;
	    if(t>=2) t=0;
	goto Begin;
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
