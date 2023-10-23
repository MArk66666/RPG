using System;
using UnityEditor;

namespace CustomInspector.Extensions
{
    public class NewIndentLevel : IDisposable
    {
        readonly int prevIndentLevel;
        public NewIndentLevel()
        {
            prevIndentLevel = EditorGUI.indentLevel;
        }
        public NewIndentLevel(int indentLevel)
        {
            prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = indentLevel;
        }

        public void Dispose()
        {
            EditorGUI.indentLevel = prevIndentLevel;
        }
    }
}
