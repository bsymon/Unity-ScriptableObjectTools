using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Variables.Timeline {

[TrackColor(0.3780236f, 0.682353f, 0f)]
[TrackClipType(typeof(GameObjectActivationClip))]
[TrackBindingType(typeof(GameObjectVariable))]
public class GameObjectActivationTrack : TrackAsset {
	
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
		return ScriptPlayable<GameObjectActivationMixerBehaviour>.Create(graph, inputCount);
	}
	
}

}