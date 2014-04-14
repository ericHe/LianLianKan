using UnityEngine;
using System.Collections;
using Jun.Message;

public class GamePlaying : State<GameManager> {
	private static GamePlaying instance;
	public static GamePlaying Instance (){
		if (instance == null)instance = new GamePlaying();
		return instance;
	}

	public Level		currentLevel	{ get; set; }
	public BoxPanel		boxPanel		{ get; set; }
	public GameObject	bBanner			{ get; set; }

	private bool	isPlaying = false;
	
	public override void Enter (GameManager Entity)
	{
		Messenger.AddListener(ConstValue.MSG_GAME_TO_LEVEL, GameToLevel);
		Messenger.AddListener<GamePropsId>(ConstValue.MSG_USE_PROP, UseProp);

		currentLevel = LoadLevel.CreateLevel(ConstValue.level);
		boxPanel = GameTools.CreateGameObject<BoxPanel>();
		boxPanel.Init();
		bBanner = GameObject.Instantiate(ResourceMgr.Instance().GetResFromName(ConstValue.GAME_B_BANNER),
		            new Vector3(0f, -7f, 0),
		            Quaternion.identity) as GameObject;
		isPlaying = true;
	}
	
	public override void Execute (GameManager Entity)
	{
		if(isPlaying)
			boxPanel.MyUpdate();
	}

	public override void LateExecute (GameManager Entity)
	{
		if(isPlaying){
			if(boxPanel.bottomBox != null
			   && (boxPanel.bottomBox.transform.position.y - ConstValue.BoxHeight/2) <= -ScreenInfo.h/2){
				isPlaying = false;
				Messenger.Broadcast(ConstValue.MSG_GAME_DONE, GameDoneState.GameOver);
			}
			
			if(boxPanel.isLoadDone && !boxPanel.canLink){
				isPlaying = false;
				Messenger.Broadcast(ConstValue.MSG_GAME_DONE, GameDoneState.GameWin);
			}
		}
	}

	public override void FixedExecute (GameManager Entity){
		if(isPlaying)
			boxPanel.MyFixedUpdate();
	}
	
	public override void Exit (GameManager Entity)
	{
		isPlaying = false;
		Messenger.RemoveListener(ConstValue.MSG_GAME_TO_LEVEL, GameToLevel);
		Messenger.RemoveListener<GamePropsId>(ConstValue.MSG_USE_PROP, UseProp);
		GameObject.Destroy(boxPanel.gameObject);
		GameObject.Destroy(bBanner);
	}

	void GameToLevel(){
		Target.GetFSM().ChangeState(GameStart.Instance());
	}

	void UseProp(GamePropsId propId){
		switch(propId){
		case GamePropsId.Bomb:
			Debug.Log("user bomb");
			break;
		}
	}
}
