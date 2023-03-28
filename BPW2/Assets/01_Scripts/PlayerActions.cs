using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using SimpleDungeon;

public class PlayerActions : MonoBehaviour
{
    public float moveTime;
    public Vector3[] movementOptions = new Vector3[4] { Vector3.forward, Vector3.left, Vector3.right, Vector3.back };

    public bool playerTurn = false;
    public DungeonGenerator dungeon;
    public TurnController turnController;
    public InventoryManager inventoryManager;
    public UI_ItemSlot activeSlot;

    public void Start()
    {
    }

    
    public void SetActiveSlot(int slot)
    {
        activeSlot = inventoryManager.slots[slot - 1];
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!playerTurn) { return; }
        Vector3 goal = transform.position;
        goal.x += context.ReadValue<Vector2>().x;
        goal.z += context.ReadValue<Vector2>().y;
        Vector3 chosenOption = Vector3.zero;
        float optionDistance = Mathf.Infinity;
        for (int i = 0; i < movementOptions.Length; i++)
        {
            Vector3 option = movementOptions[i] + transform.position;
            Vector3Int optionTile = new Vector3Int((int)option.x, 0, (int)option.z);
            if (dungeon.IsTileWalkable(optionTile) && Vector3.Distance(option, goal) < optionDistance)
            {
                optionDistance = Vector3.Distance(option, goal);
                chosenOption = movementOptions[i];
            }
        }
        if (chosenOption != Vector3.zero)
        {
            transform.Translate(chosenOption);
        }

        turnController.currentTurn = TurnController.Turn.Enemy;
    }

    public void UseItem()
    {
        if (!playerTurn) { return; }
        if (activeSlot.heldItem != null)
        {
            activeSlot.UseItem();
            turnController.currentTurn = TurnController.Turn.Enemy;
        }
    }

    public void DropItem()
    {
        if (!playerTurn) { return; }
        if (activeSlot.heldItem != null)
        {
            activeSlot.heldItem.DropItem(transform.position);
        }
    }

    
}
