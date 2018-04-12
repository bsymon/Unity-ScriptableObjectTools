using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Variables.Timeline {

public class GameObjectActivationMixerBehaviour : PlayableBehaviour {
	
	GameObjectVariable trackBinding;
	bool variableBinded;
	bool defaultActiveState;
	bool firstDisabled;
	
	// -- //
	
	public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
		if(!trackBinding) {
			trackBinding = playerData as GameObjectVariable;
		}
		
		if(!trackBinding)
			return;
		
		variableBinded = trackBinding.Value != null;
		
		if(!variableBinded) {
			return;
		}
		
		if(!firstDisabled) {
			firstDisabled      = true;
			defaultActiveState = trackBinding.Value.activeInHierarchy;
			trackBinding.Value.SetActive(false);
		}
		
		int inputCount = playable.GetInputCount();
		
		for (int i = 0; i < inputCount; i++) {
			float inputWeight = playable.GetInputWeight(i);
			ScriptPlayable<GameObjectActivationBehaviour> inputPlayable = (ScriptPlayable<GameObjectActivationBehaviour>)playable.GetInput(i);
			GameObjectActivationBehaviour input = inputPlayable.GetBehaviour();
			
			if(inputWeight > 0 && !input.Active) {
				trackBinding.Value.SetActive(true);
				input.Active = true;
			} else if(inputWeight == 0 && input.Active) {
				trackBinding.Value.SetActive(false);
				input.Active = false;
			}
		}
	}
	
	public override void OnPlayableDestroy(Playable playable) {
		if(variableBinded) {
			trackBinding.Value.SetActive(defaultActiveState);
		}
	}
	
}

}