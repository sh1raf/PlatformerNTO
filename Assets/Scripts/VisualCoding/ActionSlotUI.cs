using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSlotUI : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData) 
	{
		GameObject dropped = eventData.pointerDrag;
		BlocksUI blocksUI = dropped.GetComponent<BlocksUI>();
		blocksUI.parentAfterDrag = transform;
	}
}
