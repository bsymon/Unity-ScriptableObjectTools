using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Variables.Timeline {

[System.Serializable]
public class GameObjectActivationClip : PlayableAsset, ITimelineClipAsset {
	
	[HideInInspector] GameObjectActivationBehaviour template = new GameObjectActivationBehaviour();
	
	// -- //
	
	public ClipCaps clipCaps {
		get { return ClipCaps.None; }
	}
	
	// -- //
	
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
		var playable = ScriptPlayable<GameObjectActivationBehaviour>.Create(graph, template);
		GameObjectActivationBehaviour clone = playable.GetBehaviour();
		
		return playable;
	}
	
}

}