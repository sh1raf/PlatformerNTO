using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionsUI : MonoBehaviour//, IDropHandler
{	
	[SerializeField]
	int actionsCount;
	[SerializeField]
	GameObject prefab;
	
	Transform blockHolder;
	TextMeshProUGUI actionCounter;
	ActionQueue.action action;
	Sprite sprite;
	GameObject block;
	
	private void Awake()
	{
		actionCounter = gameObject.transform.Find("__CountPanel").GetComponentInChildren<TextMeshProUGUI>();
		blockHolder = transform.Find("__BlockHolder");
		action = blockHolder.GetComponentInChildren<BlocksUI>().action;
		sprite = blockHolder.GetComponentInChildren<Image>().sprite;
		UpdateCounterDisplay(0);
	}
	
	public void OnUnattach() 
	{
		UpdateCounterDisplay(-1);
		if (actionsCount > 0) 
		{
			block = Instantiate(prefab, transform.position, Quaternion.identity);
			block.transform.SetParent(blockHolder);
			block.GetComponent<BlocksUI>().action = action;
			block.GetComponent<Image>().sprite = sprite;
			block.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
		}
	}
	
	//public void OnDrop(PointerEventData eventData) 
	//{
	//	//if (actionsCount == 0) 
	//	//{
	//	//	block = Instantiate(prefab);
	//	//	block.transform.SetParent(blockHolder);
	//	//	block.GetComponent<BlocksUI>().action = action;
	//	//	block.GetComponent<Image>().sprite = sprite;
	//	//	block.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
	//	//}
	//	if (eventData.pointerDrag.GetComponent<BlocksUI>().action == action)
	//	{
	//		UpdateCounterDisplay(1);
	//		if (block) Destroy(block);
	//		GameObject dropped = eventData.pointerDrag;
	//		BlocksUI blocksUI = dropped.GetComponent<BlocksUI>();
	//		blocksUI.parentAfterDrag = blockHolder;
	//		blockHolder.transform.position = transform.position;
	//		eventData.pointerDrag.GetComponent<BlocksUI>().firstDrag = true;
	//	}
	//}
	
	//public void OnUnattach() 
	//{
	//	UpdateCounterDisplay(-1);
	//	if (actionsCount <= 0) return;
	//	block = Instantiate(prefab);
	//	block.transform.SetParent(blockHolder);
	//	block.GetComponent<BlocksUI>().action = action;
	//	block.GetComponent<Image>().sprite = sprite;
	//	block.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
	//}
	//
	//public void OnRollback(GameObject newBlock) 
	//{
	//	UpdateCounterDisplay(1);
	//	Destroy(block);
	//	block = newBlock;
	//}
	
	void UpdateCounterDisplay(int value) 
	{
		actionsCount += value;
		actionCounter.text = actionsCount.ToString();
	}
}