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
                    elementHeight = 50,
                    drawElementCallback = (rect, index, active, focused) => {
                        var element = prop.GetArrayElementAtIndex(index);
                        var task = new SerializedObject(element.objectReferenceValue);
                        var textProp = task.FindProperty("_text");

                        EditorGUI.BeginChangeCheck();
                        rect.height = 15;
                        element.objectReferenceValue.name = EditorGUI.TextField(rect, "Title", element.objectReferenceValue.name);

                        rect.y += 17;
                        rect.height = 30;
                        textProp.stringValue = GUI.TextArea(rect, textProp.stringValue);
                        
                        if (EditorGUI.EndChangeCheck()) {
                            task.ApplyModifiedProperties();
                        }
                    },
                    onAddCallback = (list) => {
                        var task = CreateInstance<QuestTask>();
                        task.name = "Task";
                        
                        Undo.RegisterCreatedObjectUndo(task, "Created a new task");
                        
                        AssetDatabase.AddObjectToAsset(task, target);
                        AssetDatabase.SaveAssets();

                        prop.InsertArrayElementAtIndex(Mathf.Max(0, list.index));
                        prop.GetArrayElementAtIndex(list.index + 1).objectReferenceValue = task;
                    },
                    onRemoveCallback = (list) => {
                        var element = prop.GetArrayElementAtIndex(list.index);
                        var task = element.objectReferenceValue;
                        var questDefinition = target as QuestDefinition;

                        Undo.RecordObject(target, "Remove Task");
                        
                        questDefinition._tasks.RemoveAt(list.index);
                        Undo.RecordObject(task, "Remove Task");
                        
                        Undo.DestroyObjectImmediate(task);
                        
                        Undo.RecordObject(target, "Remove Task");
                        Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
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
