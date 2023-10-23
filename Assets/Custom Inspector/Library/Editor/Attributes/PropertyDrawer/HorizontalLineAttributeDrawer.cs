using CustomInspector.Extensions;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace CustomInspector.Editor
{
    [CustomPropertyDrawer(typeof(HorizontalLineAttribute))]
    public class HorizontalLineAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HorizontalLineAttribute hl = (HorizontalLineAttribute)attribute;
            position.y += hl.spacing;

            float height;
            if (!string.IsNullOrEmpty(hl.message))
            {
                height = Math.Max(hl.thickness, EditorGUIUtility.singleLineHeight);
                //Message
                var info = GetInfo(hl.message);
                Rect messageRect = new(x: position.x + (position.width - info.Width) / 2,
                                       y: position.y + height / 2 - info.Height / 2,
                                       width: info.Width,
                                       height: EditorGUIUtility.singleLineHeight);

                using (new NewIndentLevel(0))
                {
                    EditorGUI.LabelField(messageRect, info.guiContent);
                }
                //Line
                //up and down
                if (hl.thickness > info.Height)
                {
                    Rect lineRect = new(x: messageRect.x,
                                        y: position.y,
                                        width: messageRect.width,
                                        height: (hl.thickness - info.Height) / 2);
                    EditorGUI.DrawRect(lineRect, ToColor(hl.color));
                    lineRect.y += lineRect.height + info.Height;
                    EditorGUI.DrawRect(lineRect, ToColor(hl.color));
                }
                //right and left
                if (messageRect.x > hl.gapSize) //enough space
                {
                    Rect lineRect = new(x: position.x + hl.gapSize,
                                        y: position.y + height / 2 - hl.thickness / 2,
                                        width: messageRect.x - position.x - hl.gapSize,
                                        height: hl.thickness);
                    EditorGUI.DrawRect(lineRect, ToColor(hl.color));
                    lineRect.x = messageRect.x + messageRect.width;
                    EditorGUI.DrawRect(lineRect, ToColor(hl.color));
                }
            }
            else //If there is ONLY the line (no string)
            {
                height = hl.thickness;
                //Line
                Rect lineRect = new(x: position.x + hl.gapSize,
                                    y: position.y,
                                    width: position.width - 2 * hl.gapSize,
                                    height: height);
                EditorGUI.DrawRect(lineRect, ToColor(hl.color));

            }

            //Property field
            height += hl.spacing + EditorGUIUtility.standardVerticalSpacing;
            Rect propRect = new(position)
            {
                y = position.y + height,
                height = position.height - (height + hl.spacing),
            };
            DrawProperties.PropertyField(propRect, label, property);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            HorizontalLineAttribute hl = (HorizontalLineAttribute)attribute;
            float lineSize;
            if (!string.IsNullOrEmpty(hl.message))
            {
                lineSize = Math.Max(hl.thickness, EditorGUIUtility.singleLineHeight) + 2 * hl.spacing;
            }
            else
                lineSize = hl.thickness + 2 * hl.spacing;

            return lineSize + EditorGUIUtility.standardVerticalSpacing + DrawProperties.GetPropertyHeight(label, property);
        }
        private Color ToColor(FixedColor c) => c.ToColor();

        static readonly Dictionary<string, TextInfo> cachedLabels = new();
        TextInfo GetInfo(string text)
        {
            if(!cachedLabels.TryGetValue(text, out TextInfo res))
            {
                res = new(text);
                cachedLabels.Add(text, res);
            }
            return res;
        }
        class TextInfo
        {
            public readonly GUIContent guiContent;
            public readonly Vector2 size;
            public float Width => size.x;
            public float Height => size.y;

            public TextInfo(string text)
            {
                guiContent = new(text);
                size = GUI.skin.label.CalcSize(guiContent);
            }
        }
    }
}