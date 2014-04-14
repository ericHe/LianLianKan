using UnityEngine;
using System.Collections;

public class GameTools {
	static public GameObject AddChild (GameObject parent, Object prefab)
	{
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		
		#if UNITY_EDITOR && !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2
		UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
		#endif
		
		if (go != null && parent != null)
		{
			Transform t = go.transform;
			t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			go.layer = parent.layer;
		}
		return go;
	}

	public static T CreateGameObject<T> () where T: Component {
		GameObject linkObject = new GameObject(typeof(T).Name);
		T link = linkObject.AddComponent<T>();
		return link;
	}
}
