using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tools.Sets {

abstract public class Set<T> : ScriptableObject, IEnumerable<T> {
	
	public int Length {
		get { return set.Count; }
	}
	
	bool lockRemove;
	public bool LockRemove {
		get { return lockRemove; }
		set {
			lockRemove = value;
			
			if(!lockRemove) {
				for(int i = 0; i < pendingRemove.Count; ++i) {
					set.Remove(pendingRemove[i]);
				}
				
				pendingRemove.Clear();
			}
		}
	}
	
	// -- //
	
	[SerializeField] T[] defaultSet;
	[SerializeField] int capacity;
	
	List<T> set = new List<T>();
	List<T> pendingRemove = new List<T>();
	
	// -- //
	
	void OnEnable() {
		set.Clear();
		pendingRemove.Clear();
		
		if(defaultSet != null) {
			for(int i = 0; i < defaultSet.Length; ++i) {
				Add(defaultSet[i]);
			}
		}
	}
	
	// -- //
	
	public bool Add(T toAdd) {
		if(capacity > 0 && Length == capacity) {
			Debug.LogWarning(string.Format("Set \"{0}\" : Exceeded capacity (maximum {1}), can not add \"{2}\"", name, capacity, toAdd), toAdd as Object);
			return false;
		}
		
		set.Add(toAdd);
		return true;
	}
	
	public bool SetAt(T toSet, int index) {
		if(index >= set.Count) {
			if(capacity == 0) {
				Add(toSet);
			} else {
				return false;
			}
		}
		
		set[index] = toSet;
		return true;
	}
	
	public bool Remove(T toDelete) {
		if(LockRemove) {
			pendingRemove.Add(toDelete);
			return true;
		}
		
		return set.Remove(toDelete);
	}
	
	public T GetAt(int index) {
		return set[index];
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