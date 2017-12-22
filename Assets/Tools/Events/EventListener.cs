using UnityEngine;
using UnityEngine.Events;

namespace Game.Tools.Events {

public class EventListener : MonoBehaviour {
	
	public GameEvent _eventToListen;
	public UnityEvent _actions;
	
	// -- //
	
	void Awake() {
		_eventToListen.AddListener(OnEvent);
	}
	
	void OnDestroy() {
		_eventToListen.RemoveListener(OnEvent);
		_actions.RemoveAllListeners();
	}
	
	// -- //
	
	void OnEvent() {
		_actions.Invoke();
	}
	
}

}