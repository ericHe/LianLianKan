using UnityEngine;
using System.Collections;
using Jun.Message;

public class GUILoad : State<GUIManager> {
	private static GUILoad instance;
	public static GUILoad Instance (){
		if (instance == null)instance = new GUILoad();
		return instance;
	}

	private GUILoadInterface	m_load;
	private GameObject			m_panel;

	public override void Enter (GUIManager Entity)
	{
		Object panel = ResourceMgr.Instance().LoadRes(ConstValue.RES_GUI_PATH, ConstValue.GUI_LOAD);
		m_panel = GameTools.AddChild(Entity.UIRoot, panel);

		if(m_load != null)
			m_load.Load(Entity);
	}
	
	public override void Execute (GUIManager Entity)
	{
		if(m_load.is_load){
			m_load.LoadDone(Entity);
		}
	}
	
	public override void Exit (GUIManager Entity)
	{
		GameObject.Destroy(m_panel);
		ResourceMgr.Instance().RemoveResByName(ConstValue.GUI_LOAD);
	}

	public void SetLoad(GUILoadInterface load){
		m_load = load;
	}

}
