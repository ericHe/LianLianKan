using UnityEngine;
using System.Collections;

public class ScreenInfo : MonoBehaviour {
	public static float w;
	public static float h;

	void Awake(){
		h = camera.orthographicSize*2;
		w = h * (float)Screen.width/(float)Screen.height;

		Debug.Log("ScreenInfo: width "+ ScreenInfo.w + ", height " + ScreenInfo.h);
	}

}
