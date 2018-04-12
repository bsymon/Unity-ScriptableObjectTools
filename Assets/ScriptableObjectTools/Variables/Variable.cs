using UnityEngine;

namespace Game.Tools.Variables {

abstract public class Variable : ScriptableObject {
	
	abstract public object GenericValue();
	abstract public System.Type GetBindingType();
	
}

abstract public class Variable<T> : Variable {
	
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
	
	sealed override public object GenericValue() {
		return Value;
	}
	
	sealed override public System.Type GetBindingType() {
		return typeof(T);
	}
	
	// -- Operators -- //
	
	static public implicit operator T(Variable<T> variable) {
		return variable.Value;
	}
	
}

}