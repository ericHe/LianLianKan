using UnityEngine;
using System.Collections;

public class GameStaticData{

	static int combo = 0;
	public static int Combo {
		get { return combo; }
		set { 
			combo = value; 
			GUIPlaying.Instance().ChangeComboNum();
			if(combo != 0 && combo%3 == 0){
				GUIPlaying.Instance().AddPropNum(GamePropsId.Bomb, 1);
			}
		}
	}

}
