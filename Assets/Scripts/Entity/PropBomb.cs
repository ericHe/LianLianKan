using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Jun.Message;

public class PropBomb : MonoBehaviour {
	ComBox box;
	GameObject exp;
	void Start(){

	}

	public void Init(ComBox box){
		this.box = box;
		Invoke("Explode", 1f);
	}

	void Explode(){
		gameObject.SetActive(false);
		exp = GameObject.Instantiate(ResourceMgr.Instance().GetResFromName(ConstValue.GAME_BOMB_EXP),
		                                        transform.position, Quaternion.identity) as GameObject;
		ParticleSystem ps = exp.GetComponent<ParticleSystem>();
		ps.Play();
		Invoke("DestoryExp", 1f);

		List<ComBox> neighborBoxs = BoxManager.GetInstance().GetNeighborBoxs(box);
		box.Explode();
		foreach(ComBox comBox in neighborBoxs){
			comBox.Explode();
		}
		GamePlaying.Instance().boxPanel.CheckPanelState();
		GamePlaying.Instance ().boxPanel.BackMoveSpeed ();
		GamePlaying.Instance().isPlaying = true;
		GamePlaying.Instance ().boxPanel.m_CanTouch = true;
		GamePlaying.Instance().boxPanel.isUsingProp = false;
		GamePlaying.Instance().boxPanel.usingPropID = GamePropsId.None;
		Messenger.Broadcast(ConstValue.MSG_USE_PROP_SUC, GamePropsId.Bomb);
	}

	void DestoryExp(){
		Destroy(exp);
		Destroy(gameObject);
	}
}
