using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();                //�θ� �ʱ�ȭ
        Debug.Log("LoginScene Init");//�α��� �� �ʱ�ȭ
    }

    protected override void Clear()
    {        
        Debug.Log("LoginScene Clear");
    }

}
