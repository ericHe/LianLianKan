using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUILevel : State<GUIManager> {
	private static GUILevel instance;
	public static GUILevel Instance (){
		if (instance == null)instance = new GUILevel();
		return instance;
	}

	private PanelLevel		m_panelLevel;
	
	public override void Enter (GUIManager Entity)
	{
		Object panel = ResourceMgr.Instance().LoadRes(ConstValue.RES_GUI_PATH, ConstValue.GUI_LEVEL);
		GameObject levelPanel = GameTools.AddChild(Entity.UIRoot, panel);
		m_panelLevel = levelPanel.GetComponent<PanelLevel>();
		m_panelLevel.Init();
	}
	
	public override void Execute (GUIManager Entity)
	{
		//base.Execute (Entity);	
	}
	
	public override void Exit (GUIManager Entity)
	{
		GameObject.Destroy(m_panelLevel.gameObject);
		ResourceMgr.Instance().RemoveResByName(ConstValue.GUI_LEVEL);
	}

	/// <summary>
	/// 点击关卡按钮时执行
	/// </summary>
	/// <param name="go">Go.</param>
	public void LevelClick(GameObject go){
		ConstValue.level = int.Parse(go.name);
		GUILoad.Instance().SetLoad(GUILoadGame.Instance());
		GUIManager.It.GetFSM().ChangeState(GUILoad.Instance());

		//Debug.Log(ConstValue.level);
//		if(GameManager.It.GetFSM().CurrentState() == GameStart.Instance()){
//			Messenger.Broadcast<int>(ConstValue.MSG_START_GAME, ConstValue.level);
//		}
	}
}
