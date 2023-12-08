using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : ActionObject
{
    [SerializeField] private bool isActive;

    [SerializeField] private Transform wind;
    [SerializeField] private List<float> angles = new();

    private int _currentAngleIndex = 0;
    public override void Action()
    {
        if(isActive)
        {
            ChangeDirection();
        }
        else
        {

        }
    }

    private void ChangeDirection()
    {
        if (_currentAngleIndex + 1 < angles.Count)
            _currentAngleIndex++;
        else
            _currentAngleIndex = 0;
        wind.eulerAngles = new Vector3(0,0, angles[_currentAngleIndex]);
    }
}
