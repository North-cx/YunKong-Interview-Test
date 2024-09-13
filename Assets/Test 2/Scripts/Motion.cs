using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    void Start()
    {
        // ��ʼ��һ���������
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        // �����ٶ��ڵ�ǰ�������ƶ�
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // ��ȡ��ײ�Ӵ���ķ��ߣ�������ͶӰ�� x-z ƽ��
        Vector3 normal = collision.contacts[0].normal;
        normal.y = 0;
        normal.Normalize();

        // ���㷴�䷽�򣬲������һ��
        direction = Vector3.Reflect(direction, normal).normalized;
    }
}
