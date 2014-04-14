using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PanelLevel : MonoBehaviour {
	public List<GameObject> levelList;

	private string levelCan	= "level_start";
	private string levelUn	= "level_start_un";
	public void Init(){
		int count = levelList.Count;
		int level = PlayerPrefs.GetInt(ConstValue.PRE_CUR_LEVEL, 1);
		//test
		if(level > 3) level = 3;
		for(int i=1; i<=level; i++){
			levelList[i-1].GetComponent<UISprite>().spriteName = levelCan;
			UIEventListener.Get(levelList[i-1]).onClick = GUILevel.Instance().LevelClick;
		}
		for(int i=level+1; i<=count; i++){
			levelList[i-1].GetComponent<UISprite>().spriteName = levelUn;
			levelList[i-1].GetComponent<BoxCollider>().enabled = false;
		}
	}
}
