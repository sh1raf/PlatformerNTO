using System;
using System.Collections;
using System.Collections.Generic;
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

    private bool _isActionEnded { get; set; } = true;

    private Queue<Action> _q = new();

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            _q.Enqueue(HorizontalStep);
        }

        for(int i = 0; i < 3; i++)
        {
            _q.Enqueue(VerticalStepBack);
        }
        _q.Enqueue(Grab);
        _q.Enqueue(VerticalStep);
        _q.Enqueue(UnGrab);

        StartCoroutine(StartActions());
    }

    public bool IsActionEnded() { return _isActionEnded; } 

    public void AddHorizontalStep()
    {
        _q.Enqueue(HorizontalStep);
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

        _q.Clear();
    }

    private IEnumerator Move(Vector2 targetPosition)
    {
        _isActionEnded = false;

        if(targetPosition.x >= horizontalStart.position.x || targetPosition.x <= horizontalEnd.position.x ||
            targetPosition.y <= verticalStart.position.y || targetPosition.y >= verticalEnd.position.y)
        {
            while(Vector2.Distance(transform.position, targetPosition) > 0.02f)
            {
                transform.position = Vector2.Lerp(transform.position, targetPosition, 0.5f * speed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPosition;

            yield return new WaitForSeconds(1f);
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
