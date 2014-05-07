using UnityEngine;
using System.Collections;
using Jun.Message;

public class GameStart : State<GameManager> {
	private static GameStart instance;
	public static GameStart Instance (){
		if (instance == null)instance = new GameStart();
		return instance;
	}

	public override void Enter (GameManager Entity)
	{
		Messenger.AddListener(ConstValue.MSG_START_GAME, StartGame);
	}
	
	public override void Execute (GameManager Entity)
	{

	}
	
	public override void Exit (GameManager Entity)
	{
		Messenger.RemoveListener(ConstValue.MSG_START_GAME, StartGame);
	}

	void StartGame(){
		Debug.Log("start " + ConstValue.level);
		Target.GetFSM().ChangeState(GamePlaying.Instance());
	}
}
