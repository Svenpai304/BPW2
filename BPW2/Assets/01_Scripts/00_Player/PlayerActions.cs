using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using SimpleDungeon;

public class PlayerActions : MonoBehaviour
{
    public Vector3[] movementOptions = new Vector3[4] { Vector3.forward, Vector3.left, Vector3.right, Vector3.back };
    public Vector3 direction;
    public Transform playerModel;

    public bool playerTurn = false;
    public bool attackUIOn = false;
    public DungeonGenerator dungeon;
    public TurnController turnController;
    public InventoryManager inventoryManager;
    public UI_ItemSlot activeSlot;
    public int activeSlotNumber;
    public AttackUI attackUI;

    public ParticleSystem moveParticles;
    public ParticleSystem attackParticles;

    private void Start()
    {
        inventoryManager.playerActions = this;
        SetActiveSlot(1);
    }
    public void SetActiveSlot(int slot)
    {
        DisableAttackUI();
        activeSlot = inventoryManager.slots[slot - 1];
        activeSlotNumber = slot;
        inventoryManager.SetActiveSlot(activeSlot);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!playerTurn) { return; }
        Vector3 goal = transform.position;
        goal.x += context.ReadValue<Vector2>().x;
        goal.z += context.ReadValue<Vector2>().y;
        Vector3 chosenOption = Vector3.zero;
        Vector3Int chosenTile = Vector3Int.zero;
        float optionDistance = Mathf.Infinity;
        for (int i = 0; i < movementOptions.Length; i++)
        {
            Vector3 option = movementOptions[i] + transform.position;
            Vector3Int optionTile = new Vector3Int((int)option.x, 0, (int)option.z);
            if (Vector3.Distance(option, goal) < optionDistance)
            {
                optionDistance = Vector3.Distance(option, goal);
                chosenOption = movementOptions[i];
                chosenTile = optionTile;
            }
        }
        

        if (attackUIOn)
        {
            if (activeSlot.heldItem.itemRef.itemUseType == Item.ItemUseType.Orthogonal)
            {
                direction = chosenOption; 
                playerModel.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, direction, Vector3.up), 0);
            }
            else direction = Vector3.zero;
            UseDirectionalItem();
            return;
        }

        direction = chosenOption.normalized;
        playerModel.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, direction, Vector3.up), 0);

        RaycastHit[] hits = Physics.RaycastAll(transform.position + chosenOption + Vector3.up * 5, Vector3.down, 10);
        bool tileClear = true;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<EnemyController>() != null) { tileClear = false; break; }
            }
        }
        if (chosenOption != Vector3.zero && dungeon.IsTilePlayerWalkable(chosenTile) && tileClear)
        {
            transform.Translate(chosenOption); 
            if (moveParticles != null)
            {
                moveParticles.Stop();
                moveParticles.Clear();
                moveParticles.Play();
            }
        }

        turnController.currentTurn = TurnController.Turn.Enemy;
    }

    public void UseItem()
    {
        if (!playerTurn) { return; }
        if (activeSlot.heldItem == null) { return; }
        if (attackUIOn)
        {
            DisableAttackUI();
            return;
        }

        switch (activeSlot.heldItem.itemRef.itemUseType)
        {
            case Item.ItemUseType.None: 
                activeSlot.UseItem();
                turnController.currentTurn = TurnController.Turn.Enemy; break;
            case Item.ItemUseType.Orthogonal:
                attackUI.SetAttackUI(Item.ItemUseType.Orthogonal);
                attackUIOn = true;
                break;
            case Item.ItemUseType.Self:
                attackUI.SetAttackUI(Item.ItemUseType.Self);
                attackUIOn = true; break;
        }
    }

    public void UseDirectionalItem()
    {
        if (direction != Vector3.zero)
        {
            playerModel.rotation = Quaternion.Euler(new Vector3(0, Vector3.SignedAngle(Vector3.forward, direction, Vector3.up), 0));
        }
        DisableAttackUI();
        activeSlot.UseItem();
        if (attackParticles != null)
        {
            attackParticles.Stop();
            attackParticles.Clear();
            attackParticles.Play();
        }
        turnController.currentTurn = TurnController.Turn.Enemy;
    }

    public void DisableAttackUI()
    {
        attackUI.SetAttackUI(Item.ItemUseType.None);
        attackUIOn = false;
    }

    public void DropItem()
    {
        if (!playerTurn) { return; }
        if (activeSlot.heldItem != null)
        {
            activeSlot.heldItem.DropItem(transform.position + direction);
        }
    }

    public void DropPassiveItem()
    {
        if (!playerTurn) { return; }

        UI_ItemSlot passiveSlot = inventoryManager.slots[activeSlotNumber + 2];
        if (passiveSlot.heldItem != null)
        {
            passiveSlot.heldItem.DropItem(transform.position + direction);
        }
    }


}
