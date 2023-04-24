using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SerializableDictionary<TKey, TVaule> : Dictionary<TKey,TVaule>, ISerializationCallbackReceiver
{
    public List<TKey> InspectorKeys;
    public List<TVaule> InspectorValues;

    public SerializableDictionary()
    {
        InspectorKeys = new List<TKey>();
        InspectorValues = new List<TVaule>();
        SyncInspectorFromDictionary();
    }

    public new void Add(TKey key , TVaule value)
    {
        base.Add(key, value);
        SyncInspectorFromDictionary();
    }

    public new void Remove(TKey key)
    {
        base.Remove(key);
        SyncInspectorFromDictionary();
    }

    public void OnBeforeSerialize() { }
    public void SyncInspectorFromDictionary()               //��ųʸ��� �ִ°��� �ν����� â�� ��ũ�� ���ߴ� �Լ�
    {
        InspectorKeys.Clear();
        InspectorValues.Clear();

        foreach(KeyValuePair<TKey, TVaule> pair in this)
        {
            InspectorKeys.Add(pair.Key);
            InspectorValues.Add(pair.Value);
        }
    }

    public void SyncDictionaryFromInspector()                   //�ν����Ϳ� �ִ°��� ��ųʸ��� ��ũ�� ����
    {
        foreach (var key in Keys.ToList())
        {
            base.Remove(key);
        }

        for(int i = 0;i < InspectorKeys.Count; i++)
        {
            if(this.ContainsKey(InspectorKeys[i]))
            {
                Debug.LogError("�ߺ� Ű�� �ֽ��ϴ�. ");
                break;
            }

            base.Add(InspectorKeys[i], InspectorValues[i]); 
        }        
    }
    
    public void OnAfterDeserialize()
    {
        if (InspectorKeys.Count == InspectorValues.Count)
        {
            SyncDictionaryFromInspector();
        }
    }
}
