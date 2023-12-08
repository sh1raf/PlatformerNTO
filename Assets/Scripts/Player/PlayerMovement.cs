using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float slipTime = 0.15f;
    [SerializeField, Range(0f, 1f)] private float smoothness = 0.1f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpAcceleration;

    [SerializeField] private float jumpChargeTime = 0.5f;

    private Rigidbody2D _rb;

    private bool _isGrounded;

    private float _expiredTime;

    private Animator _animator;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 direction)
    {
         _rb.velocity = Vector2.Lerp(_rb.velocity, new Vector2(direction.x * speed, _rb.velocity.y), smoothness);
        if (_rb.velocity.x > 0f && transform.localScale.x == -1)
            transform.localScale = new Vector3(1, 2, 1);
        else if(_rb.velocity.x < 0f && transform.localScale.x == 1f)
            transform.localScale = transform.localScale = new Vector3(-1, 2, 1);
        _animator.SetFloat("Move", direction.magnitude);
    }

    public void Jump()
    {
        if(_isGrounded)
        {
            _expiredTime = 0;
            _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }

        Debug.Log("JUMP");
    }

    public void ChargeJumping()
    {
        if (_rb.velocity.y > 0 && _expiredTime <= jumpChargeTime)
        {
            _rb.AddForce(Vector2.up * jumpAcceleration);
            _expiredTime += Time.fixedDeltaTime;
            Debug.Log("Charging");
        }
        else if (_rb.velocity.y < 0)
            _expiredTime = 0;

    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "floor")
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "floor")
            _isGrounded = false;
    }
}


