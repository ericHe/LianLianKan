using UnityEngine;
using System.Collections;

public class GameManager : BaseGameEntity {
	public static GameManager It;
	StateMachine<GameManager> m_StateMachine;

	void Awake() {
		It = this;
	}

	void Start(){
		// set id 
		SetID((int)EntityID.GameManager);
		m_StateMachine = new StateMachine<GameManager>(this);	
		m_StateMachine.SetCurrentState(GameStart.Instance());	
		m_StateMachine.SetGlobalStateState(GameGlobal.Instance());
	}
	
	void Update(){
		m_StateMachine.SMUpdate();
	}

	void LateUpdate(){
		m_StateMachine.SMLateUpdate();
	}

	void FixedUpdate(){
		m_StateMachine.SMFixedUpdate();
	}
	
	public StateMachine<GameManager> GetFSM (){
		return m_StateMachine;
	}

}
