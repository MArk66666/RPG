using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class Door : Entity, IInteractable
{
    [Header("General Setup")]
    [SerializeField] private Item key;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private Transform doorObject;

    private bool _open = false;
    private NavMeshObstacle _obstacle;
    private Coroutine _animationCorotuine;

    public static event Action<string> OnDoorInteracted;

    private void Start()
    {
        _obstacle = doorObject.GetComponent<NavMeshObstacle>();
    }

    public void Interact()
    {
        if (_animationCorotuine != null)
        {
            StopCoroutine(_animationCorotuine);
        }

        if (_open == true)
        {
            Close();
        }           
        else
        {
            Open();
        }

        OnDoorInteracted?.Invoke(EntityIDHolder.GetID());
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Vector3 initialRotation = doorObject.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < 2f)
        {
            transform.eulerAngles = Vector3.Lerp(initialRotation, targetRotation, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = targetRotation;
    }

    public void Open()
    {
        Vector3 targetRotation = new Vector3(0f, openAngle, 0f);
        _animationCorotuine = StartCoroutine(RotateDoor(targetRotation));
        _obstacle.enabled = false;
        _open = true;
    }

    public void Close()
    {
        Vector3 targetRotation = Vector3.zero;
        _animationCorotuine = StartCoroutine(RotateDoor(targetRotation));
        _obstacle.enabled = true;
        _open = false;
    }
}