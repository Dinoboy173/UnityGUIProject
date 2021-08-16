using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public List<DropCondition> DropConditions = new List<DropCondition>();
    public event Action<DraggableComponent> onDropHandler;

    public bool Accetps(DraggableComponent draggable)
    {
        return DropConditions.TrueForAll(cond => cond.Check(draggable));
    }

    public void Drop(DraggableComponent draggable)
    {
        onDropHandler?.Invoke(draggable);
    }
}
