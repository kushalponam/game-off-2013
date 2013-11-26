using UnityEngine;
using System.Collections;

public class Level2 : GameManager {
// Use this for initialization
	public override void Start () {
		 RotationRequired=false;
		 TimerFeedback=false;
		AddDuplicateKey=true;
		 AddKeyAtStart=true;
		 base.Start();
	}
	 
	public override void ChangeArrowTime ()
	{
		ArrowTime=0.6f;
		TileTime=0.5f;
		base.ChangeArrowTime();
	}
	public override void LevelComplete ()
	{
		base.LevelComplete ();
		if(Application.CanStreamedLevelBeLoaded("ChangeLevel3")){
			Application.LoadLevel("ChangeLevel3");
		}
	}
	
	public override void ChangePlayerSettings ()
	{
		PlayerMoveStep=10;
		ColorsToChange=3;
		base.ChangePlayerSettings ();
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}
}
