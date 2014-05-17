using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossEntity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddBoxFight(){
		//取两个位置
		List<Vector2> nullPosition = BoxManager.GetInstance().GetTwoNullPosition();
		Debug.Log("nullPosition"+nullPosition.Count);
		foreach(Vector2 v in nullPosition){
			Debug.Log(""+v.x + "," + v.y);
		}

		Random.seed = (int)System.DateTime.Now.Ticks;

		if(nullPosition.Count > 1){
			int boxId = Random.Range(1, 11);
			Object boxClone = Instantiate(ResourceMgr.Instance().GetResFromName(ConstValue.GAME_LIAN_PRE+boxId),
			                                  Vector3.zero,
			                                  Quaternion.identity);
			GameObject oneBox = boxClone as GameObject;
			oneBox.transform.parent = transform;
			oneBox.transform.position = transform.position;
			oneBox.GetComponent<ComBox>().fall = true;

			GameObject secBox = Instantiate(boxClone) as GameObject;
			secBox.transform.parent = transform;
			secBox.transform.position = transform.position;
			secBox.GetComponent<ComBox>().fall = true;

			int firIndex = Random.Range(0, nullPosition.Count);
			Vector2 firPos = nullPosition[firIndex];
			Debug.Log("firPos "+firPos.x + "," + firPos.y);
			nullPosition.RemoveAt(firIndex);
			int secIndex = Random.Range(0, nullPosition.Count);
			Vector2 secPos = nullPosition[secIndex];
			Debug.Log("secPos "+secPos.x + "," + secPos.y);
		}
	}
}
