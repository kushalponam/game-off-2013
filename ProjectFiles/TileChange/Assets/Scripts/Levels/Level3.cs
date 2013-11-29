using UnityEngine;
using System.Collections;

public class Level3 : GameManager {

	// Use this for initialization
	public override void Start () {
	     RotationRequired=true;
		 TimerFeedback=false;
		 AddDuplicateKey=true;
		 AddKeyAtStart=true; 
		 base.Start();
		 ChangeStep = 3;
	}
	 
	public override void ChangeArrowTime ()
	{
		ArrowTime=0.7f;
		TileTime=0.5f;
		base.ChangeArrowTime();
	}
	public override void LevelComplete ()
	{
		base.LevelComplete ();
		if(Application.CanStreamedLevelBeLoaded("HLevel4")){
			Application.LoadLevel("HLevel4");
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
	base.Update();
	}
}
