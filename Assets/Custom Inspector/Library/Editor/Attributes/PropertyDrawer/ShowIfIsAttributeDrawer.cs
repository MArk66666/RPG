using CustomInspector.Extensions;
using UnityEditor;
using UnityEngine;

namespace CustomInspector.Editor
{

    [CustomPropertyDrawer(typeof(ShowIfIsAttribute))]
    public class ShowIfIsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfIsAttribute sa = (ShowIfIsAttribute)attribute;

            property.serializedObject.ApplyModifiedPropertiesWithoutUndo();

            object refValue = DirtyValue.GetOwner(property).FindRelative(sa.fieldPath).GetValue();

            if((refValue.IsUnityNull() && sa.value.IsUnityNull())
                || (!refValue.IsUnityNull() && refValue.Equals(sa.value)))
            {
                //Show
                position.height = DrawProperties.GetPropertyHeight(label, property);
                using (new EditorGUI.IndentLevelScope(1))
                {
                    DrawProperties.PropertyField(position, label: label, property: property);
                }
                return;
            }
            else
            {
                //Hide
                return;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfIsAttribute sa = (ShowIfIsAttribute)attribute;


            object refValue = DirtyValue.GetOwner(property).FindRelative(sa.fieldPath).GetValue();

            if ((refValue == null && sa.value == null)
                || (refValue != null && refValue.Equals(sa.value)))
            {
                //Show
                return DrawProperties.GetPropertyHeight(label, property);
            }
            else
            {
                //Hide
                return -EditorGUIUtility.standardVerticalSpacing;
            }
        }
    }
}

