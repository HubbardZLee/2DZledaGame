using UnityEditor;
using UnityEngine;

/*
 * Creates a ReadOnly attribute
 * Put [ReadOnly] before a variable in conjunction with [SerializeField] to make the variable appear
 * in the inspector for viewing, but not allow the user to change it.
 */
[CustomPropertyDrawer(typeof(ReadOnly))]
public class ReadOnlyDrawer : PropertyDrawer 
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
