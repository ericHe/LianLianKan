using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpParticleFactory{
	private static ExpParticleFactory instance;
	public static ExpParticleFactory Instance(){
		if (instance == null)instance = new ExpParticleFactory();
		return instance;
	}

	List<ParticleSystem> expList = new List<ParticleSystem>();

	public ParticleSystem CreateExpParticel(Vector3 pos){
		foreach(ParticleSystem go in expList){
			if(!go.IsAlive()){
				go.transform.position = pos;
				return go;
			}
		}
		GameObject exp = GameObject.Instantiate(ResourceMgr.Instance().GetResFromName(ConstValue.GAME_BOX_EXP),
		                                        pos, Quaternion.identity) as GameObject;
		ParticleSystem ps = exp.GetComponent<ParticleSystem>();
		expList.Add(ps);
		return ps;
	}
}
