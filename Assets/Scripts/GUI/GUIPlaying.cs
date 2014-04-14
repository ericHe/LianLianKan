using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUIPlaying : State<GUIManager> {
	private static GUIPlaying instance;
	public static GUIPlaying Instance (){
		if (instance == null)instance = new GUIPlaying();
		return instance;
	}

	private PanelPlaying m_panelPlaying;

	public override void Enter (GUIManager Entity)
	{
		Messenger.AddListener<GameDoneState>(ConstValue.MSG_GAME_DONE, GameDone);

		Object panel = ResourceMgr.Instance().LoadRes(ConstValue.RES_GUI_PATH, ConstValue.GUI_PLAYING);
		GameObject gamePanel = GameTools.AddChild(Entity.UIRoot, panel);
		m_panelPlaying = gamePanel.GetComponent<PanelPlaying>();
	}
	
	public override void Execute (GUIManager Entity)
	{

	}
	
	public override void Exit (GUIManager Entity)
	{
		Messenger.RemoveListener<GameDoneState>(ConstValue.MSG_GAME_DONE, GameDone);

		GameObject.Destroy(m_panelPlaying.gameObject);
		ResourceMgr.Instance().RemoveResByName(ConstValue.GUI_PLAYING);
	}

	void GameDone(GameDoneState state){
		switch(state){
		case GameDoneState.GameOver:
			Target.GetFSM().ChangeState(GUIOver.Instance());
			break;
		case GameDoneState.GameWin:
			Target.GetFSM().ChangeState(GUIWin.Instance());
			break;
		}
	}

	public void UseProp(GamePropsId propId){
		Messenger.Broadcast(ConstValue.MSG_USE_PROP, propId);
	}
}
