using UnityEngine;
using System.Collections;

public class PanelPlaying : MonoBehaviour {

	public GameObject bombBtn;

	void Awake(){
		UIEventListener.Get(bombBtn).onClick = BombBtnClick;
	}

	void BombBtnClick(GameObject go){
		GUIPlaying.Instance().UseProp(GamePropsId.Bomb);
	}
}
