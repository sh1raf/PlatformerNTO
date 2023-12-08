using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : ActionObject
{
    [SerializeField] private Door door;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Action()
    {
        door.Open();
        _spriteRenderer.color = Color.black;
    }
}

public class ActionObject : MonoBehaviour
{
    public virtual void Action()
    {

    }
}
