using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Events.Timeline {

[TrackColor(0f, 1f, 0f)]
[TrackClipType(typeof(EventTriggerClip))]
public class EventTriggerTrack : TrackAsset {
	
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
		return ScriptPlayable<EventTriggerMixerBehaviour>.Create(graph, inputCount);
	}
	
}

}