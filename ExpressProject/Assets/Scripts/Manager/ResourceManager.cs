using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;


public class ResourceManager
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();       //���ҽ� ����Ʈ ��� ������ 
    public Manager Manager => Manager.Instance;                                                             //�Ŵ����� ���ؼ� Object ������ �ϱ� ���� ����

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {key}");
            return null;
        }

        if (pooling)
            return Manager.Pool.Pop(prefab);

        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;
        return go;
    }

    public void Destory(GameObject go)
    {
        if (go == null)
            return;

        if (Manager.Pool.Push(go))
            return;

        Object.Destroy(go);
    }

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))                                                    //Ű ���� �˻��ؼ� 
        {
            if (typeof(T) == typeof(Sprite))                                                                     //��������Ʈ�� ��� 2D ���� ó�� 
            {
                Texture2D tex = resource as Texture2D;
                Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                return spr as T;
            }
            return resource as T;
        }
        return Resources.Load<T>(key);                                                                           // Ű�� ������Ʈ�� �����ش�. 
    }

    public void LoadAsync<T>(string key, Action<T> callbock = null) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out Object resource))                                                    //Ű ���� �˻��ؼ� 
        {
            callbock?.Invoke(resource as T);                                                                        //�ݹ� �˻��� ��ȯ
            return;
        }

        var asyncOperation = Addressables.LoadAssetAsync<T>(key);                                               //���ҽ� �ε�
        asyncOperation.Completed += (op) =>                                                                     //�Ϸ�Ǹ� �ݹ� �˻��ؼ� ��ȯ
        {
            _resources.Add(key, op.Result);
            callbock?.Invoke(op.Result);
        };

    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callbock) where T : UnityEngine.Object
    {
        var OpHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        OpHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;                                                                //�ε��� ī��Ʈ �÷��� �Ϸ� �Ҷ� ���� ���� 
                    callbock?.Invoke(result.PrimaryKey, loadCount, totalCount);

                });
            }
        };
    }
}