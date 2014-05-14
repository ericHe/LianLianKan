using UnityEngine;
using System.Collections;
using Jun.Message;

public class PanelPlaying : MonoBehaviour {

	public PropsSprite bombBtn;
	public PropsSprite rocketBtn;
	public PropsSprite shockBtn;
	public PropsSprite sameBtn;
	public UILabel comboNumLabel;

	private GamePropsId currentProp = GamePropsId.None;

	void Awake(){
		UIEventListener.Get(bombBtn.gameObject).onClick 	= BombBtnClick;
		UIEventListener.Get(rocketBtn.gameObject).onClick 	= RocketBtnClick;
		UIEventListener.Get (shockBtn.gameObject).onClick 	= ShockBtnClick;
		UIEventListener.Get (sameBtn.gameObject).onClick 	= sameBtnClick;
	}

	public void Init(){
		bombBtn.Num = 1;
		rocketBtn.Num = 5;
		shockBtn.Num = 5;
		sameBtn.Num = 5;
		ChangeComboNum();
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

	void ShockBtnClick(GameObject go){
		if(currentProp == GamePropsId.Shock){
			shockBtn.State = PropsSprite.PropState.Default;
			Messenger.Broadcast(ConstValue.MSG_USE_PROP_CEL, GamePropsId.Shock);
			ChangeCurrentProp(GamePropsId.None);
		} else {
			if(shockBtn.Num > 0){
				ChangeCurrentProp(GamePropsId.Shock);
				shockBtn.State = PropsSprite.PropState.Click;
				GUIPlaying.Instance().UseProp(GamePropsId.Shock);
			}
		}
	}

	void sameBtnClick(GameObject go){
		if(currentProp == GamePropsId.Same){
			sameBtn.State = PropsSprite.PropState.Default;
			Messenger.Broadcast(ConstValue.MSG_USE_PROP_CEL, GamePropsId.Same);
			ChangeCurrentProp(GamePropsId.None);
		} else {
			if(sameBtn.Num > 0){
				ChangeCurrentProp(GamePropsId.Same);
				sameBtn.State = PropsSprite.PropState.Click;
				GUIPlaying.Instance().UseProp(GamePropsId.Same);
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
			case GamePropsId.Shock:
				shockBtn.State = PropsSprite.PropState.Default;
				break;
			case GamePropsId.Same:
				sameBtn.State = PropsSprite.PropState.Default;
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
		case GamePropsId.Shock:
			shockBtn.Num -= 1;
			break;
		case GamePropsId.Same:
			sameBtn.Num -= 1;
			break;
		}
	}

	public void AddPropNum(GamePropsId prop, int num = 1){
		switch(prop){
		case GamePropsId.Bomb:
			bombBtn.Num += num;
			break;
		case GamePropsId.Rocket:
			rocketBtn.Num += num;
			break;
		case GamePropsId.Shock:
			shockBtn.Num += num;
			break;
		case GamePropsId.Same:
			sameBtn.Num += num;
			break;
		}
	}

	public void ChangeComboNum(){
		comboNumLabel.text = GameStaticData.Combo.ToString();
	}
}
