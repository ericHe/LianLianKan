using UnityEngine;
using System.Collections;

public class GUILoadMenu : GUILoadInterface {
	private static GUILoadMenu instance;
	public static GUILoadMenu Instance (){
		if (instance == null)instance = new GUILoadMenu();
		return instance;
	}

	public override void Load(GUIManager gui){
		
	}

	public override void LoadDone(GUIManager gui){

	}
}
