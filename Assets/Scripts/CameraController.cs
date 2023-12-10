using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private float speed;

    [SerializeField] private Transform player;
    [SerializeField] private float dampTime = 0.4f;

    private Vector3 _camPos;
    private Vector3 _velocity;

    private bool _isStatic;

    private void Awake()
    {
        canvas.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(!_isStatic)
        {
            _camPos = new Vector3(player.transform.position.x, player.transform.position.y + 2f, -10f);
            transform.position = Vector3.SmoothDamp(transform.position, _camPos, ref _velocity, dampTime);
        }
    }

    public void GoStatic(Vector2 position)
    {
        StartCoroutine(StaticMovement(position));
    }

    private IEnumerator StaticMovement(Vector2 targetPosition)
    {
        _isStatic = true;
        var expiredTime = 0f;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f && _isStatic && expiredTime <= 2f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, targetPosition.y, -10), ref _velocity, 1);
            expiredTime += Time.deltaTime;
            yield return null;
        }

        canvas.gameObject.SetActive(true);
    }

    public void GoPlayer()
    {
        _isStatic = false;
        canvas.gameObject.SetActive(false);
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        var expiredTime = 0f;

        while (Vector3.Distance(transform.position, player.position) > 1f && expiredTime <= 2f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y, -10), ref _velocity, 3);
            expiredTime += Time.deltaTime;
            yield return null;
        }
    }
}
