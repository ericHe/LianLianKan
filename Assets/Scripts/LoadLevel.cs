using UnityEngine;
using System.Collections;
using LitJson;

public class LoadLevel {
	public static Level CreateLevel(int lv){
		TextAsset textAsset = Resources.Load("Levels/"+lv, typeof(TextAsset)) as TextAsset;
		return JsonMapper.ToObject<Level>(textAsset.text);
	}
}
