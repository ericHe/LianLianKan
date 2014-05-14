using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Jun.Message;

public class PropSame : MonoBehaviour {
	ComBox box;

	public void Init(ComBox box){
		this.box = box;
		Invoke("Explode", 1f);
	}

	void Explode(){
		gameObject.SetActive(false);

		List<ComBox> sameBoxs = BoxManager.GetInstance().GetSameBoxs(box);
		box.Explode();
		int c = sameBoxs.Count;
		if(c == 1) 
			sameBoxs[0].Explode();
		else if(c > 1){
			int same = Random.Range(0, c);
			sameBoxs[same].Explode();
		}
		GamePlaying.Instance().boxPanel.CheckPanelState();
		GamePlaying.Instance ().boxPanel.BackMoveSpeed ();
		GamePlaying.Instance().isPlaying = true;
		GamePlaying.Instance ().boxPanel.m_CanTouch = true;
		GamePlaying.Instance().boxPanel.isUsingProp = false;
		GamePlaying.Instance().boxPanel.usingPropID = GamePropsId.None;
		Messenger.Broadcast(ConstValue.MSG_USE_PROP_SUC, GamePropsId.Same);

		Destroy(gameObject);
	}
}
