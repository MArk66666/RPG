using CustomInspector.Extensions;
using UnityEditor;
using UnityEngine;

namespace CustomInspector.Editor
{   
    [CustomPropertyDrawer(typeof(HideFieldAttribute))]
    public class HideFieldAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.IsArrayElement()) //is element in a list
            {
                position.height = DrawProperties.errorHeight;
                EditorGUI.HelpBox(position, "Use [HideInInspector] to hide the whole list instead of just the elements", MessageType.Warning); //Pack lists in classes and show classes parallel
                return;
            }
            else
            {
                // nothing
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(property.IsArrayElement())
                return DrawProperties.errorHeight;
            else
                return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}

