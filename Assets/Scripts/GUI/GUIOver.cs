using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUIOver : State<GUIManager> {
	private static GUIOver instance;
	public static GUIOver Instance (){
		if (instance == null)instance = new GUIOver();
		return instance;
	}

	private PanelOver	m_panelOver;

	public override void Enter (GUIManager Entity)
	{
		Object panel = ResourceMgr.Instance().LoadRes(ConstValue.RES_GUI_PATH, ConstValue.GUI_OVER);
		GameObject overPanel = GameTools.AddChild(Entity.UIRoot, panel);
		m_panelOver = overPanel.GetComponent<PanelOver>();
		m_panelOver.Init();
	}
	
	public override void Exit (GUIManager Entity)
	{
		GameObject.Destroy(m_panelOver.gameObject);
		ResourceMgr.Instance().RemoveResByName(ConstValue.GUI_OVER);
	}

	public void RetryBtnClick(){
		Target.GetFSM().ChangeState(GUILevel.Instance());
		Messenger.Broadcast(ConstValue.MSG_GAME_TO_LEVEL);
	}
}
