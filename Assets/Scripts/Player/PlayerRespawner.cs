using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerRespawner : MonoBehaviour
{
    public Transform CurrentCheckPoint;
    private PlayerPCInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerPCInput>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "RedZone")
            StartCoroutine(Respawn());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint")
        {
            CurrentCheckPoint = collision.transform;
        }
    }
    private IEnumerator Respawn()
    {
        _input.DisableInput();

        yield return new WaitForSeconds(2f);

        _input.EnableInput();
        transform.position = CurrentCheckPoint.position;
    }
}
