using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    void Start()
    {
        // 初始化一个随机方向
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        // 按照速度在当前方向上移动
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 获取碰撞接触点的法线，并将其投影到 x-z 平面
        Vector3 normal = collision.contacts[0].normal;
        normal.y = 0;
        normal.Normalize();

        // 计算反射方向，并将其归一化
        direction = Vector3.Reflect(direction, normal).normalized;
    }
}
