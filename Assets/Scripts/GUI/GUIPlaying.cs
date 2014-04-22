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
		Messenger.AddListener<GamePropsId>(ConstValue.MSG_USE_PROP_SUC, UsePropSuc);
		GameStaticData.Combo = 0;

		Object panel = ResourceMgr.Instance().LoadRes(ConstValue.RES_GUI_PATH, ConstValue.GUI_PLAYING);
		GameObject gamePanel = GameTools.AddChild(Entity.UIRoot, panel);
		m_panelPlaying = gamePanel.GetComponent<PanelPlaying>();
		m_panelPlaying.Init();
	}
	
	public override void Execute (GUIManager Entity)
	{

	}
	
	public override void Exit (GUIManager Entity)
	{
		Messenger.RemoveListener<GameDoneState>(ConstValue.MSG_GAME_DONE, GameDone);
		Messenger.RemoveListener<GamePropsId>(ConstValue.MSG_USE_PROP_SUC, UsePropSuc);

		GameObject.Destroy(m_panelPlaying.gameObject);
		m_panelPlaying = null;
		ResourceMgr.Instance().RemoveResByName(ConstValue.GUI_PLAYING);
	}

	#region msg
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

	void UsePropSuc(GamePropsId prop){
		m_panelPlaying.UsePropSuc(prop);
	}
	#endregion

	public void UseProp(GamePropsId propId){
		Messenger.Broadcast(ConstValue.MSG_USE_PROP, propId);
	}

	public void AddPropNum(GamePropsId prop, int num = 1){
		if(m_panelPlaying != null)
			m_panelPlaying.AddPropNum(prop, num);
	}

	public void ChangeComboNum(){
		if(m_panelPlaying != null)
			m_panelPlaying.ChangeComboNum();
	}

}
