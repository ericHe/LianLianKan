using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoxManager {
	private static List<BoxObject> m_BoxList = new List<BoxObject>();
	private static BoxManager instance;

	public static BoxManager GetInstance ()
	{
		if (instance == null) {
			instance = new BoxManager();
		}
		return instance;
	}

	private int levelWidth;
	private int levelHeight;

	public ComBox this[int x, int y] {
		get {
			BoxObject tmp = m_BoxList.Find (p => p.x == x && p.y == y);
			if (tmp != null && tmp.gameObject != null) {
				return tmp.gameObject.GetComponent<ComBox> ();
			} else
				return null;
		}
		set {
			if (x < 0 || y < 0)
				return;
			if (m_BoxList.Find (p => p.x == x && p.y == y) != null) {
				BoxObject tmp = m_BoxList.Find (p => p.x == x && p.y == y && p.gameObject != null);
				if (value == null && tmp != null) {
					//GameObject.Destroy(tmp.gameObject);
					m_BoxList.Remove(tmp);
				} else {
					if (tmp != null) {
						tmp.gameObject = value.gameObject;
						value.x = x;
						value.y = y;
					}
				}
			} else {
				AddBox(value, x, y);
			}
		}
	}

	public void Init(){
		m_BoxList.Clear();
		levelWidth = GamePlaying.Instance().currentLevel.width;
		levelHeight = GamePlaying.Instance().currentLevel.height;
	}

	void AddBox(ComBox box, int x, int y){
		BoxObject bo = new BoxObject(x, y, box.index);
		if(box != null) bo.gameObject = box.gameObject;
		m_BoxList.Add(bo);
	}

	public void RemoveBox(ComBox box){
		this[box.x, box.y] = null;
	}

	public ComBox GetBottomBox ()
	{
		if (m_BoxList.Count == 0) {
			return null;
		}		
		BoxObject tmp = m_BoxList.OrderByDescending (p => p.y).FirstOrDefault ();
		if(tmp != null)
			return tmp.gameObject.GetComponent<ComBox> ();
		return null;
	}

	public List<ComBox> GetBoxListByRow(int row){
		List<ComBox> bbs = new List<ComBox> ();
		List<BoxObject> tmpList = m_BoxList.FindAll (p => p.y == row);
		for (int i=0; i<tmpList.Count; i++) {
			bbs.Add (tmpList[i].gameObject.GetComponent<ComBox>());
		}
		return bbs;
	}

	public List<ComBox> GetAllRow(){
		List<ComBox> bbs = new List<ComBox> ();
		for (int i=0; i<m_BoxList.Count; i++) {
			bbs.Add (m_BoxList[i].gameObject.GetComponent<ComBox>());
		}
		return bbs;
	}

	public List<ComBox> GetNeighborBoxs(ComBox box){
		List<ComBox> boxs = new List<ComBox>();
		ComBox boxTemp = null;
		if((boxTemp = this[box.x-1, box.y-1]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x, box.y-1]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x+1, box.y-1]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x-1, box.y]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x+1, box.y]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x-1, box.y+1]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x, box.y+1]) != null) boxs.Add(boxTemp);
		if((boxTemp = this[box.x+1, box.y+1]) != null) boxs.Add(boxTemp);
		return boxs;
	}

	//取和当前box相同类型的box, 不包括自己
	public List<ComBox> GetSameBoxs(ComBox box){
		List<ComBox> bbs = new List<ComBox> ();
		List<BoxObject> tmpList = m_BoxList.FindAll (p => p.index == box.index && (p.x != box.x || p.y != box.y));
		for (int i=0; i<tmpList.Count; i++) {
			bbs.Add (tmpList[i].gameObject.GetComponent<ComBox>());
		}
		return bbs;
	}

	/// <summary>
	/// 取最下面的空行，如果是关卡最后一行，返回最后一行
	/// </summary>
	/// <returns>The bottom null row.</returns>
	public int GetBottomNullRow(){
		if (m_BoxList.Count == 0) {
			return 0;
		}		
		BoxObject tmp = m_BoxList.OrderByDescending (p => p.y).FirstOrDefault ();
		if(tmp != null){
			int result = tmp.y;
			if(result == levelHeight-1)
				return result;
			else
				return result + 1;
		}
		return 0;
	}

	#region link
	public List<Vector2> LinkTwoBox(ComBox one, ComBox two){
		List<Vector2> list = new List<Vector2>();
		if(CheckDirectLink(one.x, one.y, two.x, two.y)) {
			list.Add(new Vector2(one.x, one.y));
			list.Add(new Vector2(two.x, two.y));
			return list;
		}
		Vector2 tmpV = CheckTwoPoint(one.x, one.y, two.x, two.y);
		if(tmpV.x > -1){
			list.Add(new Vector2(one.x, one.y));
			list.Add(tmpV);
			list.Add(new Vector2(two.x, two.y));
			return list;
		}
		List<Vector2> tmpList = CheckThreePoint(one.x, one.y, two.x, two.y);
		if(tmpList != null) {
			list.Add(new Vector2(one.x, one.y));
			list.Add(tmpList[0]);
			list.Add(tmpList[1]);
			list.Add(new Vector2(two.x, two.y));
			return list;
		}
		return null;
	}

	bool CheckSameX(int x, int oneY, int twoY){
		int maxY = Math.Max(oneY, twoY);
		int minY = Math.Min(oneY, twoY);
		for(int i=minY+1; i<maxY; i++){
			if(this[x, i] != null) return false;
		}
		return true;
	}

	bool CheckSameY(int y, int oneX, int twoX){
		int maxX = Math.Max(oneX, twoX);
		int minX = Math.Min(oneX, twoX);
		for(int i=minX+1; i<maxX; i++){
			if(this[i, y] != null) return false;
		}
		return true;
	}

	bool CheckDirectLink(int oneX, int oneY, int twoX, int twoY){
		if(oneX == twoX){
			if(CheckSameX(oneX, oneY, twoY))
				return true;
		}
		if(oneY == twoY){
			if(CheckSameY(oneY, oneX, twoX))
				return true;
		}
		return false;
	}

	Vector2 CheckTwoPoint(int oneX, int oneY, int twoX, int twoY){
		if(CheckDirectLink(oneX, oneY, oneX, twoY) && this[oneX, twoY] == null && CheckDirectLink(twoX, twoY, oneX, twoY))
			return new Vector2(oneX, twoY);
		if(CheckDirectLink(oneX, oneY, twoX, oneY) && this[twoX, oneY] == null && CheckDirectLink(twoX, twoY, twoX, oneY))
			return new Vector2(twoX, oneY);
		return new Vector2(-1f, -1f);
	}

	List<Vector2> CheckThreePoint(int oneX, int oneY, int twoX, int twoY){
		bool left = true, right = true, up = true, down = true;
		int len = Math.Max(levelWidth, levelHeight);
		List<Vector2> list = new List<Vector2>();
		int j;
		Vector2 temV;
		for(int i=1; i<len-1; i++){
			if(left){
				j = oneX - i;
				if(j<0 || this[j, oneY] != null)
					left = false;
				else {
					temV =	CheckTwoPoint(j, oneY, twoX, twoY);
					if(temV.x > -1){
						list.Add(new Vector2(j, oneY));
						list.Add(temV);
						return list;
					}
				}
			}
			if(right){
				j = oneX + i;
				if(j>levelWidth-1 || this[j, oneY] != null)
					right = false;
				else {
					temV = CheckTwoPoint(j, oneY, twoX, twoY);
					if(temV.x > -1){
						list.Add(new Vector2(j, oneY));
						list.Add(temV);
						return list;
					}
				}
			}
			if(up){
				j = oneY - i;
				if(j<GamePlaying.Instance().boxPanel.topRow || this[oneX, j] != null)
					up = false;
				else {
					temV = CheckTwoPoint(oneX, j, twoX, twoY);
					if(temV.x > -1){
						list.Add(new Vector2(oneX, j));
						list.Add(temV);
						return list;
					}
				}
			}
			if(down){
				j = oneY + i;
				if(j>GamePlaying.Instance().boxPanel.bottomRow || this[oneX, j] != null)
					down = false;
				else {
					temV = CheckTwoPoint(oneX, j, twoX, twoY);
					if(temV.x > -1){
						list.Add(new Vector2(oneX, j));
						list.Add(temV);
						return list;
					}
				}
			}
			if(!left && !right && !up && !down) break;
		}
		return null;
	}
	#endregion
}
