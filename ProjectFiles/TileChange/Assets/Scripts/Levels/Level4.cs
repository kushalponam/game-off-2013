using UnityEngine;
using System.Collections;

public class Level4 : GameManager {

	// Use this for initialization
	public override void Start () {
	
		 RotationRequired=false;
		 TimerFeedback=false;
		 AddDuplicateKey=false;
		 AddKeyAtStart = false;
		 base.Start();
	}
	
    public override void ChangeArrowTime ()
	{
		ArrowTime=0.6f;
		TileTime=0.4f;
		base.ChangeArrowTime();
	}
	public override void LevelComplete ()
	{
		base.LevelComplete ();
		if(Application.CanStreamedLevelBeLoaded("Won")){
			Application.LoadLevel("Won");
		}
	}
	
	public override void ChangePlayerSettings ()
	{
		PlayerMoveStep=10;
		ColorsToChange=3;
		base.ChangePlayerSettings ();
	}
}
