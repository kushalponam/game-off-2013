using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUIText[] timertext;
	// Use this for initialization
	void Start () {
	    GameObject obj = GameObject.Find("TimerText");
		timertext = obj.transform.GetComponentsInChildren<GUIText>();
	    foreach(GUIText t in timertext){
			t.guiText.material.color = Color.black;
			t.guiText.text = " ";
		}
	}
	public void UpdateText(string text){
		foreach(GUIText t in timertext){
			t.guiText.text = text;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
