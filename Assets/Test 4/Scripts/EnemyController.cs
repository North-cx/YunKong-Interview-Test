using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float minX, maxX, minZ, maxZ; // 场景中可移动的位置范围

    public UnityEvent OnAttack1;

    public event Action OnAttack2;

    public delegate void AttackAction();
    public event AttackAction OnAttack3;

    private void Start()
    {
        if (OnAttack1 == null)
        {
            OnAttack1 = new UnityEvent();

            OnAttack1.AddListener(HandleAttack1);
        }
    }

    private void OnEnable()
    {
        OnAttack2 += HandleAttack2;
        OnAttack3 += HandleAttack3;
    }

    private void OnDisable()
    {
        OnAttack2 -= HandleAttack2;
        OnAttack3 -= HandleAttack3;
    }

    public void OnAttack()
    {
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.25f, UnityEngine.Random.Range(minZ, maxZ));
        transform.position = randomPosition;

        if (OnAttack1 != null)
        {
            OnAttack1.Invoke();
        }

        if (OnAttack2 != null)
        {
            OnAttack2.Invoke();
        }

        if (OnAttack3 != null)
        {
            OnAttack3.Invoke();
        }

        // Unity Console
        Debug.Log("BlueCube received an attack.");
    }

    public void HandleAttack1()
    {
        Debug.Log("UnityEvent: Attacked");
    }

    void HandleAttack2()
    {
        Debug.Log("Event: Attacked");
    }

    void HandleAttack3()
    {
        Debug.Log("Delegate: Attacked");
    }
}
