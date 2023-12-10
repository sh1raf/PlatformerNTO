using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class PlayerHealthLogic : MonoBehaviour
{
    [SerializeField] private float timeToRespawn;

    public Vector2 LevelPoint;
    public Vector2 CurrentCheckPoint;
    [Inject] private PlayerPCInput _input;

    private void Awake()
    {
        if(CurrentCheckPoint == null)
            CurrentCheckPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER");
        if(collision.tag == "RedZone")
        {
            StartCoroutine(Respawn());
            Debug.Log("YOU ARE DIE");
        }
        else if (collision.tag == "CheckPoint")
        {
            CurrentCheckPoint = collision.transform.position;
        }
        else if (collision.tag == "LevelPoint")
            LevelPoint = collision.transform.position;
    }
    private IEnumerator Respawn()
    {
        _input.DisableInput();

        yield return new WaitForSeconds(2f);

        _input.EnableInput();
        transform.position = CurrentCheckPoint;
    }
}
