using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFuncControl : MonoBehaviour
{
    public PlayerController player;

    public GameObject forward;
    public GameObject turnLeft;
    public GameObject turnRight;
    public GameObject toggleLight;
    public GameObject jump;
    public GameObject func1;
    public GameObject func2;

    void Start()
    {
        int nextChildIndex = 0;
        var children = GetComponentsInChildren<CommandSlot>();

        foreach (var action in player.actions)
        {
            if (nextChildIndex >= children.Length)
            {
                break;
            }

            var child = children[nextChildIndex];

            switch (action)
            {
                case PlayerController.ActionType.MOVE_FORWARD: Instantiate(forward, child.transform); break;
                case PlayerController.ActionType.ROTATE_LEFT: Instantiate(turnLeft, child.transform); break;
                case PlayerController.ActionType.ROTATE_RIGHT: Instantiate(turnRight, child.transform); break;
                case PlayerController.ActionType.TOGGLE_LIGHT: Instantiate(toggleLight, child.transform); break;
                case PlayerController.ActionType.JUMP: Instantiate(jump, child.transform); break;
                case PlayerController.ActionType.FUNC1: Instantiate(func1, child.transform); break;
                case PlayerController.ActionType.FUNC2: Instantiate(func2, child.transform); break;
            }

            nextChildIndex++;
        }
    }

    public void UpdatePlayerCommandList()
    {
        player.actions.Clear();
        var childDraggableCommand = GetComponentsInChildren<DragCommand>();

        foreach (var cmd in childDraggableCommand)
        {
            if (cmd.actionType != PlayerController.ActionType.FUNC1 || cmd.actionType != PlayerController.ActionType.FUNC2)
                player.actions.Add(cmd.actionType);
        }
    }
}
