using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUIWin : State<GUIManager> {
	private static GUIWin instance;
	public static GUIWin Instance (){
		if (instance == null)instance = new GUIWin();
		return instance;
	}

	private PanelWin	m_panelWin;
	
	public override void Enter (GUIManager Entity)
	{
		if(ConstValue.level == PlayerPrefs.GetInt(ConstValue.PRE_CUR_LEVEL, 1)){
			PlayerPrefs.SetInt(ConstValue.PRE_CUR_LEVEL, ConstValue.level+1);
		}
		Object panel = ResourceMgr.Instance().LoadRes(ConstValue.RES_GUI_PATH, ConstValue.GUI_WIN);
		GameObject winPanel = GameTools.AddChild(Entity.UIRoot, panel);
		m_panelWin = winPanel.GetComponent<PanelWin>();
		m_panelWin.Init();
	}
	
	public override void Exit (GUIManager Entity)
	{
		GameObject.Destroy(m_panelWin.gameObject);
		ResourceMgr.Instance().RemoveResByName(ConstValue.GUI_WIN);
	}

	public void NextBtnClick(){
		Target.GetFSM().ChangeState(GUILevel.Instance());
		Messenger.Broadcast(ConstValue.MSG_GAME_TO_LEVEL);
	}
}
