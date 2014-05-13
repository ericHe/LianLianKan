using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Jun.Message;

public class PropRocket : MonoBehaviour {
	ComBox box;
	GameObject leftTrail;
	GameObject rightTrail;
	List<ComBox> rowBoxs;

	int boxX;
	int desIndex;
	// Use this for initialization
	void Start () {
	
	}
	
	public void Init(ComBox box){
		this.box = box;
		boxX = box.x;
		desIndex = 0;
		Invoke("Explode", 1f);
	}
	
	void Explode(){
		gameObject.SetActive(false);
		leftTrail = GameObject.Instantiate(ResourceMgr.Instance().GetResFromName(ConstValue.GAME_ROCKET_TR),
		                             transform.position, Quaternion.identity) as GameObject;
		leftTrail.GetComponent<RocketTrail>().speed = -0.14f;
		rightTrail = GameObject.Instantiate(ResourceMgr.Instance().GetResFromName(ConstValue.GAME_ROCKET_TR),
		                                   transform.position, Quaternion.identity) as GameObject;
		rightTrail.GetComponent<RocketTrail>().speed = 0.14f;
		InvokeRepeating("DestoryBox", 0.01f, 0.1f);
		Invoke("DestoryTrail", 1f);

		rowBoxs = BoxManager.GetInstance().GetBoxListByRow(box.y);
	}

	void DestoryBox(){
		Debug.Log("," + Time.fixedTime + "," + Time.time);
		foreach(ComBox comBox in rowBoxs){
			if(comBox.x == boxX + desIndex || comBox.x == boxX - desIndex){
				Debug.Log(comBox.x + "," + desIndex + "," + boxX);
				comBox.Explode();
			}
		}
		desIndex++;
	}

	void DestoryTrail(){
		CancelInvoke("DestoryBox");
		Destroy(leftTrail);
		Destroy(rightTrail);
		Destroy(gameObject);

		GamePlaying.Instance().boxPanel.CheckPanelState();
		GamePlaying.Instance ().boxPanel.BackMoveSpeed ();
		GamePlaying.Instance().isPlaying = true;
		GamePlaying.Instance ().boxPanel.m_CanTouch = true;
		GamePlaying.Instance().boxPanel.isUsingProp = false;
		GamePlaying.Instance().boxPanel.usingPropID = GamePropsId.None;
		Messenger.Broadcast(ConstValue.MSG_USE_PROP_SUC, GamePropsId.Rocket);
	}
}
