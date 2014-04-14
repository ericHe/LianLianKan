using UnityEngine;
using System.Collections;

public class PanelWin : MonoBehaviour {

	public UIPlayTween		playTween;
	public GameObject		nextBtn;

	public void Init(){
		playTween.Play(true);
		UIEventListener.Get(nextBtn).onClick = NextBtn;
	}

	void NextBtn(GameObject go){
		playTween.onFinished.Clear ();
		EventDelegate.Add (playTween.onFinished, GUIWin.Instance().NextBtnClick, true);
		playTween.Play(false);
	}
}
