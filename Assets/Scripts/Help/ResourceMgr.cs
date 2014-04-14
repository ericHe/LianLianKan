using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceMgr {

	private static ResourceMgr instance;
	public static ResourceMgr Instance (){
		if (instance == null)instance = new ResourceMgr();
		return instance;
	}

	private Dictionary<string, Object> m_ResMap = new Dictionary<string, Object> ();

	public Object GetResFromName(string name){
		if(m_ResMap.ContainsKey(name)) return m_ResMap[name];
		return null;
	}

	public void RemoveResByName(string name){
		if(m_ResMap.ContainsKey(name)){
			m_ResMap.Remove(name);		
		}
		Resources.UnloadUnusedAssets();
	}
	
	public Object LoadRes (string path, string name) {
		if(m_ResMap.ContainsKey(name)) return m_ResMap[name];

		Object o = Resources.Load(path + "/" + name);
		if(o != null) m_ResMap.Add(name, o);
		return o;
	}
}
