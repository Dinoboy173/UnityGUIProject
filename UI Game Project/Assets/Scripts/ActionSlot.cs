using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSlot : MonoBehaviour
{
    protected DropArea dropArea;

    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.onDropHandler += OnItemDropped;
        dropArea.DropConditions.Add(new ActionSlotDropCondition(this));
    }

    private void OnItemDropped(DraggableComponent draggable)
    {
        draggable.transform.SetParent(transform);
        draggable.transform.localPosition = Vector3.zero;
    }

    public class ActionSlotDropCondition : DropCondition
    {
        public ActionSlot slot;

        public ActionSlotDropCondition(ActionSlot actionSlot)
        {
            slot = actionSlot;
        }

        public override bool Check(DraggableComponent draggable)
        {
            return slot.transform.childCount == 0;
        }
    }
}