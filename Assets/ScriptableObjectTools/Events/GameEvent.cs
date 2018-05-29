using UnityEngine;

namespace Game.Tools.Events {

[CreateAssetMenu(menuName="Event")]
public class GameEvent : ScriptableObject {
	
	public GameObject Invoker { get; private set; }
	
	// -- //
	
	[SerializeField]
	[TextArea]
	string description;
	
	System.Action listeners;
	System.Action<GameEvent> listenersWithEvent;
	
	// -- //
	
	public void AddListener(System.Action listener) {
		listeners += listener;
	}
	
	public void AddListener(System.Action<GameEvent> listener) {
		listenersWithEvent += listener;
	}
	
	public void RemoveListener(System.Action listener) {
		listeners -= listener;
	}
	
	public void RemoveListener(System.Action<GameEvent> listener) {
		listenersWithEvent -= listener;
	}
	
	public void Trigger() {
		if(listeners != null) {
			listeners();
		}
		
		if(listenersWithEvent != null) {
			listenersWithEvent(this);
		}
	}
	
	public void Trigger(GameObject invoker) {
		Invoker = invoker;
		Trigger();
		Invoker = null;
	}
	
}

}