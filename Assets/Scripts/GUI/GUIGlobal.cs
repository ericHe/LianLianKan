using UnityEngine;
using System.Collections;

public class GUIGlobal : State<GUIManager> {
	private static GUIGlobal instance;
	public static GUIGlobal Instance (){
		if (instance == null)instance = new GUIGlobal();
		return instance;
	}

	public override void Enter (GUIManager Entity)
	{
		//base.Enter (Entity);
	}
	
	public override void Execute (GUIManager Entity)
	{
		//base.Execute (Entity);	
	}
	
	public override void Exit (GUIManager Entity)
	{
		//base.Exit (Entity);
	}
}
