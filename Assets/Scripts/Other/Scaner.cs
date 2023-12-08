using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    public int ScanDelay;

    [SerializeField] private Door door;
    [SerializeField] private TMP_Text text;

    private List<Rigidbody2D> _collisions = new List<Rigidbody2D>();

    private void Awake()
    {
        StartScan();
    }

    public void StartScan()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        for(int i = 0; i < ScanDelay; i++)
        {
            text.text = (ScanDelay - i).ToString();
            yield return new WaitForSeconds(1);
        }

        text.text = "";

        if(_collisions.Count > 0)
        {
            Activate();
        }
    }

    private void Activate()
    {
        door.Open();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>().mass > 15)
            _collisions.Add(collision.GetComponent<Rigidbody2D>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>().mass > 15)
            _collisions.Remove(collision.GetComponent<Rigidbody2D>());
    }
}
