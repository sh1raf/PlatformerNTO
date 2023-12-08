using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float dampTime = 0.4f;

    private Vector3 _camPos;
    private Vector3 _velocity;

    private bool _isStatic;

    private void FixedUpdate()
    {
        if(!_isStatic)
        {
            _camPos = new Vector3(player.transform.position.x, player.transform.position.y + 2f, -10f);
            transform.position = Vector3.SmoothDamp(transform.position, _camPos, ref _velocity, dampTime);
        }
    }

    public void GoStatic()
    {

    }

    public void GoPlayer()
    {

    }
}
