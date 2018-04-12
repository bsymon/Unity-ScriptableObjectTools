using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Events.Timeline {

public class EventTriggerMixerBehaviour : PlayableBehaviour {
	
	EventTriggerBehaviour lastEvent;
	
	// -- //
	
	public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
		int inputCount = playable.GetInputCount ();
		
		for (int i = 0; i < inputCount; i++) {
			float inputWeight = playable.GetInputWeight(i);
			ScriptPlayable<EventTriggerBehaviour> inputPlayable = (ScriptPlayable<EventTriggerBehaviour>)playable.GetInput(i);
			EventTriggerBehaviour input = inputPlayable.GetBehaviour();
			
			if(!input.Triggered && inputWeight > 0) {
				if(input.onStartPlaying != null) {
					Trigger(input.onStartPlaying, input.invoker);
					input.Triggered = true;
					lastEvent       = input;
				}
			} else if(input.Triggered && inputWeight == 0) {
				if(input.onFinishedPlaying != null) {
					Trigger(input.onFinishedPlaying, input.invoker);
				}
				
				input.Triggered = false;
				lastEvent       = null;
			}
		}
	}
	
	public override void OnPlayableDestroy(Playable playable) {
		// Allows to play the Finished Event if the clip was the last and the duration mode is "Base On Clips"
		if(lastEvent != null && lastEvent.onFinishedPlaying != null) {
			Trigger(lastEvent.onFinishedPlaying, lastEvent.invoker);
		}
	}
	
	// -- //
	
	void Trigger(GameEvent theEvent, GameObject invoker) {
		if(invoker != null) {
			theEvent.Trigger(invoker);
		} else {
			theEvent.Trigger();
		}
	}
	
}

}