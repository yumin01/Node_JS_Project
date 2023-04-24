using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;                      //InputSystem ���Խ����̽� �߰�

public class PlayerInputSystem : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed;                     //�ӵ�

    public Rigidbody rigid;                 //���� �̵�

    private void FixedUpdate()
    {
        Vector3 nextVec = new Vector3(inputVec.x, 0.0f, inputVec.y);            //x,y ��ǥ�� (x,0,y)�� ��ȯ�ؼ� 3D �̵� ����
        rigid.MovePosition(rigid.position + nextVec * speed * Time.fixedDeltaTime);         

    }

    private void OnMove(InputValue value)           //InputValue �μ� ���� �����´�. 
    {
        inputVec = value.Get<Vector2>();            //Vector2������ �μ� ���� �����´�. 
    }
}
