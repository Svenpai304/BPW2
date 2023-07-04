using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemHoverText : Selectable
{
    public UI_ItemSlot slot;
    public GameObject hoverText;
    public GameObject currentHoverText;
    public bool mouseOver;
    public Vector3 offset = Vector3.zero;
    public string itemName;
    public string itemText;

    private void Update()
    {
        if (IsHighlighted())
        {
            OnHover();
        }
        else if (mouseOver)
        {
            OnHoverEnd();
        }
    }

    public void OnHover()
    {
        if (slot.heldItem == null) { return; }
        if (slot.heldItem.itemRef == null) { return; }

        if (!mouseOver || itemName != slot.heldItem.itemRef.hoverName)
        {
            mouseOver = true;
            itemName = slot.heldItem.itemRef.hoverName;
            itemText = slot.heldItem.itemRef.hoverText;
            InstantiateHoverText();
        }


    }

    private void OnHoverEnd()
    {
        mouseOver = false;
        DestroyHoverText();
    }

    public void InstantiateHoverText()
    {
        GameObject newHoverText = Instantiate(hoverText, transform, false);
        currentHoverText = newHoverText;
        newHoverText.GetComponent<RectTransform>().localPosition = offset;
        newHoverText.GetComponentInChildren<HoverTextName>().GetComponent<TextMeshProUGUI>().text = itemName;
        newHoverText.GetComponentInChildren<HoverTextDescription>().GetComponent<TextMeshProUGUI>().text = itemText;
    }

    public void DestroyHoverText()
    {
        if(currentHoverText != null)
        {
            Destroy(currentHoverText);
        }
    }
}
