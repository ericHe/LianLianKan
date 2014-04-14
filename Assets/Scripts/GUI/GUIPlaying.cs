using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUIPlaying : State<GUIManager> {
	private static GUIPlaying instance;
	public static GUIPlaying Instance (){
		if (instance == null)instance = new GUIPlaying();
		return instance;
	}

	public override void Enter (GUIManager Entity)
	{
		Messenger.AddListener<int>(ConstValue.MSG_GAME_DONE, GameDone);
	}
	
	public override void Execute (GUIManager Entity)
	{

	}
	
	public override void Exit (GUIManager Entity)
	{
		Messenger.RemoveListener<int>(ConstValue.MSG_GAME_DONE, GameDone);
	}

	void GameDone(int state){
		switch(state){
		case (int)GameDoneState.GameOver:
			Target.GetFSM().ChangeState(GUIOver.Instance());
			break;
		case (int)GameDoneState.GameWin:
			Target.GetFSM().ChangeState(GUIWin.Instance());
			break;
		}
	}
}
