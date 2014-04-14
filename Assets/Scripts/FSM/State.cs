using UnityEngine;
using System.Collections;

//C# fan xing yun xing shi hou zhi dao shi ju ti na ge lei 
public class State<entity_type>
{
	
	public entity_type Target ;
	//Enter state  
	public virtual void Enter (entity_type entityType)
	{
		
	}

	//Execute state
	public virtual void Execute (entity_type entityType)
	{
		
	}

	public virtual void FixedExecute (entity_type entityType)
	{
		
	}

	public virtual void LateExecute (entity_type entityType)
	{
		
	}

	//Exit state
	public virtual void Exit (entity_type entityType)
	{
		
	}
}


