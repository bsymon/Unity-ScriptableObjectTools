using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Events.Timeline {

[System.Serializable]
public class EventTriggerClip : PlayableAsset, ITimelineClipAsset {
	
	[HideInInspector] public EventTriggerBehaviour template = new EventTriggerBehaviour();
	
	public GameEvent onStartPlaying;
	public GameEvent onFinishedPlaying;
	public ExposedReference<GameObject> invoker;
	
	// -- //
	
	public ClipCaps clipCaps {
		get { return ClipCaps.Blending; }
	}
	
	// -- //
	
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
		var playable = ScriptPlayable<EventTriggerBehaviour>.Create(graph, template);
		EventTriggerBehaviour clone = playable.GetBehaviour ();
		
		clone.onStartPlaying    = onStartPlaying;
		clone.onFinishedPlaying = onFinishedPlaying;
		clone.invoker           = invoker.Resolve(graph.GetResolver());
		
		return playable;
	}
	
}

}