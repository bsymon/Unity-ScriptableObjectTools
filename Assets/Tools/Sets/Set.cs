using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tools.Sets {

abstract public class Set<T> : ScriptableObject, IEnumerable<T> {
	
	public int Length {
		get { return set.Count; }
	}
	
	// -- //
	
	[SerializeField] T[] defaultSet;
	[SerializeField] int capacity;
	
	HashSet<T> set = new HashSet<T>();
	
	// -- //
	
	void OnEnable() {
		if(defaultSet == null) {
			return;
		}
		
		for(int i = 0; i < defaultSet.Length; ++i) {
			Add(defaultSet[i]);
		}
	}
	
	// -- //
	
	public bool Add(T toAdd) {
		if(capacity > 0 && Length == capacity) {
			Debug.LogWarning(string.Format("Set \"{0}\" : Exceeded capacity (maximum {1}), can not add \"{2}\"", name, capacity, toAdd), toAdd as Object);
			return false;
		}
		
		return set.Add(toAdd);
	}
	
	public bool Remove(T toDelete) {
		return set.Remove(toDelete);
	}
	
	// -- //
	
	public IEnumerator<T> GetEnumerator() {
		return set.GetEnumerator();
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}
	
}

}