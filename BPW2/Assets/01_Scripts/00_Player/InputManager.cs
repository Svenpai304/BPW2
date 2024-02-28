using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{

    public PlayerActions playerActions;

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.UseItem();
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.DropItem();
    }

    public void OnDropPassive(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.DropPassiveItem();
    }
    public void OnSelectSlot1(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.SetActiveSlot(1);
    }
    public void OnSelectSlot2(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.SetActiveSlot(2);
        
    }
    public void OnSelectSlot3(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.SetActiveSlot(3);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
            playerActions.Move(context);
    }

}
