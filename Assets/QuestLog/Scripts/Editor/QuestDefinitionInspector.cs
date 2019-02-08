using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CleverCrow.QuestLogs.Editors {
    [CustomEditor(typeof(QuestDefinition))]
    public class QuestDefinitionInspector : Editor {
        private ReorderableList _list;
        
        public override void OnInspectorGUI () {
            if (_list == null) {
                var prop = serializedObject.FindProperty("_tasks");
                _list = new ReorderableList(serializedObject, prop) {
                    drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Tasks"),
                    elementHeight = 30,
                    drawElementCallback = (rect, index, active, focused) => {
                        var task = prop.GetArrayElementAtIndex(index);
                        var textProp = task.FindPropertyRelative("_text");
                        textProp.stringValue = GUI.TextArea(rect, textProp.stringValue);
                    }
                };
            }
            
            base.OnInspectorGUI();
            
            serializedObject.Update();
            _list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
