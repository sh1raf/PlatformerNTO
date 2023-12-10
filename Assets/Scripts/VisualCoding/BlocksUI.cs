using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class BlocksUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	[HideInInspector]
	public Transform parentAfterDrag;
	public ActionQueue.action action;
	public bool firstDrag = true;
	Image image;

	[Inject] private PlayerPCInput _input;
	
	private void Awake() {
		image = gameObject.GetComponent<Image>();
	}

    private void OnEnable()
    {
		if (_input != null)
			return;
		_input = FindObjectOfType<PlayerPCInput>();
    }

    public void OnBeginDrag(PointerEventData eventData) 
	{
		if (firstDrag)
		{
			transform.parent.parent.GetComponent<ActionsUI>().OnUnattach();
			firstDrag = false;
		}
		transform.SetParent(transform.root);
		transform.SetAsLastSibling();
		parentAfterDrag = transform;
		image.raycastTarget = false;
	}
	
	public void OnDrag(PointerEventData eventData) 
	{
		var pos = Camera.main.ScreenToWorldPoint(_input.Input.Player.MousePosition.ReadValue<Vector2>());
		transform.position = new Vector3(pos.x, pos.y, 0);
		Debug.Log($"{pos} {transform.position}");
	}
	
	public void OnEndDrag(PointerEventData eventData) 
	{
		transform.SetParent(parentAfterDrag);
		image.raycastTarget = true;
	}
}
