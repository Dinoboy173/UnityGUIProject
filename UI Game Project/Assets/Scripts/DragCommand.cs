using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCommand : DraggableComponent
{
    public PlayerController.ActionType actionType;

    public override void Awake()
    {
        base.Awake();
        onEndDragHandler += DragStopped;
    }

    private void DragStopped(PointerEventData eventData, bool dropSuccess)
    {
        if (startParent.GetComponent<CommandSlot>() != null && !dropSuccess)
        {
            DestroyImmediate(gameObject);

            startParent.GetComponentInParent<MainFuncControl>()?.UpdatePlayerCommandList();
        }
    }
}