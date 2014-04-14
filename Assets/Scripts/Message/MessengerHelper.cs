using UnityEngine;
using System.Collections;
using Jun.Message;

//This manager will ensure that the messenger's eventTable will be cleaned up upon loading of a new level.
public sealed class MessengerHelper : MonoBehaviour {
	void Awake ()
	{
		DontDestroyOnLoad(gameObject);	
	}
	
	//Clean up eventTable every time a new level loads.
	public void OnDisable() {
		Messenger.Cleanup();
	}
}
