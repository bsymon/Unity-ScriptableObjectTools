using UnityEngine;

namespace Game.Tools.Sets {

public class AddGameObjectInSet : MonoBehaviour {
	
	[SerializeField] GameObjectSet _set;
	[SerializeField] bool _addOnAwake;
	
	// -- //
	
	bool inSet;
	
	// -- //
	
	void Awake() {
		if(_addOnAwake) {
			AddInSet();
		}
	}
	
	void OnDestroy() {
		RemoveFromSet();
	}
	
	// -- //
	
	public void AddInSet() {
		if(_set == null) {
			Debug.LogWarning(string.Format("Can not add GameObject \"{0}\" in set because it is null", gameObject.name), gameObject);
			return;
		}
		
		if(!inSet) {
			inSet = _set.Add(gameObject);
		}
	}
	
	public void RemoveFromSet() {
		if(inSet) {
			inSet = !_set.Remove(gameObject);
		}
	}
	
}

}