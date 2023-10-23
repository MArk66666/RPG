using System;
using UnityEditor;
using UnityEngine;


namespace CustomInspector.Editor
{
    [CustomPropertyDrawer(typeof(MaskAttribute))]
    public class MaskAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            if(property.propertyType == SerializedPropertyType.Integer)
            {
                MaskAttribute m = (MaskAttribute)attribute;

                Rect labelRect = new(position)
                {
                    width = EditorGUIUtility.labelWidth,
                };
                EditorGUI.LabelField(labelRect, label);
                Rect toggleRect = new(position)
                {
                    x = labelRect.x + labelRect.width,
                    width = EditorGUIUtility.singleLineHeight,
                };

                int value = property.intValue;
                EditorGUI.BeginChangeCheck();
                for (int i = 0; i < m.bitsAmount; i++)
                {
                    bool res = EditorGUI.Toggle(toggleRect, (value & (1 << i)) != 0);
                    
                    if(res)
                    {
                        value |= 1 << i;
                    }
                    else
                    {
                        value &= ~(1 << i);
                    }

                    toggleRect.x += EditorGUIUtility.singleLineHeight;
                    //if out of view
                    if (toggleRect.x > position.x + position.width)
                        break;
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = value;
                }
            }
            else if(property.propertyType == SerializedPropertyType.Enum)
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorGUI.BeginChangeCheck();
                Enum res = EditorGUI.EnumFlagsField(position, (Enum)Enum.ToObject(fieldInfo.FieldType, property.intValue));
                if (EditorGUI.EndChangeCheck())
                    property.intValue = (int)Convert.ChangeType(res, typeof(int));
            }
            else
            {
                EditorGUI.HelpBox(position, "MaskAttribute only supports integers and enums", MessageType.Error);
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}