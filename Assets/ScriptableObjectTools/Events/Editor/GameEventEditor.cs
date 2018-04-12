using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Tools.Events {

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor {
	
	static GameObject invoker;
	
	// -- //
	
	GameEvent gameEvent;
	SerializedProperty descriptionProp;
	string triggerButtonLabel;
	
	// -- //
	
	void OnEnable() {
		if(target != null) {
			gameEvent          = target as GameEvent;
			descriptionProp    = serializedObject.FindProperty("description");
			triggerButtonLabel = string.Format("Trigger \"{0}\"", gameEvent.name);
		}
	}
	
	override public void OnInspectorGUI() {
		serializedObject.Update();
		
		EditorGUILayout.PropertyField(descriptionProp);
		
		// Trigger the event
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Trigger the event", EditorStyles.boldLabel);
		
		invoker = EditorGUILayout.ObjectField("Invoker", invoker, typeof(GameObject), true) as GameObject;
		
		if(GUILayout.Button(triggerButtonLabel)) {
			gameEvent.Trigger(invoker);
		}
		
		serializedObject.ApplyModifiedProperties();
	}
	
}

}