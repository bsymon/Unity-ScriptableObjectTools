using UnityEngine;

namespace Game.Tools.Variables {
	
public enum Type { Direct, VariableObject }

abstract public class Reference<TVar, TType> where TVar : Variable<TType> {
	
	public Type _referenceType;
	public TVar _variable;
	public TType _value;
	
	// -- //
	
	public TType Value {
		get { return _variable != null && _referenceType == Type.VariableObject ? _variable.Value : _value; }
		set {
			if(_variable != null && _referenceType == Type.VariableObject) {
				_variable.Value = value;
			} else {
				_value = value;
			}
		}
	}
	
	// -- //
	
	override public bool Equals(object other) {
		Reference<TVar, TType> obj = other as Reference<TVar, TType>;
		
		if(obj != null) {
			return obj == this;
		}
		
		return false;
	}
	
	override public int GetHashCode() {
		return _variable.GetHashCode() + _value.GetHashCode();
	}
	
	override public string ToString() {
		return _variable != null ? _variable.ToString() : _value.ToString();
	}
	
	// -- Operators -- //

	static public implicit operator TType(Reference<TVar, TType> reference) {
		return reference.Value;
	}
}

}