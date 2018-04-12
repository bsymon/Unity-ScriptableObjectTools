using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.Tools.Variables.Timeline {

[System.Serializable]
public class GameObjectActivationBehaviour : PlayableBehaviour {
	
	public bool Active { get; set; }
	
}

}