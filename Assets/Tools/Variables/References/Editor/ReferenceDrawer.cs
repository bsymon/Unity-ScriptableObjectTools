using UnityEngine;
using UnityEditor;

namespace Game.Tools.Variables {

abstract public class ReferenceDrawer : PropertyDrawer {
	
	float LineHeight {
		get { return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; }
	}
	
	bool fold;
	
	// -- //
	
	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		SerializedProperty typeProp     = property.FindPropertyRelative("_referenceType");
		SerializedProperty variableProp = property.FindPropertyRelative("_variable");
		SerializedProperty valueProp    = property.FindPropertyRelative("_value");
		
		Rect boxPosition  = position;
		Rect itemPosition = position;
		boxPosition.height  = LineHeight * (fold ? 3f : 1f) + 10f;
		itemPosition.height = LineHeight;
		itemPosition.xMax  -= 5f;
		itemPosition.y     += 5f;
		
		GUIStyle foldoutStyle  = EditorStyles.foldout;
		foldoutStyle.fontStyle = FontStyle.Bold;
		
		GUI.Box(boxPosition, "");
		
		EditorGUI.indentLevel = 1;
		fold = EditorGUI.Foldout(itemPosition, fold, property.displayName, foldoutStyle);
		
		if(fold) {
			itemPosition.y += LineHeight;
			EditorGUI.PropertyField(itemPosition, typeProp);
			
			switch(typeProp.enumValueIndex) {
				case (int) Type.Direct:
					DrawProperty(itemPosition, valueProp);
					break;
				case (int) Type.VariableObject:
					DrawProperty(itemPosition, variableProp);
					break;
			}
		}
	}
	
	override public float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return LineHeight * (fold ? 3f : 1f) + 10f;
	}
	
	// -- //
	
	void DrawProperty(Rect position, SerializedProperty property) {
		position.y     += LineHeight;
		position.height = EditorGUIUtility.singleLineHeight;
		
		EditorGUI.PropertyField(position, property);
	}
	
}

}