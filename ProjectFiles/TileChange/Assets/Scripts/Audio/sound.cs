using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]



public class sound : MonoBehaviour {
	
    
	public AudioClip Level1Sound;
	public AudioClip Level2Sound;
	public AudioClip Level3Sound;
	public AudioClip Level4Sound;
	// Use this for initialization
	void Start () {
	Invoke("PlaySound",1.0f);	
	
	}
   
	void OnLevelWasLoaded(int Level){
		
		switch(Level){
		case 0:
			Destroy(gameObject);
			break;
		case 2:
			if(audio.clip == Level1Sound) break;
		    audio.clip = Level1Sound;
			Invoke("PlaySound",1.0f);	
			break;
		case 5:
			if(audio.clip == Level2Sound) break;
			 audio.clip = Level2Sound;
			Invoke("PlaySound",1.0f);	
			break;
		case 7:
			if(audio.clip == Level3Sound) break;
			 audio.clip = Level3Sound;
			audio.volume = 0.3f;
			Invoke("PlaySound",1.0f);	
			break;
		case 9:
			if(audio.clip == Level4Sound) break;
			audio.clip = Level4Sound;
			Invoke("PlaySound",1.0f);	
			break;
		case 11:
			if(audio.clip == Level1Sound) break;
		    audio.clip = Level1Sound;
			PlaySound();
			break;
		case 12:
			if(audio.clip == Level1Sound) break;
		    audio.clip = Level1Sound;
			PlaySound();
			break;
		default:
			break;
		}
	}
	
	void FadeOut(){
		
	}
	
	void PlaySound(){
		audio.Play();
	}
	// Update is called once per frame
	void Update () {
	
		DontDestroyOnLoad(gameObject);
	}
}
