using UnityEngine;

namespace Game.Tools.Variables {

abstract public class Variable<T> : ScriptableObject {
	
	[SerializeField]
	[TextArea]
	private string Description;
	
	[SerializeField]
	private T DefaultValue;
	
	[HideInInspector]
	public T Value;
	
	// -- //
	
	void OnEnable() {
		Value = DefaultValue;
	}
	
	// -- //
	
	override public bool Equals(object other) {
		if(other is Variable<T>) {
			return ((Variable<T>) other).GetInstanceID() == GetInstanceID();
		}
		
		return false;
	}
	
	override public int GetHashCode() {
		return GetInstanceID();
	}
	
	override public string ToString() {
		return Value.ToString();
	}
	
	// -- Operators -- //
	
	static public implicit operator T(Variable<T> variable) {
		return variable.Value;
	}
	
}

}