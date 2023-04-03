using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseScene), true)]             // 자식 클래스에도 적용하기 위해서 사

public class BaseSceneInspector : Editor            //에디터를 상
{
    BaseScene _editor;

    void OnEnable()
    {
        _editor = target as BaseScene;
    }

    public override void OnInspectorGUI()           //실제 구현 
    {
        _editor.sceneIndex = EditorGUILayout.Popup(_editor.sceneIndex, new string[] { "데이터 0", "데이터 1" });
        _editor.sceneDataShow = EditorGUILayout.Toggle(new GUIContent("데이터 보기"), _editor.sceneDataShow);

        switch(_editor.sceneIndex)
        {
            case 0:
                _editor.sceneData0 = EditorGUILayout.TextField(new GUIContent("데이터 0"), _editor.sceneData0);
                if(_editor.sceneDataShow)
                {
                    _editor.sceneIndex = EditorGUILayout.IntField(new GUIContent("SceneIndex"), _editor.sceneIndex);
                }
                break;

            case 1:
                _editor.sceneData1 = EditorGUILayout.TextField(new GUIContent("데이터 1"), _editor.sceneData1);
                break;
        }
    }
}
