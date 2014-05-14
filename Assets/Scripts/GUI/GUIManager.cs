using UnityEngine;
using System.Collections;

public class GUIManager : BaseGameEntity {
	public static GUIManager It;

	public GameObject UIRoot;

	StateMachine<GUIManager> m_StateMachine;

	void Awake(){
		It = this;
	}

	void Start(){
		// set id 
		SetID((int)EntityID.GUIManager);
		m_StateMachine = new StateMachine<GUIManager>(this);	
		m_StateMachine.SetCurrentState(GUILevel.Instance());	
		m_StateMachine.SetGlobalStateState(GUIGlobal.Instance());
	}

	void Update(){
		m_StateMachine.SMUpdate();
	}

	public StateMachine<GUIManager> GetFSM (){
		return m_StateMachine;
	}

	#region load Res
	public void LoadGameRes(){
		StartCoroutine("LoadGamePrefabs");
	}

	IEnumerator LoadGamePrefabs(){
		yield return null;
		for(int i=1; i<11; i++){
			ResourceMgr.Instance().LoadRes(ConstValue.RES_GAME_PATH, ConstValue.GAME_LIAN_PRE+i);
		}

		GameObject box = ResourceMgr.Instance().GetResFromName(ConstValue.GAME_LIAN_PRE+1) as GameObject;
		ConstValue.BoxWidth = box.GetComponent<SpriteRenderer>().bounds.size.x;
		ConstValue.BoxHeight = box.GetComponent<SpriteRenderer>().bounds.size.y;
		ResourceMgr.Instance().LoadRes(ConstValue.RES_GAME_PATH, ConstValue.GAME_LINE_HEN);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_GAME_PATH, ConstValue.GAME_LINE_SHU);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_GAME_PATH, ConstValue.GAME_BACKGROUND);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_GAME_PATH, ConstValue.GAME_B_BANNER);

		ResourceMgr.Instance().LoadRes(ConstValue.RES_PART_PATH, ConstValue.GAME_BOX_EXP);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_PART_PATH, ConstValue.GAME_BOMB_EXP);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_PART_PATH, ConstValue.GAME_ROCKET_TR);

		ResourceMgr.Instance().LoadRes(ConstValue.RES_PROP_PATH, ConstValue.GAME_PROP_BOMB);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_PROP_PATH, ConstValue.GAME_PROP_ROCKET);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_PROP_PATH, ConstValue.GAME_PROP_SHOCK);
		ResourceMgr.Instance().LoadRes(ConstValue.RES_PROP_PATH, ConstValue.GAME_PROP_SAME);

		GUILoadGame.Instance().is_load = true;
	}
	#endregion
}
