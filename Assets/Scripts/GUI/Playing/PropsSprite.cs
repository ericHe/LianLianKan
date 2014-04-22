using UnityEngine;
using System.Collections;

public class PropsSprite : MonoBehaviour {
	private int num;
	public int Num {
		get {
			return num;
		}
		set {
			num = value;
			numLabel.text = num.ToString();
		}
	}

	public UILabel numLabel;
	public TweenScale tweenScale;

	public enum PropState {
		Default = 0,
		Click
	}
	private PropState state;
	public PropState State{
		get{ return state; }
		set{
			state = value;
			switch(state){
			case PropState.Default:
				transform.localScale = Vector3.one;
				tweenScale.enabled = false;
				break;
			case PropState.Click:
				transform.localScale = Vector3.one;
				tweenScale.enabled = true;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("PropsSprite start");
		State = PropState.Default;
	}
}
