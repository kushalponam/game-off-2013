using UnityEngine;
using System.Collections;

public class Level1 : GameManager {

	// Use this for initialization
	public override void Start () {
		RotationRequired=false;
	    TimerFeedback=false;
		 AddKeyAtStart=true;
		base.Start();
	}
	
	public override void ChangeArrowTime ()
	{
		ArrowTime=0.6f;
		base.ChangeArrowTime ();
	}
	
	public override void LevelComplete ()
	{
		base.LevelComplete ();
		if(Application.CanStreamedLevelBeLoaded("HLevel2")){
			Application.LoadLevel("HLevel2");
		}
	}
	
	public override void ChangePlayerSettings ()
	{
		PlayerMoveStep =10;
		ColorsToChange=4;
		TileTime = 0.6f;
		base.ChangePlayerSettings ();
		
	}
	
	// Update is called once per frame
	public override void Update () {
	}
}
