using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Manager() { }
    public static Manager Instance { get; private set; }

    public PoolManager Pool = new PoolManager();
    public ResourceManager Resource = new ResourceManager();
    public ObjectManager Object = new ObjectManager();

    private void Awake()                            //�̱����̱� ������ ó�����ִ� �͵�
    {
        if (Instance != null)
        {
            Destroy(gameObject);                    //���� ��ü ����
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);          //�� �̵��� �ı� ���� �ʰ� �ϱ� ���ؼ�
        }
    }

}