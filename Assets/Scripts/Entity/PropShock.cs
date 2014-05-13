using UnityEngine;
using System.Collections;
using Jun.Message;

public class PropShock : MonoBehaviour {
	float speed = 0.2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
		if (transform.position.y > ScreenInfo.h / 2) {
			Destroy (gameObject);
			GamePlaying.Instance ().boxPanel.BackMoveSpeed ();
			GamePlaying.Instance().isPlaying = true;
			GamePlaying.Instance().boxPanel.m_CanTouch = true;
			GamePlaying.Instance().boxPanel.isUsingProp = false;
			GamePlaying.Instance().boxPanel.usingPropID = GamePropsId.None;
			Messenger.Broadcast(ConstValue.MSG_USE_PROP_SUC, GamePropsId.Shock);
		}
	}
}
