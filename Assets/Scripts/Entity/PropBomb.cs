using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropBomb : MonoBehaviour {
	ComBox box;
	void Start(){

	}

	public void Init(ComBox box){
		this.box = box;
		Invoke("Explode", 1f);
	}

	void Explode(){
		Destroy(gameObject);
		List<ComBox> neighborBoxs = BoxManager.GetInstance().GetNeighborBoxs(box);
		box.Explode();
		foreach(ComBox comBox in neighborBoxs){
			comBox.Explode();
		}
		GamePlaying.Instance().boxPanel.CheckPanelState();
		GamePlaying.Instance().isPlaying = true;
		GamePlaying.Instance().boxPanel.isUsingProp = false;
		GamePlaying.Instance().boxPanel.usingPropID = GamePropsId.None;
	}
}
