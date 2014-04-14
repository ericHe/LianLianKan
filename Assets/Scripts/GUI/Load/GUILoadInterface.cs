using UnityEngine;
using System.Collections;

public class GUILoadInterface {
	public bool is_load = false;
	public virtual void Load(GUIManager gui){}
	public virtual void LoadDone(GUIManager gui){}
}
