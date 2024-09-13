using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;   // 角色的 Transform
    public Vector3 offset;     // 摄像机与角色之间的偏移量

    void LateUpdate()
    {
        // 设置摄像机的位置
        transform.position = player.position + offset;

        // 可选：使摄像机始终朝向角色
        transform.LookAt(player);
    }
}
