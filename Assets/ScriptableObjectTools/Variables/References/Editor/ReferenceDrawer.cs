using UnityEngine;
using UnityEditor;

namespace Game.Tools.Variables {

abstract public class ReferenceDrawer : PropertyDrawer {
	
	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		SerializedProperty typeProp     = property.FindPropertyRelative("_referenceType");
		SerializedProperty variableProp = property.FindPropertyRelative("_variable");
		SerializedProperty valueProp    = property.FindPropertyRelative("_value");
		
		Rect itemPosition       = position;
		Rect switchTypePosition = position;
		
		itemPosition.xMax       -= 30f;
		switchTypePosition.xMin  = itemPosition.xMax + 3f;
		switchTypePosition.width = 25f;
		
		// Change the type of the Reference
		int currentType = typeProp.enumValueIndex;
		if(GUI.Button(switchTypePosition, currentType == 0 ? "D" : "V")) {
			typeProp.enumValueIndex = currentType == 0 ? 1 : 0;
		}
		
		DrawProperty(itemPosition, currentType == 0 ? valueProp : variableProp, property.displayName);
	}
	
	// -- //
	
	void DrawProperty(Rect position, SerializedProperty property, string label) {
		EditorGUI.PropertyField(position, property, new GUIContent(label));
	}
	
}

}