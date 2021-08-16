using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSlot : MonoBehaviour
{
    protected DropArea dropArea;

    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.onDropHandler += OnItemDropped;
        dropArea.DropConditions.Add(new CommandSlotDropCondition(this));
    }

    private void OnItemDropped(DraggableComponent draggable)
    {
        if (draggable.startParent.GetComponent<ActionSlot>() != null)
        {
            var obj = Instantiate(draggable.gameObject, draggable.startParent.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
        }

        draggable.transform.SetParent(transform);
        draggable.transform.localPosition = Vector3.zero;
    }

    public class CommandSlotDropCondition : DropCondition
    {
        public CommandSlot slot;

        public CommandSlotDropCondition(CommandSlot commandSlot)
        {
            slot = commandSlot;
        }

        public override bool Check(DraggableComponent draggable)
        {
            return slot.transform.childCount == 0;
        }
    }
}