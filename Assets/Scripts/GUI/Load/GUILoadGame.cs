using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUILoadGame : GUILoadInterface {
	private static GUILoadGame instance;
	public static GUILoadGame Instance (){
		if (instance == null)instance = new GUILoadGame();
		return instance;
	}

	public override void Load(GUIManager gui){
		gui.LoadGameRes();
	}

	public override void LoadDone(GUIManager gui){
		Messenger.Broadcast(ConstValue.MSG_START_GAME);
		gui.GetFSM().ChangeState(GUIPlaying.Instance());
	}
}
