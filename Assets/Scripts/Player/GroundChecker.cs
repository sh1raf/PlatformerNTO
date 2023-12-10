using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GroundChecker : MonoBehaviour
{
    [Inject] private PlayerMovement _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "Box")
            _player.Grounded();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "Box")
            _player.Ungrounded();
    }
}
