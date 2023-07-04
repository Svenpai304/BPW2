using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class AttackUI : MonoBehaviour
{
    public PlayerActions actions;
    public Transform orthogonal;
    public Transform self;

    public void SetAttackUI(Item.ItemUseType type)
    {
        orthogonal.gameObject.SetActive(false); 
        self.gameObject.SetActive(false);
        switch (type)
        {
            case Item.ItemUseType.None: break;
            case Item.ItemUseType.Orthogonal: 
                orthogonal.gameObject.SetActive(true);
                break;
            case Item.ItemUseType.Self: 
                self.gameObject.SetActive(true);
                break;
        }
    }

    public void UseItem(string direction)
    {
        switch (direction)
        {
            case "Zero": actions.direction = new Vector3 (0, 0, 0); break;
            case "Z+": actions.direction = new Vector3(0, 0, 1); break;
            case "Z-": actions.direction = new Vector3(0, 0, -1); break;
            case "X+": actions.direction = new Vector3(1, 0, 0); break;
            case "X-": actions.direction = new Vector3(-1, 0, 0); break;
            default: break;
        }

        actions.UseDirectionalItem();
    }


}
