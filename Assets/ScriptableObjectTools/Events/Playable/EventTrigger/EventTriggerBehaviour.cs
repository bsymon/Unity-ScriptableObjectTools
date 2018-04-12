using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Events.Timeline {

[System.Serializable]
public class EventTriggerBehaviour : PlayableBehaviour {
	
	public GameEvent onStartPlaying;
	public GameEvent onFinishedPlaying;
	public GameObject invoker;
	
	// -- //
	
	public bool Triggered { get; set; }
	
}

}