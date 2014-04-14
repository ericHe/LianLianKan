using UnityEngine;
using System.Collections;

public class StateMachine<entity_type>
{
	//entity shi ti 
	private entity_type m_pOwner;

	private State<entity_type> m_pCurrentState;
	private State<entity_type> m_pPreviousState;
	private State<entity_type> m_pGlobalState;

	public StateMachine (entity_type owner)
	{
		m_pOwner = owner;
		m_pCurrentState = null;
		m_pPreviousState = null;
		m_pGlobalState = null;
	}
	
	public void GlobalStateEnter()
	{
		m_pGlobalState.Enter(m_pOwner);
	}
	
	public void SetGlobalStateState(State<entity_type> GlobalState)
	{
		m_pGlobalState = GlobalState;
		m_pGlobalState.Target = m_pOwner;
		m_pGlobalState.Enter(m_pOwner);
	}
	
	public void SetCurrentState(State<entity_type> CurrentState)
	{
		m_pCurrentState = CurrentState;
		m_pCurrentState.Target = m_pOwner;
		m_pCurrentState.Enter(m_pOwner);
	}
	public void SMUpdate ()
	{
		//quan ju zhuang tai cun zai yun xing quan ju zhuang tai de Execute fang fa
		if (m_pGlobalState != null)
			m_pGlobalState.Execute (m_pOwner);
		
		//dang qian zhuang tai cun zai yun xing dang qian zhuang tai de Execute fang fa 
		if (m_pCurrentState != null)
			m_pCurrentState.Execute (m_pOwner);
	}

	public void SMFixedUpdate(){
		if (m_pGlobalState != null)
			m_pGlobalState.FixedExecute (m_pOwner);

		if (m_pCurrentState != null)
			m_pCurrentState.FixedExecute (m_pOwner);
	}

	public void SMLateUpdate(){
		//quan ju zhuang tai cun zai yun xing quan ju zhuang tai de Execute fang fa
		if (m_pGlobalState != null)
			m_pGlobalState.LateExecute (m_pOwner);
		
		//dang qian zhuang tai cun zai yun xing dang qian zhuang tai de Execute fang fa 
		if (m_pCurrentState != null)
			m_pCurrentState.LateExecute (m_pOwner);
	}

	public void ChangeState (State<entity_type> pNewState)
	{
		if (pNewState == null) {
			//mei you state yin wei shi kong de da yin cuo wu 
			Debug.LogError ("bu cun zai de zhuang tai");
		}

		Debug.Log(pNewState);
		
		m_pCurrentState.Exit(m_pOwner);
		//bao cun zhi qian de zhuang tai 
		m_pPreviousState = m_pCurrentState;
		
		//she zhi dang qian zhuang tai wei pNewState
		m_pCurrentState = pNewState;
		m_pCurrentState.Target = m_pOwner;
		//jin tu dang qian zhuang dai Enter
		m_pCurrentState.Enter (m_pOwner);
	}

	public void RevertToPreviousState ()
	{
		//qie huan dao qian yi ge zhuang tai 
		ChangeState (m_pPreviousState);
		
	}

	public State<entity_type> CurrentState ()
	{
		//fan hui dang qian zhuang tai 
		return m_pCurrentState;
	}
	public State<entity_type> GlobalState ()
	{
		//fan hui quan ju zhuang tai 
		return m_pGlobalState;
	}
	public State<entity_type> PreviousState ()
	{
		//fan hui qian yi ge zhuang tai 
		return m_pPreviousState;
	}
	
}
