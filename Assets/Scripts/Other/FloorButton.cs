using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private Door door;

    private List<Rigidbody2D> _collisions = new();


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Rigidbody2D>().mass > 15 && !door.IsOpen)
        {
            door.Open();
        }
        else if(door.IsOpen)
            door.Close();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
            _collisions.Add(collision.GetComponent<Rigidbody2D>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
            _collisions.Remove(collision.GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        int big = 0;
        if(_collisions.Count > 0)
        {
            foreach(var collision in _collisions)
            {
                if(collision.mass > 15)
                    big++;
            }
        }

        if (door.IsOpen && big <= 0)
            door.Close();
        else if (!door.IsOpen && big > 0)
            door.Open();
    }
}
