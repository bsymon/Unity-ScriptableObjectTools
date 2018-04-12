using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tools.Events {

public class EventListener : MonoBehaviour {
	
	public GameEvent EventToListen {
		get { return _eventToListen; }
		set {
			_eventToListen = value;
			_eventToListen.AddListener(OnEvent);
		}
	}
	
	// -- //
	
	public GameEvent _eventToListen;
	public GameObject _expectedInvoker;
	public List<GameObject> _expectedInvokers;
	public bool _rewaitInvokersAfterTrigger;
	public bool _allowChildren;
	public UnityEvent _actions;
	
	// -- //
	
	bool[] invokersDoneState;
	bool allInvokersDone;
	
	// -- //
	
	void Awake() {
		if(_eventToListen != null) {
			EventToListen = _eventToListen;
		}
	}
	
	void OnDestroy() {
		if(_eventToListen != null) {
			_eventToListen.RemoveListener(OnEvent);
			_actions.RemoveAllListeners();
		}
	}
	
	// -- //
	
	void IsInvokersDone(GameObject go) {
		if(allInvokersDone) {
			if(!_rewaitInvokersAfterTrigger) {
				return;
			}
			
			allInvokersDone = false;
		}
		
		if(_expectedInvokers.Count > 0 && invokersDoneState == null) {
			invokersDoneState = new bool[_expectedInvokers.Count];
		}
		
		int id = _expectedInvokers.IndexOf(go);
		
		if(id > -1) {
			invokersDoneState[id] = true;
			
			for(int i = 0; i < invokersDoneState.Length; ++i) {
				if(!invokersDoneState[i]) {
					allInvokersDone = false;
					break;
				}
				
				allInvokersDone = true;
			}
		}
	}
	
	bool IsInvoker(GameObject go) {
		IsInvokersDone(go);
		
		// TODO voir pour utiliser Linq
		return _expectedInvoker == null && _expectedInvokers.Count == 0
			|| go == _expectedInvoker
			|| allInvokersDone
		;
	}
	
	// -- //
	
	void OnEvent() {
		if(IsInvoker(_eventToListen.Invoker)
		  || _allowChildren && _eventToListen.Invoker.transform.IsChildOf(_expectedInvoker.transform)
		) {
			_actions.Invoke();
		}
	}
	
}

}