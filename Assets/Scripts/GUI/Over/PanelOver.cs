using UnityEngine;
using System.Collections;

public class PanelOver : MonoBehaviour {

	public UIPlayTween		playTween;
	public GameObject		retryBtn;

	public void Init(){
		playTween.Play(true);
		UIEventListener.Get(retryBtn).onClick = RetryBtn;
	}

	public void RetryBtn(GameObject go){
		playTween.onFinished.Clear ();
		EventDelegate.Add (playTween.onFinished, GUIOver.Instance().RetryBtnClick, true);
		playTween.Play(false);
	}
}
