using UnityEngine;

namespace Game.Tools.Events {

public delegate void EventAction();

[CreateAssetMenu(menuName="Event")]
public class GameEvent : ScriptableObject {
	
	public static GameEvent CurrentEvent { get; private set; }
	
	// -- //
	
	public GameObject Invoker { get; private set; }
	
	// -- //
	
	[SerializeField]
	[TextArea]
	string description;
	
	EventAction listeners;
	
	// -- //
	
	public void AddListener(EventAction listener) {
		listeners += listener;
	}
	
	public void RemoveListener(EventAction listener) {
		listeners -= listener;
	}
	
	public void Trigger() {
		CurrentEvent = this;
		if(listeners != null) {
			listeners.Invoke();
		}
		CurrentEvent = null;
	}
	
	public void Trigger(GameObject invoker) {
		Invoker = invoker;
		Trigger();
		Invoker = null;
	}
	
}

}