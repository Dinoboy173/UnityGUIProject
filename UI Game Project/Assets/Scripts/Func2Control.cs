using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func2Control : MonoBehaviour
{
    public PlayerController player;

    public void UpdatePlayerCommandList()
    {
        player.actions.Clear();
        var childDraggableCommand = GetComponentsInChildren<DragCommand>();

        foreach (var cmd in childDraggableCommand)
        {
            player.actions.Add(cmd.actionType);
        }
    }
}
