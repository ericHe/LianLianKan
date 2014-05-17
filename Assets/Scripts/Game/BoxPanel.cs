using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxPanel : MonoBehaviour {
	[HideInInspector]
	public float			boxPanelW;
	[HideInInspector]
	public float			boxPanelH;
	
	public float			speed;
	public ComBox			bottomBox	{ get; set; }
	public int				topRow		{ get; set; } //已经加载的最上面一行
	public int				bottomRow	{ get; set; } //关卡最下面一行，或者关卡最下一行消除后的最下面的空行
	public bool				canLink		{ get; set; } //是否还有可以相连的
	public bool				isLoadDone	{ get; set; } //是否加载完成全部关卡元素

	public GamePropsId		usingPropID { get; set; } //使用的道具
	public bool 			isUsingProp { get; set; }
	public bool				m_CanTouch 	{ get; set; }

	private int				visibleRow = 10;
	private BoxManager		boxManager;
	private static Object	_lock;
	private float			moveSpeed;
	private Level			m_level;
	private ResourceMgr		m_resource;

	private ComBox			m_SelectBox;
	private ComBox			m_OtherBox;
	private GameObject		background;
	private GameObject		boss;

	private ParticleSystem boxExp1;
	private ParticleSystem boxExp2;

	public void Init(){
		if(_lock == null)
			_lock = new Object();

		m_level = GamePlaying.Instance().currentLevel;
		m_resource = ResourceMgr.Instance();

		speed = 0.005f;
		moveSpeed = -speed;
		canLink = true;
		isLoadDone = false;
		usingPropID = GamePropsId.None;
		isUsingProp = false;
		m_CanTouch = true;

		boxPanelW = m_level.width * ConstValue.BoxWidth;
		boxPanelH = m_level.height * ConstValue.BoxHeight;
		Debug.Log("boxPanelWH:"+boxPanelW + "," + boxPanelH);

		transform.position = new Vector3(0, ScreenInfo.h/2-ConstValue.BoxHeight*visibleRow+boxPanelH/2, 0);
		background = Instantiate(m_resource.GetResFromName(ConstValue.GAME_BACKGROUND),
		                                  new Vector3(10f, 10f, 0),
		                                  Quaternion.identity) as GameObject;
		background.transform.parent = transform;

		boxManager = BoxManager.GetInstance();
		boxManager.Init();

		bottomRow = m_level.height - 1;
		InitBoxs(bottomRow, visibleRow);
		GetBottomBox();
	}

	/// <summary>
	/// Inits the boxs.
	/// </summary>
	/// <param name="downRow">Down row. 从多少行开始</param>
	/// <param name="numRow">Number row. 往上加载多少行</param>
	public void InitBoxs(int downRow, int numRow){
		lock(_lock) {
			topRow = downRow - numRow + 1;
			if(topRow == 0){
				//加载banner和boss
				GameObject banner = Instantiate(m_resource.GetResFromName(ConstValue.GAME_T_BANNER),
				                                Vector3.zero,
				                                Quaternion.identity) as GameObject;
				banner.transform.parent = transform;
				banner.transform.position = new Vector3(0, GetTopPosition()+ConstValue.TopBannerHeight/2, 0);
				if(ConstValue.currentLevel.boss > 0){
					boss = Instantiate(m_resource.GetResFromName(ConstValue.bosses[ConstValue.currentLevel.boss]),
				                                Vector3.zero, Quaternion.identity) as GameObject;
					boss.transform.parent = transform;
					boss.transform.position = new Vector3(0, banner.transform.position.y+ConstValue.BossHeight/2, 0);
					InvokeRepeating("BossFight", 1f, 3f);
				}
			}
			for(int i=downRow; i>=topRow; i--){
				for(int j=0; j<m_level.width; j++){
					int index = i*m_level.width+j;
					Debug.Log(downRow+","+topRow+","+index);
					if(m_level.data[index] != 0){
						GameObject boxClone = Instantiate(m_resource.GetResFromName(ConstValue.GAME_LIAN_PRE+m_level.data[index]),
						                                  GetPosFromXY(j, i),
						                                  Quaternion.identity) as GameObject;
						boxClone.transform.parent = transform;
						boxClone.transform.position += transform.position;
						ComBox cb = boxClone.GetComponent<ComBox>();
						cb.index = m_level.data[index];
						cb.ice = m_level.meta[index];
						cb.x = j;
						cb.y = i;
						cb.fall = false;
						boxManager[j, i] = cb;
					}
				}
			}
			bottomRow = boxManager.GetBottomNullRow();
		}
	}

	public void MyUpdate(){
		if (m_CanTouch && Input.GetMouseButtonDown(0))	
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
			if(hit.collider != null)
			{
				ComBox hitBox = hit.transform.gameObject.GetComponent<ComBox>();
				if(hitBox != null){
					if(isUsingProp){
						UsingProp(hitBox);
					} else {
						ClickComBox(hitBox);
					}
				}
			}
		}
	}

	public void MyFixedUpdate () {
		transform.position += new Vector3(0f,moveSpeed,0f); 
		//Debug.Log(GetTopPosition());
		if(topRow > 0 && GetTopPosition() <= ScreenInfo.h/2+0.1){
			InitBoxs(topRow-1, 1);
		}
		//最后一行出现完全时,检测是否有相连
		if(!isLoadDone && topRow <= 0 && GetTopPosition() <= ScreenInfo.h/2){
			isLoadDone = true;
			canLink = CheckCanLian();
		}
	}

	void BossFight(){
		BossEntity be = boss.GetComponent<BossEntity>();
		be.AddBoxFight();
		//bottomRow = boxManager.GetBottomNullRow();
	}

	public void SetMoveSpeed(float sp){
		moveSpeed = sp;
	}

	#region Update
	void UsingProp(ComBox hitBox){
		GamePlaying.Instance().isPlaying = true;
		m_CanTouch = false;
		background.transform.localPosition = new Vector3(10f, 10f, 0f);
		switch(usingPropID){
		case GamePropsId.Bomb:
			SetMoveSpeed(0f);
			GameObject bomb = Instantiate(m_resource.GetResFromName(ConstValue.GAME_PROP_BOMB),
			            Vector3.zero,
			            Quaternion.identity) as GameObject;
			bomb.transform.parent = transform;
			bomb.transform.localPosition = hitBox.gameObject.transform.localPosition;
			bomb.GetComponent<PropBomb>().Init(hitBox);
			break;
		case GamePropsId.Rocket:
			SetMoveSpeed(0f);
			GameObject rocket = Instantiate(m_resource.GetResFromName(ConstValue.GAME_PROP_ROCKET),
			                              Vector3.zero,
			                              Quaternion.identity) as GameObject;
			rocket.transform.parent = transform;
			rocket.transform.localPosition = hitBox.gameObject.transform.localPosition;
			rocket.GetComponent<PropRocket>().Init(hitBox);
			break;
		case GamePropsId.Shock:
			GameObject shock = Instantiate(m_resource.GetResFromName(ConstValue.GAME_PROP_SHOCK),
			                               Vector3.zero,
			                               Quaternion.identity) as GameObject;
			shock.transform.parent = transform;
			shock.transform.position = new Vector3(0f, -ScreenInfo.h/2 + ConstValue.BOTTOM_BAN_HEIGHT, 0f);
			SetMoveSpeed(0.2f);
			Invoke("BackMoveSpeed", 0.5f);
			break;
		case GamePropsId.Same:
			SetMoveSpeed(0f);
			GameObject same = Instantiate(m_resource.GetResFromName(ConstValue.GAME_PROP_SAME),
			                                Vector3.zero,
			                                Quaternion.identity) as GameObject;
			same.transform.parent = transform;
			same.transform.localPosition = hitBox.gameObject.transform.localPosition;
			same.GetComponent<PropSame>().Init(hitBox);
			break;
		}
	}

	public void BackMoveSpeed(){
		SetMoveSpeed (-speed);
	}

	void ClickComBox(ComBox hitBox) {
		if(hitBox.index != 10 && !hitBox.fall){
			if(m_SelectBox == null){
				m_SelectBox = hitBox;
				background.transform.localPosition = m_SelectBox.gameObject.transform.localPosition;
			} else {
				m_OtherBox = hitBox;
				if(m_SelectBox != m_OtherBox && m_SelectBox.index == m_OtherBox.index){
					List<Vector2> lineList = boxManager.LinkTwoBox(m_SelectBox, m_OtherBox);
					if(lineList != null){
						m_SelectBox.Explode();
						m_SelectBox = null;
						m_OtherBox.Explode();
						m_OtherBox = null;
						background.transform.localPosition = new Vector3(10f, 10f, 0f);
						DrawLine(lineList);
						GameStaticData.Combo += 1;
						CheckPanelState();
					} else {
						GameStaticData.Combo = 0;
						m_SelectBox = m_OtherBox;
						background.transform.localPosition = m_SelectBox.gameObject.transform.localPosition;
					}
				} else {
					GameStaticData.Combo = 0;
					m_SelectBox = m_OtherBox;
					background.transform.localPosition = m_SelectBox.gameObject.transform.localPosition;
				}
			}
		}
	}

	public void CheckPanelState(){
		CheckFallDown();
		GetBottomBox();
		canLink = CheckCanLian();
	}
	#endregion

	#region box
	public Vector3 GetPosFromXY(int x, int y){
		return new Vector3(-boxPanelW/2 + (2*x+1)*ConstValue.BoxWidth/2, boxPanelH/2 - (2*y+1)*ConstValue.BoxHeight/2, 0f);
	}

	public void GetBottomBox () {
		bottomBox = boxManager.GetBottomBox();
	}

	//检测可以掉落的
	void CheckFallDown(){
		List<ComBox> allBoxs = boxManager.GetAllRow();
		List<ComBox> topBoxs = boxManager.GetBoxListByRow(topRow);

		//把所有球状态设置为未连接
		foreach (ComBox box in allBoxs) {
			box.link = false;
			box.check = false;
		}
		//遍历检测
		foreach (ComBox box in topBoxs) {
			CheckFallBall(box);
		}

		foreach (ComBox box in allBoxs) {
			if(!box.link) {
				boxManager.RemoveBox(box);
				box.FallDown();
			}
		}
	}

	//检测是否还有可以相连
	bool CheckCanLian(){
		List<ComBox> allBoxs = boxManager.GetAllRow();
		foreach (ComBox box in allBoxs) {
			box.check = false;
		}
		foreach (ComBox box in allBoxs) {
			if(!box.check){
				List<ComBox> sameBoxs = boxManager.GetSameBoxs(box);
				foreach(ComBox same in sameBoxs){
					Debug.Log(same.index);
					if(boxManager.LinkTwoBox(box, same) != null) return true;
				}
			}
		}
		return false;
	}
	
	void CheckFallBall(ComBox box){
		if(box.check) return;
		box.check = true;
		box.link = true;

		List<ComBox> neighbors = boxManager.GetNeighborBoxs(box);
		foreach(ComBox boxTemp in neighbors){
			CheckFallBall(boxTemp);
		}
	}

	float GetTopPosition(){
		return GetPosFromXY(0, topRow).y + ConstValue.BoxHeight/2 + transform.position.y;
	}
	#endregion

	#region line
	void DrawLine(List<Vector2> list){
		List<GameObject> m_lines = new List<GameObject>();
		for(int i=1; i<list.Count; i++){
			m_lines.Add(DrawLineFromTwoPos(list[i-1], list[i]));
		}
		list.Clear();
		list = null;
		StartCoroutine(DestoryLines(m_lines));
	}
	
	GameObject DrawLineFromTwoPos(Vector2 one, Vector2 two){
		int oneX = (int)one.x, oneY = (int)one.y, twoX = (int)two.x, twoY = (int)two.y;
		//Debug.Log(oneX + "," + oneY);
		//Debug.Log(twoX + "," + twoY);
		Vector3 oneP = GetPosFromXY(oneX, oneY), twoP =  GetPosFromXY(twoX, twoY);
		//Debug.Log(oneP.x + "," + oneP.y);
		//Debug.Log(twoP.x + "," + twoP.y);
		GameObject line;
		if(oneX == twoX) {
			float scaleY = Mathf.Abs(oneP.y - twoP.y)/ConstValue.BoxHeight;
			line = Instantiate(m_resource.GetResFromName(ConstValue.GAME_LINE_SHU),
			                   new Vector3(oneP.x, oneP.y + (twoP.y - oneP.y)/2, 0f),
			                   Quaternion.identity) as GameObject;
			line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y*scaleY, line.transform.localScale.z);
		} else {
			float scaleX = Mathf.Abs(oneP.x - twoP.x)/ConstValue.BoxWidth;
			line = Instantiate(m_resource.GetResFromName(ConstValue.GAME_LINE_HEN),
			                   new Vector3(oneP.x + (twoP.x - oneP.x)/2, oneP.y, 0f),
			                   Quaternion.identity) as GameObject;
			line.transform.localScale = new Vector3(line.transform.localScale.x*scaleX, line.transform.localScale.y, line.transform.localScale.z);
		}
		line.transform.parent = transform;
		line.transform.position += transform.position;
		return line;
	}

	IEnumerator DestoryLines(List<GameObject> lines){
		yield return new WaitForSeconds(0.3f);
		foreach(GameObject go in lines){
			Destroy(go);
		}
	}
	#endregion
}
