using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen { get { return _collider.isTrigger; } private set { } }

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = false;
    }

    public void Open()
    {
        _collider.isTrigger = true;
    }

    public void Close()
    {
        _collider.isTrigger=false;
    }
}
