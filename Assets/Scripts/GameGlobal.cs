using UnityEngine;
using System.Collections;

public class GameGlobal : State<GameManager> {
	private static GameGlobal instance;
	public static GameGlobal Instance (){
		if (instance == null)instance = new GameGlobal();
		return instance;
	}
	
	public override void Enter (GameManager Entity)
	{
		//base.Enter (Entity);
	}
	
	public override void Execute (GameManager Entity)
	{
		//base.Execute (Entity);	
	}
	
	public override void Exit (GameManager Entity)
	{
		//base.Exit (Entity);
	}
}
