using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Manipulator : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform horizontalStart;
    [SerializeField] private Transform horizontalEnd;

    [SerializeField] private Transform verticalStart;
    [SerializeField] private Transform verticalEnd;

    [SerializeField] private float step;

    [SerializeField] private LayerMask grabLayers;

    private Vector3 _velocity;

    private bool _isActionEnded { get; set; } = true;

    private Queue<Action> _q = new();

    public event Action ActionsEnd, OnStartPoint;

    private Vector2 _startPoint;

    private void Awake()
    {
        _startPoint = transform.position;
    }

    public bool IsActionEnded() { return _isActionEnded; } 

    public void AddHorizontalStep()
    {
        _q.Enqueue(HorizontalStep);
    }

    public void AddHorizontalStepBack()
    {
        _q.Enqueue(HorizontalStepBack);
    }

    public void AddVerticalStep()
    {
        _q.Enqueue(VerticalStep);
    }
    public void AddVerticalStepBack()
    {
        _q.Enqueue(VerticalStepBack);
    }
    public void AddGrab()
    {
        _q.Enqueue(Grab);
    }

    public void AddUnGrab()
    {
        _q.Enqueue(UnGrab);
    }

    public IEnumerator StartActions()
    {

        foreach(var action in _q)
        {
            Debug.Log("NACH");
            yield return new WaitUntil(new Func<bool>(IsActionEnded));
            Debug.Log("End");

            action.Invoke();
        }

        yield return new WaitUntil(new Func<bool>(IsActionEnded));

        _q.Clear();
        StartCoroutine(GoStart());
        yield return new WaitForSeconds(3f);

        ActionsEnd?.Invoke();
    }

    private IEnumerator GoStart()
    {
        UnGrab();

        var expiredTime = 0f;

        while(Vector2.Distance(transform.position, _startPoint) > 0.1f && expiredTime <= 5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _startPoint, ref _velocity, 1f);
            expiredTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _startPoint;

        OnStartPoint?.Invoke();
    }

    private IEnumerator Move(Vector2 targetPosition)
    {
        _isActionEnded = false;

        if(targetPosition.x >= horizontalStart.position.x || targetPosition.x <= horizontalEnd.position.x ||
            targetPosition.y <= verticalStart.position.y || targetPosition.y >= verticalEnd.position.y)
        {
            while(Vector2.Distance(transform.position, targetPosition) > 0.02f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, 1f);
                yield return null;
            }

            transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("TargetPosition Out of bounds");
        }

        _isActionEnded = true;
    }


    public void HorizontalStep()
    {
        StartCoroutine(Move(transform.position + new Vector3(step, 0f, 0f)));
    }

    private void HorizontalStepBack()
    {
        StartCoroutine(Move(transform.position - new Vector3(step, 0f, 0f)));
    }

    public void VerticalStep()
    {
        StartCoroutine(Move(transform.position + new Vector3(0f, step, 0f)));
    }

    public void VerticalStepBack()
    {
        StartCoroutine(Move(transform.position - new Vector3(0f, step, 0f)));
    }

    public void Grab()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius, grabLayers);

        foreach(var collider in colliders)
        {
            collider.transform.parent = transform;
            if (collider.GetComponent<Rigidbody2D>())
                collider.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void UnGrab()
    {
        foreach(var child in GetComponentsInChildren<Collider2D>())
        {
            child.transform.parent = null;
            if (child.GetComponent<Rigidbody2D>())
                child.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
