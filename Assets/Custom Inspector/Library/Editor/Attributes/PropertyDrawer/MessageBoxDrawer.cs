using UnityEngine;
using UnityEditor;
using CustomInspector.Extensions;

namespace CustomInspector.Editor
{
    /// <summary>
    /// Draws the field name and behind a custom errorMessage
    /// </summary>
    [CustomPropertyDrawer(typeof(MessageBoxAttribute))]
    public class MessageBoxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MessageBoxAttribute hv = (MessageBoxAttribute)attribute;

            position.y += DrawProperties.errorSpacing;
            Rect messBoxR = EditorGUI.IndentedRect(position);
            messBoxR.height = hv.height;
            using (new NewIndentLevel(0))
            {
                EditorGUI.HelpBox(messBoxR, hv.content, MessageBoxConvert.ToUnityMessageType(hv.type));
            }
            position.y += messBoxR.height;
            position.height -= DrawProperties.errorSpacing + messBoxR.height;
            DrawProperties.PropertyField(position, label, property);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MessageBoxAttribute hv = (MessageBoxAttribute)attribute;
            return DrawProperties.errorSpacing + hv.height + DrawProperties.GetPropertyHeight(label, property);
        }
    }
}