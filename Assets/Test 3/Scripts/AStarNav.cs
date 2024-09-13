using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNav : MonoBehaviour
{
    public Camera mainCamera;

    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测左键点击
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标屏幕坐标
            mousePosition.z = mainCamera.nearClipPlane; // 设置 z 值为摄像机的近裁剪面

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // 转换为世界坐标
            Debug.Log("Mouse click position in world space: " + worldPosition);

            targetPosition = new Vector2(worldPosition.x, worldPosition.z);
        }
    }
}
