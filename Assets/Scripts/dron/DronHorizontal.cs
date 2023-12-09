using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronHorizontal : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _rb;

    private bool _isMoving;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        MoveRight();
    }

    private IEnumerator Moving(Vector2 direction)
    {
        while(_isMoving)
        {
            _rb.velocity = new Vector2(direction.x * speed, _rb.velocity.y);
            yield return null;
        }
    }

    public void MoveLeft()
    {
        _isMoving = true;
        StartCoroutine(Moving(-Vector2.right));
    }

    public void MoveRight()
    {
        _isMoving = true;
        StartCoroutine(Moving(Vector2.right));
    }

    public void Stop()
    {
        _isMoving = false;
    }
}
