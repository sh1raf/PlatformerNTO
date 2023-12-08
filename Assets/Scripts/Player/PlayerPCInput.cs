using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPCInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    private PlayerInput _input;

    private bool _isJumping = false;

    private bool _canAction;
    private ActionObject _currentAction;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Jump.performed += context => StartJump();
        _input.Player.Jump.canceled += context => EndJumping();
        _input.Player.Action.performed += context => Action();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void StartJump()
    {
        _player.Jump();
        _isJumping = true;
    }

    private void EndJumping()
    {
        _isJumping = false;
        Debug.Log("End");
    }

    public void EnableInput()
    {
        OnEnable();
    }

    public void Action()
    {
        if(_canAction && _currentAction != null)
        {
            _currentAction.Action();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ActionObject>())
        {
            _canAction = true;
            _currentAction = collision.GetComponent<ActionObject>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ActionObject>())
        {
            _canAction = false;
            _currentAction = null;
        }
    }

    public void DisableInput()
    {
        OnDisable();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= context => StartJump();
        _input.Player.Jump.canceled -= context => EndJumping();
        _input.Player.Action.performed -= context => Action();
    }

    private void FixedUpdate()
    {
        _player.Move(_input.Player.Move.ReadValue<Vector2>());

        if(_isJumping)
        {
            _player.ChargeJumping();
        }
    }
}
