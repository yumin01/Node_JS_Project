using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseScene), true)]         //�ڽ� Ŭ�������� ���� �ϱ� ���ؼ� ���
public class BaseSceneInspector : Editor                //�����͸� ���
{
    BaseScene _editor;

    void OnEnable()
    {
        _editor = target as BaseScene;
    }

    public override void OnInspectorGUI()       //���� ���� ��
    {
        _editor.sceneIndex = EditorGUILayout.Popup(_editor.sceneIndex, new string[] { "������ 0", "������ 1" });
        _editor.sceneDataShow = EditorGUILayout.Toggle(new GUIContent("������ ����"), _editor.sceneDataShow);

        switch(_editor.sceneIndex)
        {
            case 0:
                _editor.sceneData0 = EditorGUILayout.TextField(new GUIContent("������ 0"), _editor.sceneData0);
                if(_editor.sceneDataShow)
                {
                    _editor.sceneIndex = EditorGUILayout.IntField(new GUIContent("SceneIndex"), _editor.sceneIndex);
                }
                break;

            case 1:
                _editor.sceneData1 = EditorGUILayout.TextField(new GUIContent("������1"), _editor.sceneData1);
                break;

        }
    }
}
