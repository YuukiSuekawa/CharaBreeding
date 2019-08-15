using CharaBreeding.Util;
using UnityEditor;
using UnityEditor.UI;

namespace UnityEngine.UI
{
    [CanEditMultipleObjects,CustomEditor(typeof(MenuButton),true)]
    public class MenuButtonEditor : ButtonEditor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonType"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}