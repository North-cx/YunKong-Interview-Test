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
        if (Input.GetMouseButtonDown(0)) // ���������
        {
            Vector3 mousePosition = Input.mousePosition; // ��ȡ�����Ļ����
            mousePosition.z = mainCamera.nearClipPlane; // ���� z ֵΪ������Ľ��ü���

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // ת��Ϊ��������
            Debug.Log("Mouse click position in world space: " + worldPosition);

            targetPosition = new Vector2(worldPosition.x, worldPosition.z);
        }
    }
}
