using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseGameEntity : MonoBehaviour
{

	private int m_ID;
	
	private static ArrayList m_idArray = new ArrayList();
	public int ID ()
	{
		return m_ID;
	}

	protected void SetID (int val)
	{
		if (m_idArray.Contains(val)) {
			Debug.LogError ("id cuo wu ");
			return;
		}
		m_idArray.Add(val);
		m_ID = val;
	}
	
}
