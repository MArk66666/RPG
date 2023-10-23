using CustomInspector.Extensions;
using CustomInspector.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomInspector.Editor
{
    [CustomPropertyDrawer(typeof(ShowAssetReferenceAttribute))]
    public class ShowAssetReferenceAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(property.propertyType == SerializedPropertyType.Generic)
            {
                //Get Asset Reference
                Object assetReference = GetAssetReference(property);

                if(assetReference != null)
                {
                    //label has to be drawn first bec first label gets parameter next time
                    Rect labelRect = EditorGUI.IndentedRect(position);
                    labelRect.height = EditorGUIUtility.singleLineHeight;
                    using (new NewIndentLevel(0))
                    {
                        labelRect.width = EditorGUIUtility.labelWidth; //make sure to not override the assetRef object field
                        EditorGUI.LabelField(labelRect, label);
                    }

                    //Show Asset Reference
                    Rect refRect = new(position)
                    {
                        height = EditorGUIUtility.singleLineHeight,
                    };
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUI.ObjectField(refRect, label: " ", obj: assetReference, objType: typeof(Object), allowSceneObjects: false);
                    }

                    //Draw Property
                    EditorGUI.PropertyField(position: position, label: GUIContent.none, property: property, includeChildren: true);
                }
                else
                {
                    DrawProperties.DrawFieldWithMessage(position, label, property, $"AssetReference: No asset with name \"{GetAssetName()}\" found", MessageType.Error);
                }
            }
            else
            {
                DrawProperties.DrawFieldWithMessage(position, label, property,
                    "[ShowAssetReference]-attribute is only valid on Generics (custom classes)", MessageType.Error);
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Generic)
            {
                if (GetAssetReference(property) != null)
                    return DrawProperties.GetPropertyHeight(label, property);
                else
                    return DrawProperties.GetPropertyWithMessageHeight(label, property);
            }
            else
            {
                return DrawProperties.GetPropertyWithMessageHeight(label, property);
            }
        }

        /// <summary> For performance reasons we save the asset reference </summary>
        readonly static Dictionary<PropertyIdentifier, Object> assetReferences = new();

        Object GetAssetReference(SerializedProperty property)
        {
            PropertyIdentifier id = new(property);
            if(!assetReferences.TryGetValue(id, out Object res))
            {
                TryGetAsset(GetAssetName(), out res);
                assetReferences.Add(id, res);
            }
            return res;

            static bool TryGetAsset(string assetName, out Object asset)
            {
                string[] guids = AssetDatabase.FindAssets(assetName);
                if (guids.Length < 1)
                {
                    asset = null;
                    return false;
                }
                asset = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guids[0]));
                return true;
            }
        }
        string GetAssetName() => ((ShowAssetReferenceAttribute)attribute).fileName ?? fieldInfo.FieldType.Name;
    }
}
