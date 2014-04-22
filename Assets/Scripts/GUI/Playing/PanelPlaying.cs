using UnityEngine;
using System.Collections;
using Jun.Message;

public class PanelPlaying : MonoBehaviour {

	public PropsSprite bombBtn;
	public PropsSprite rocketBtn;

	private GamePropsId currentProp = GamePropsId.None;

	void Awake(){
		UIEventListener.Get(bombBtn.gameObject).onClick 	= BombBtnClick;
		UIEventListener.Get(rocketBtn.gameObject).onClick 	= RocketBtnClick;
	}

	public void Init(){
		bombBtn.Num = 4;
		rocketBtn.Num = 5;
	}

	void BombBtnClick(GameObject go){
		if(currentProp == GamePropsId.Bomb){
			bombBtn.State = PropsSprite.PropState.Default;
			Messenger.Broadcast(ConstValue.MSG_USE_PROP_CEL, GamePropsId.Bomb);
			ChangeCurrentProp(GamePropsId.None);
		} else {
			if(bombBtn.Num > 0){
				ChangeCurrentProp(GamePropsId.Bomb);
				bombBtn.State = PropsSprite.PropState.Click;
				GUIPlaying.Instance().UseProp(GamePropsId.Bomb);
			}
		}
	}

	void RocketBtnClick(GameObject go){
		if(currentProp == GamePropsId.Rocket){
			rocketBtn.State = PropsSprite.PropState.Default;
			Messenger.Broadcast(ConstValue.MSG_USE_PROP_CEL, GamePropsId.Rocket);
			ChangeCurrentProp(GamePropsId.None);
		} else {
			if(rocketBtn.Num > 0){
				ChangeCurrentProp(GamePropsId.Rocket);
				rocketBtn.State = PropsSprite.PropState.Click;
				GUIPlaying.Instance().UseProp(GamePropsId.Rocket);
			}
		}
	}

	void ChangeCurrentProp(GamePropsId prop){
		if(currentProp != GamePropsId.None){
			switch(currentProp){
			case GamePropsId.Bomb:
				bombBtn.State = PropsSprite.PropState.Default;
				break;
			case GamePropsId.Rocket:
				rocketBtn.State = PropsSprite.PropState.Default;
				break;
			}
		}
		currentProp = prop;
	}

	public void UsePropSuc(GamePropsId prop){
		ChangeCurrentProp(GamePropsId.None);
		switch(prop){
		case GamePropsId.Bomb:
			bombBtn.Num -= 1;
			break;
		case GamePropsId.Rocket:
			rocketBtn.Num -= 1;
			break;
		}
	}
}
