using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;
    public bool sceneDataShow;
    public string sceneData0, sceneData1;
    private string sceneType;

    public void Awake()
    {
        Init();
    }

    protected virtual void Init()           //virtual ���� base ���� �ְ� ����
    {

    }

    protected virtual void Clear()           //Scene ����ÿ� ���� �ؾ� �� �͵��� ���� �Լ�
    {

    }


}
