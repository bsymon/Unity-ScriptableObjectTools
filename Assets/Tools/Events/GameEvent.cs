using UnityEngine;

namespace Game.Tools.Events {

public delegate void EventAction();

[CreateAssetMenu(menuName="Event")]
public class GameEvent : ScriptableObject {
	
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
		listeners.Invoke();
	}
	
}

}