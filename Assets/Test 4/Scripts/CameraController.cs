using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;   // ��ɫ�� Transform
    public Vector3 offset;     // ��������ɫ֮���ƫ����

    void LateUpdate()
    {
        // �����������λ��
        transform.position = player.position + offset;

        // ��ѡ��ʹ�����ʼ�ճ����ɫ
        transform.LookAt(player);
    }
}
