using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
	[SerializeField] private Manipulator manipulator;
	public enum action
	{
		CraneUp, CraneDown, CraneLeft, CraneRight, CraneGrab, CraneRelease, CraneUnGrab,
		FanLeft, FanRight, FanForce,
		PlatformToStart, PlatformToEnd, PlatformWait,
		DroneUp, DroneDown, DroneLeft, DroneRight
	}
	
	public action[] GetActionQueue() 
	{
		List<action> queue = new List<action>();

		for (int i = 0; i < transform.childCount; i++) 
		{
			Transform obj = transform.GetChild(i);
			if (obj.childCount == 0) continue;
			if (!obj.GetChild(0).GetComponent<BlocksUI>()) continue;
			queue.Add(obj.GetChild(0).GetComponent<BlocksUI>().action);
		}
		
		return queue.ToArray();
	}
	
	public void StartAlgorithm()
	{
		Debug.Log("AlgoStarts");

		action[] queue = GetActionQueue();
		for (int i = 0; i < queue.Length; i++)
		{
			switch(queue[i])
			{
				case action.CraneUp:
				{
					manipulator.AddVerticalStep();
					break;
				}
                case action.CraneDown:
                {
                    manipulator.AddVerticalStepBack();
                    break;
                }
                case action.CraneRight:
                {
                    manipulator.AddHorizontalStep();
                    break;
                }
                case action.CraneLeft:
                {
                    manipulator.AddHorizontalStepBack();
                    break;
                }
                case action.CraneGrab:
                {
                    manipulator.AddGrab();
                    break;
                }
                case action.CraneUnGrab:
                {
                    manipulator.AddUnGrab();
                    break;
                }
                default:
					break;
			}

		}
		StartCoroutine(manipulator.StartActions());
	}
}