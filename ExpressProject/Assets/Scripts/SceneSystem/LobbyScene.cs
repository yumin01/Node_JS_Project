using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();                    //�θ� �ʱ�ȭ
        Debug.Log("LobbyScene Init");//�κ� �� �ʱ�ȭ
    }

    protected override void Clear()
    {
        Debug.Log("LobbyScene Clear");
    }

}
